using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    /// <summary>
    /// Housekeeping: every few hours removes sessions / refresh tokens that ended more than
    /// RetentionDays ago. The retention window is kept on purpose so refresh-token reuse
    /// detection keeps working for recently rotated tokens.
    /// </summary>
    public class ExpiredSessionCleanupService : BackgroundService
    {
        private static readonly TimeSpan Interval = TimeSpan.FromHours(6);
        private const int RetentionDays = 7;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpiredSessionCleanupService> _logger;

        public ExpiredSessionCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<ExpiredSessionCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Give the app time to start (and migrations to be applied) before the first run.
            try { await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); }
            catch (OperationCanceledException) { return; }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanupAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Session cleanup failed");
                }

                try { await Task.Delay(Interval, stoppingToken); }
                catch (OperationCanceledException) { return; }
            }
        }

        private async Task CleanupAsync(CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var cutoff = DateTime.UtcNow.AddDays(-RetentionDays);

            // Bulk deletes bypass SaveChanges, so they do not create audit-log noise.
            // Deleting a session cascades to its refresh tokens.
            var sessions = await db.UserSessions
                .Where(s => (s.RevokedAtUtc != null && s.RevokedAtUtc < cutoff) || s.ExpiresAtUtc < cutoff)
                .ExecuteDeleteAsync(ct);

            // Old rotated / expired tokens that belong to still-active sessions.
            var tokens = await db.RefreshTokens
                .Where(t => (t.RevokedAtUtc != null && t.RevokedAtUtc < cutoff) || t.ExpiresAtUtc < cutoff)
                .ExecuteDeleteAsync(ct);

            if (sessions > 0 || tokens > 0)
            {
                _logger.LogInformation("Session cleanup removed {Sessions} sessions and {Tokens} refresh tokens",
                    sessions, tokens);
            }
        }
    }
}
