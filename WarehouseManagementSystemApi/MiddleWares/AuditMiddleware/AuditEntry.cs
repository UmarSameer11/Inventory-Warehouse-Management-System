using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WarehouseManagementSystemApi.MiddleWares.AuditMiddleware
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }

        //Current User
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        // Request
        public string? Method { get; set; }
        public string? Path { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? TraceId { get; set; }
        public string? CorrelationId { get; set; }

        // Entity
        public string TableName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;

        // Data
        public Dictionary<string, object?> KeyValues { get; }
            = new();
        public Dictionary<string, object?> OldValues { get; }
            = new();
        public Dictionary<string, object?> NewValues { get; }
            = new();
        public List<string> ChangedColumns { get; }
            = new();
        public List<PropertyEntry> TemporaryProperties { get; }
            = new();
        public bool HasTemporaryProperties
            => TemporaryProperties.Count > 0;
    }
}
