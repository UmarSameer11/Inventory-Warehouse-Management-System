namespace WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock
{
    /// <summary>
    /// Batch dropdown item that also carries its ProductId,
    /// so the form can filter batches by the selected product.
    /// </summary>
    public class BatchOptionViewModel
    {
        public int Value { get; set; }
        public string Text { get; set; } = null!;
        public int ProductId { get; set; }
    }
}
