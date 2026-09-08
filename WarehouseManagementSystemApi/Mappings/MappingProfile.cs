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
            CreateMap<ProductListDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();

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

        }
    }
}
