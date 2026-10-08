using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.StockTransfer
{
    public class StockTransferDetails
    {
        [Key]
        public int StockTransferDetailId { get; set; }
        public int StockTransferId { get; set; }
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        [Precision(18, 3)]
        public decimal Quantity { get; set; }
        public StockTransfers StockTransfer { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Batches Batch { get; set; } = null!;
    }
}
