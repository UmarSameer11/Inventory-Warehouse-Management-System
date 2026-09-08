using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemApi.Models.AuditLog
{
    public class AuditLog
    {
        [Key]
        public long AuditLogId { get; set; }

        // User Information
        public string? UserId { get; set; }

        [MaxLength(150)]
        public string? UserName { get; set; }

        [MaxLength(100)]
        public string? Role { get; set; }

        // Request Information
        [MaxLength(10)]
        public string? Method { get; set; }

        [MaxLength(500)]
        public string? Path { get; set; }

        [MaxLength(100)]
        public string? IPAddress { get; set; }

        [MaxLength(500)]
        public string? UserAgent { get; set; }

        [MaxLength(100)]
        public string? TraceId { get; set; }

        // Entity Information
        [Required]
        [MaxLength(100)]
        public string TableName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Action { get; set; } = string.Empty;

        // Audit Data
        public string? PrimaryKey { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? ChangedColumns { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// Correlation middleware Id
        public string? CorrelationId { get; set; }
    }
}
