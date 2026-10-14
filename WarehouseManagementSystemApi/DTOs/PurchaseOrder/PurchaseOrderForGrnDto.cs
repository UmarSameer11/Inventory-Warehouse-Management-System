namespace WarehouseManagementSystemApi.DTOs.PurchaseOrder
{
   public class PurchaseOrderForGrnDto
        {
            public int PurchaseOrderId { get; set; }
            public string PurchaseOrderNumber { get; set; } = string.Empty;

            public List<PurchaseOrderGrnDetailDto> Details { get; set; } = new();
        }

        public class PurchaseOrderGrnDetailDto
        {
            public int PurchaseOrderDetailId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;

            public decimal OrderedQuantity { get; set; }
            public decimal PreviouslyReceivedQuantity { get; set; }
            public decimal RemainingQuantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }

