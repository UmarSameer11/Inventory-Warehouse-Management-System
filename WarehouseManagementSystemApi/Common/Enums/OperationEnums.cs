namespace WarehouseManagementSystemApi.Common.Enums
{
    public enum PurchaseOrderStatus { Draft, Approved, PartiallyReceived, Received, Cancelled }

    public enum SalesOrderStatus { Draft, Pending, Approved, PartiallyDispatched, Dispatched, Delivered, Cancelled }

    public enum DispatchStatus { Pending, Loaded, InTransit, Delivered, Cancelled }

    public enum StockTransferStatus { Draft, InTransit, Received, Cancelled }

    /// <summary>Pending -> Inspected (good/damaged split done) -> Completed (stock posted).</summary>
    public enum SalesReturnStatus { Pending, Inspected, Completed, Rejected }

    public enum StockMovementType { Purchase, Sale, Return, TransferIn, TransferOut, Adjustment, Damage, Expired }
}
