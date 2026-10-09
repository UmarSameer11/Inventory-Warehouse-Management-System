using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Batch;
using WarehouseManagementSystemApi.DTOs.Department;
using WarehouseManagementSystemApi.DTOs.Designation;
using WarehouseManagementSystemApi.DTOs.Employee;
using WarehouseManagementSystemApi.DTOs.InventoryStock;
using WarehouseManagementSystemApi.DTOs.Product;
using WarehouseManagementSystemApi.DTOs.ProductCategory;
using WarehouseManagementSystemApi.DTOs.UnitOfMeasure;
using WarehouseManagementSystemApi.DTOs.Warehouse;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;
using WarehouseManagementSystemApi.Models.Warehouse;

using WarehouseManagementSystemApi.DTOs.Customer;
using WarehouseManagementSystemApi.Models.Customer;
using WarehouseManagementSystemApi.DTOs.Dispatch;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.DTOs.ReturnReason;
using WarehouseManagementSystemApi.Models.ReturnReason;
using WarehouseManagementSystemApi.DTOs.Salesman;
using WarehouseManagementSystemApi.Models.Salesman;
using WarehouseManagementSystemApi.DTOs.SalesOrder;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.DTOs.SalesReturn;
using WarehouseManagementSystemApi.Models.SalesReturn;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;
using WarehouseManagementSystemApi.Models.StockAdjustment;
using WarehouseManagementSystemApi.DTOs.StockTransfer;
using WarehouseManagementSystemApi.Models.StockTransfer;
using WarehouseManagementSystemApi.DTOs.Supplier;
using WarehouseManagementSystemApi.Models.Supplier;
using WarehouseManagementSystemApi.DTOs.Vehicle;
using WarehouseManagementSystemApi.Models.Vehicle;
using WarehouseManagementSystemApi.DTOs.VehicleAssignment;
using WarehouseManagementSystemApi.Models.VehicleAssignment;
using WarehouseManagementSystemApi.DTOs.VehicleType;
using WarehouseManagementSystemApi.Models.VehicleType;
namespace WarehouseManagementSystemApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /// Employee
            CreateMap<EmployeeCreateDto, Employees>().ReverseMap();
            CreateMap<Employees, EmployeeListDto>()
                .ForMember(d => d.DepartmentName,
                o => o.MapFrom(s => s.Department.DepartmentName))
                .ForMember(d => d.DesignationName,
                o => o.MapFrom(s => s.Designation.DesignationName));

            CreateMap<EmployeeUpdateDto, Employees>().ReverseMap();

            /// Department
            CreateMap<DepartmentCreateDto, Departments>().ReverseMap();
            CreateMap<DepartmentListDto, Departments>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Departments>().ReverseMap();

            /// Employee
            CreateMap<DesignationCreateDto, Designation>().ReverseMap();
            CreateMap<DesignationListDto, Designation>().ReverseMap();
            CreateMap<DesignationUpdateDto, Designation>().ReverseMap();

            /// Product
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<Product, ProductListDto>()
              .ForMember(
                   dest => dest.CategoryName,
                   opt => opt.MapFrom(src => src.ProductCategory.CategoryName)
                        )
              .ForMember(
                   dest => dest.UnitName,
                   opt => opt.MapFrom(src => src.UnitOfMeasure.UnitName)
                       );

            /// Unit of Messure
            CreateMap<UnitOfMeasureCreateDto, UnitOfMeasures>().ReverseMap();
            CreateMap<UnitOfMeasureListDto, UnitOfMeasures>().ReverseMap();
            CreateMap<UnitOfMeasureUpdateDto, UnitOfMeasures>().ReverseMap();

            /// Product Category
            CreateMap<ProductCategoryCreateDto, ProductCategories>().ReverseMap();
            CreateMap<ProductCategoryListDto, ProductCategories>().ReverseMap();
            CreateMap<ProductCategoryUpdateDto, ProductCategories>().ReverseMap();

            /// Batches
            CreateMap<BatchCreateDto, Batches>().ReverseMap();
            CreateMap<BatchListDto, Batches>().ReverseMap()
                 .ForMember(b => b.ProductName,
                o => o.MapFrom(b => b.Product.ProductName));
            CreateMap<BatchUpdateDto, Batches>().ReverseMap();

            /// Inventory Stock
            CreateMap<InventoryStockCreateDto, InventoryStocks>().ReverseMap();
            CreateMap<InventoryStockListDto, InventoryStocks>().ReverseMap()
                .ForMember(a => a.ProductName,
                o => o.MapFrom(a => a.Product.ProductName))
                .ForMember(a => a.WarehouseName,
                o => o.MapFrom(a => a.Warehouse.WarehouseName))
                .ForMember(a => a.BatchNumber,
                o => o.MapFrom(a => a.Batch.BatchNumber));
            CreateMap<InventoryStockUpdateDto, InventoryStocks>().ReverseMap();

            /// Warehouse
            CreateMap<WarehouseCreateDto, Warehouses>().ReverseMap();
            CreateMap<WarehouseListDto, Warehouses>().ReverseMap()
                 .ForMember(b => b.EmployeeName,
                o => o.MapFrom(b => b.Employee.FirstName));
            CreateMap<WarehouseUpdateDto, Warehouses>().ReverseMap();

            // Newly added operational modules
            CreateMap<CustomerCreateDto, Customers>().ReverseMap();
            CreateMap<Customers, CustomerListDto>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customers>().ReverseMap();
            CreateMap<DispatchCreateDto, Dispatches>().ReverseMap();
            CreateMap<Dispatches, DispatchListDto>().ReverseMap();
            CreateMap<DispatchUpdateDto, Dispatches>().ReverseMap();
            CreateMap<GoodsReceiptCreateDto, GoodsReceipts>().ReverseMap();
            CreateMap<GoodsReceipts, GoodsReceiptListDto>().ReverseMap();
            CreateMap<PurchaseOrderCreateDto, PurchaseOrders>().ReverseMap();
            CreateMap<PurchaseOrders, PurchaseOrderListDto>().ReverseMap();
            CreateMap<PurchaseOrderUpdateDto, PurchaseOrders>().ReverseMap();
            CreateMap<ReturnReasonCreateDto, ReturnReasons>().ReverseMap();
            CreateMap<ReturnReasons, ReturnReasonListDto>().ReverseMap();
            CreateMap<ReturnReasonUpdateDto, ReturnReasons>().ReverseMap();
            CreateMap<SalesmanCreateDto, Salesmen>().ReverseMap();
            CreateMap<Salesmen, SalesmanListDto>().ReverseMap();
            CreateMap<SalesmanUpdateDto, Salesmen>().ReverseMap();
            CreateMap<SalesOrderCreateDto, SalesOrders>().ReverseMap();
            CreateMap<SalesOrders, SalesOrderListDto>().ReverseMap();
            CreateMap<SalesOrderUpdateDto, SalesOrders>().ReverseMap();
            CreateMap<SalesReturnCreateDto, SalesReturns>().ReverseMap();
            CreateMap<SalesReturns, SalesReturnListDto>().ReverseMap();
            CreateMap<StockAdjustmentCreateDto, StockAdjustments>().ReverseMap();
            CreateMap<StockAdjustments, StockAdjustmentListDto>().ReverseMap();
            CreateMap<StockTransferCreateDto, StockTransfers>().ReverseMap();
            CreateMap<StockTransfers, StockTransferListDto>().ReverseMap();
            CreateMap<StockTransferUpdateDto, StockTransfers>().ReverseMap();
            CreateMap<SupplierCreateDto, Suppliers>().ReverseMap();
            CreateMap<Suppliers, SupplierListDto>().ReverseMap();
            CreateMap<SupplierUpdateDto, Suppliers>().ReverseMap();
            CreateMap<VehicleCreateDto, Vehicles>().ReverseMap();
            CreateMap<Vehicles, VehicleListDto>().ReverseMap();
            CreateMap<VehicleUpdateDto, Vehicles>().ReverseMap();
            CreateMap<VehicleAssignmentCreateDto, VehicleAssignments>().ReverseMap();
            CreateMap<VehicleAssignments, VehicleAssignmentListDto>().ReverseMap();
            CreateMap<VehicleAssignmentUpdateDto, VehicleAssignments>().ReverseMap();
            CreateMap<VehicleTypeCreateDto, VehicleTypes>().ReverseMap();
            CreateMap<VehicleTypes, VehicleTypeListDto>().ReverseMap();
            CreateMap<VehicleTypeUpdateDto, VehicleTypes>().ReverseMap();

            // Operational detail mappings (used by aggregate workflows)
            CreateMap<DispatchDetailCreateDto, DispatchDetails>().ReverseMap();
            CreateMap<DispatchDetails, DispatchDetailListDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.BatchNumber, o => o.MapFrom(s => s.Batch.BatchNumber))
                .ForMember(d => d.ExpiryDate, o => o.MapFrom(s => s.Batch.ExpiryDate));
            CreateMap<GoodsReceiptDetailCreateDto, GoodsReceiptDetails>();
            CreateMap<GoodsReceiptDetails, GoodsReceiptDetailListDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.BatchNumber, o => o.MapFrom(s => s.Batch.BatchNumber))
                .ForMember(d => d.ManufacturingDate, o => o.MapFrom(s => s.Batch.ManufacturingDate))
                .ForMember(d => d.ExpiryDate, o => o.MapFrom(s => s.Batch.ExpiryDate));
            CreateMap<PurchaseOrderDetailCreateDto, PurchaseOrderDetails>().ReverseMap();
            CreateMap<PurchaseOrderDetails, PurchaseOrderDetailListDto>()
                .ForMember(d => d.ProductCode, o => o.MapFrom(s => s.Product.ProductCode))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.UnitName, o => o.MapFrom(s => s.Product.UnitOfMeasure.UnitName))
                .ForMember(d => d.LineTotal, o => o.MapFrom(s => s.Quantity * s.UnitPrice));
            CreateMap<SalesOrderDetailCreateDto, SalesOrderDetails>().ReverseMap();
            CreateMap<SalesOrderDetails, SalesOrderDetailListDto>()
                .ForMember(d => d.ProductCode, o => o.MapFrom(s => s.Product.ProductCode))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.LineTotal, o => o.MapFrom(s => s.Quantity * s.UnitPrice));
            CreateMap<SalesReturnDetailCreateDto, SalesReturnDetails>().ReverseMap();
            CreateMap<SalesReturnDetails, SalesReturnDetailListDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.BatchNumber, o => o.MapFrom(s => s.Batch == null ? null : s.Batch.BatchNumber))
                .ForMember(d => d.ReasonName, o => o.MapFrom(s => s.ReturnReason.ReasonName));
            CreateMap<StockAdjustmentDetailCreateDto, StockAdjustmentDetails>();
            CreateMap<StockAdjustmentDetails, StockAdjustmentDetailListDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.BatchNumber, o => o.MapFrom(s => s.Batch.BatchNumber));
            CreateMap<StockTransferDetailCreateDto, StockTransferDetails>().ReverseMap();
            CreateMap<StockTransferDetails, StockTransferDetailListDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.BatchNumber, o => o.MapFrom(s => s.Batch.BatchNumber));

        }
    }
}
