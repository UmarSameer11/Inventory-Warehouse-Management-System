using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;
using WarehouseManagementSystemApi.Models.ApiPerformance;
using WarehouseManagementSystemApi.Models.AuditLog;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.MiddleWares.AuditMiddleware
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUser;
        //private readonly Dictionary<DbContext, List<AuditEntry>> _auditEntries = new();
        public AuditInterceptor(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        /// SavingChangesAsync Method
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
     DbContextEventData eventData,
     InterceptionResult<int> result,
     CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
            {
                return await base.SavingChangesAsync(
                    eventData,
                    result,
                    cancellationToken);
            }

            var auditEntries = CreateAuditEntries(context);

            foreach (var auditEntry in auditEntries)
            {
                context.Set<AuditLog>()
                    .Add(ToAuditLog(auditEntry));
            }

            return await base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        /// Create Audit Entries Method

        private List<AuditEntry> CreateAuditEntries(
                DbContext context)
        {
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog ||
                    entry.Entity is ApiPerformanceLog)
                {
                    continue;
                }

                if (entry.State == EntityState.Detached)
                    continue;

                if (entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);

                auditEntry.UserId = _currentUser.UserId;

                auditEntry.UserName = _currentUser.UserName;

                auditEntry.Role = _currentUser.Role;

                auditEntry.Method = _currentUser.Method;

                auditEntry.Path = _currentUser.Path;

                auditEntry.IPAddress = _currentUser.IPAddress;

                auditEntry.UserAgent = _currentUser.UserAgent;

                auditEntry.TraceId = _currentUser.TraceId;
                auditEntry.CorrelationId = _currentUser.CorrelationId;

                auditEntry.TableName =
                    entry.Metadata.GetTableName()!;

                auditEntry.Action =
                    entry.State.ToString();

                foreach (var property in entry.Properties)
                {
                    ProcessProperty(
                        auditEntry,
                        property,
                        entry.State);
                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }

        /// Process Property Method
        private void ProcessProperty(
               AuditEntry auditEntry,
               PropertyEntry property,
               EntityState state)
        {
            var propertyName = property.Metadata.Name;


            // 1. Temporary Property Check
            if (property.IsTemporary)
            {
                auditEntry.TemporaryProperties.Add(property);
                return;
            }


            // 2. Primary Key Store
            if (property.Metadata.IsPrimaryKey())
            {
                auditEntry.KeyValues[propertyName] =
                    property.CurrentValue;

                return;
            }


            // 3. Entity State Handling

            switch (state)
            {
                case EntityState.Added:

                    auditEntry.NewValues[propertyName] =
                        property.CurrentValue;

                    break;


                case EntityState.Deleted:

                    auditEntry.OldValues[propertyName] =
                        property.OriginalValue;

                    break;


                case EntityState.Modified:

                    if (property.IsModified)
                    {
                        auditEntry.ChangedColumns
                            .Add(propertyName);


                        auditEntry.OldValues[propertyName] =
                            property.OriginalValue;


                        auditEntry.NewValues[propertyName] =
                            property.CurrentValue;
                    }

                    break;
            }
        }

        // Audit entity changed to Audit log model 
        private AuditLog ToAuditLog(

             AuditEntry auditEntry)
        {
            return new AuditLog
            {
                UserId = auditEntry.UserId,

                UserName = auditEntry.UserName,

                Role = auditEntry.Role,

                Method = auditEntry.Method,

                Path = auditEntry.Path,

                IPAddress = auditEntry.IPAddress,

                UserAgent = auditEntry.UserAgent,

                TraceId = auditEntry.TraceId,

                TableName = auditEntry.TableName,

                Action = auditEntry.Action,
                CorrelationId = auditEntry.CorrelationId,


                PrimaryKey =
                    JsonSerializer.Serialize(
                        auditEntry.KeyValues),


                OldValues =
                    JsonSerializer.Serialize(
                        auditEntry.OldValues),


                NewValues =
                    JsonSerializer.Serialize(
                        auditEntry.NewValues),


                ChangedColumns =
                    JsonSerializer.Serialize(
                        auditEntry.ChangedColumns),


                CreatedAt = DateTime.UtcNow
            };
        }

        /// Saved Changed method 
     //   public override async ValueTask<int> SavedChangesAsync(
     //SaveChangesCompletedEventData eventData,
     //int result,
     //CancellationToken cancellationToken = default)
     //   {
     //       var context = eventData.Context;

     //       if (context == null)
     //       {
     //           return await base.SavedChangesAsync(
     //               eventData,
     //               result,
     //               cancellationToken);
     //       }

     //       if (_auditEntries.TryGetValue(
     //           context,
     //           out var auditEntries))
     //       {
     //           foreach (var auditEntry in auditEntries)
     //           {
     //               if (!auditEntry.HasTemporaryProperties)
     //                   continue;

     //               foreach (var property in auditEntry.TemporaryProperties)
     //               {
     //                   auditEntry.KeyValues[
     //                       property.Metadata.Name] =
     //                       property.CurrentValue;
     //               }

     //               context.Set<AuditLog>()
     //                   .Add(ToAuditLog(auditEntry));
     //           }

     //           await context.SaveChangesAsync(
     //               cancellationToken);

     //           _auditEntries.Remove(context);
     //       }

     //       return await base.SavedChangesAsync(
     //           eventData,
     //           result,
     //           cancellationToken);
     //   }

        /// Save Changes Faild 
        //public override Task SaveChangesFailedAsync(
        //       DbContextErrorEventData eventData,
        //       CancellationToken cancellationToken = default)
        //{
        //    if (eventData.Context != null)
        //    {
        //        _auditEntries.Remove(eventData.Context);
        //    }


        //    return base.SaveChangesFailedAsync(
        //        eventData,
        //        cancellationToken);
        //}
    }
}

