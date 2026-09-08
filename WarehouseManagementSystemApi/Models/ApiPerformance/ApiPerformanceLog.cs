using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemApi.Models.ApiPerformance
{
    public class ApiPerformanceLog
    {
        [Key]
            public int Id { get; set; }
            public string HttpMethod { get; set; } = null!;
            public string Endpoint { get; set; } = null!;
            public long ExecutionTime { get; set; }
            public int StatusCode { get; set; }
            public string? UserId { get; set; }
            public string? UserName { get; set; }
            public string? IpAddress { get; set; }
            public string? CorrelationId { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }

