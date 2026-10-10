using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Employee;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    /// <summary>
    /// Builds dropdown lists from the already existing services (Employee, Product, Warehouse, Batch).
    /// The API answers 404 when a table is empty; that is treated as an empty list so the
    /// Create / Update pages do not crash.
    /// </summary>
    internal static class DropdownHelper
    {
        public static async Task<List<EmployeeListViewModel>> GetEmployeesAsync(IEmployeeService employeeService)
        {
            try
            {
                var employees = await employeeService.GetAllAsync();
                return employees.Where(e => e != null).Select(e => e!).ToList();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<EmployeeListViewModel>();
            }
        }

        public static async Task<List<SelectListItem>> EmployeesAsync(IEmployeeService employeeService)
        {
            var employees = await GetEmployeesAsync(employeeService);

            return employees
                .OrderBy(e => e.EmployeeCode)
                .Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = $"{e.EmployeeCode} - {e.FirstName} {e.LastName}"
                })
                .ToList();
        }

        public static async Task<List<SelectListItem>> ProductsAsync(IProductService productService, int? includeProductId = null)
        {
            try
            {
                var products = await productService.GetAllAsync();

                return products
                    .Where(p => p != null && (p.IsActive || p.ProductId == includeProductId))
                    .OrderBy(p => p!.ProductName)
                    .Select(p => new SelectListItem
                    {
                        Value = p!.ProductId.ToString(),
                        Text = $"{p.ProductCode} - {p.ProductName}"
                    })
                    .ToList();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<SelectListItem>();
            }
        }

        public static async Task<List<SelectListItem>> WarehousesAsync(IWarehouseService warehouseService, int? includeWarehouseId = null)
        {
            try
            {
                var warehouses = await warehouseService.GetAllAsync();

                return warehouses
                    .Where(w => w != null && (w.IsActive || w.WarehouseId == includeWarehouseId))
                    .OrderBy(w => w!.WarehouseName)
                    .Select(w => new SelectListItem
                    {
                        Value = w!.WarehouseId.ToString(),
                        Text = $"{w.WarehouseCode} - {w.WarehouseName}"
                    })
                    .ToList();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<SelectListItem>();
            }
        }

        public static async Task<List<SelectListItem>> BatchesAsync(IBatchService batchService)
        {
            var batches = await batchService.GetAllAsync();

            return batches
                .OrderBy(b => b.BatchNumber)
                .Select(b => new SelectListItem
                {
                    Value = b.BatchId.ToString(),
                    Text = $"{b.BatchNumber} - {b.ProductName}"
                })
                .ToList();
        }
    }
}
