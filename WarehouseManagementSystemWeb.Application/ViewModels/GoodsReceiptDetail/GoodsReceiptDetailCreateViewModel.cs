namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceiptDetail
{
    public class GoodsReceiptDetailCreateViewModel
    {
        public int PurchaseOrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        //public decimal Quantity { get; set; }
        public decimal QuantityReceived { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

