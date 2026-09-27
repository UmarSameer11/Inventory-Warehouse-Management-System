namespace WarehouseManagementSystemWeb.Application.ViewModels.Batch
{
    public class BatchListViewModel
    {
        public int BatchId { get; set; }

        // Filled by API if BatchListDto exposes it, otherwise resolved in BatchService by ProductName.
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Computed helpers (read-only, never sent to / read from the API)
        public bool HasExpiry => ExpiryDate.HasValue;

        public bool IsExpired =>
            ExpiryDate.HasValue && ExpiryDate.Value.Date < DateTime.Today;

        public bool IsExpiringSoon =>
            ExpiryDate.HasValue
            && !IsExpired
            && ExpiryDate.Value.Date <= DateTime.Today.AddDays(30);
    }
}
