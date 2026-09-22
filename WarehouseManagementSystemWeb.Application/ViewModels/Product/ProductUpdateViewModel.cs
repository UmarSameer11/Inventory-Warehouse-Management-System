using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Product
{
    public class ProductUpdateViewModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int ProductCategoryId { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; }

        // Dropdowns 
        public List<SelectListItem> UnitOfMeasures { get; set; } = new();
        public List<SelectListItem> ProductCategories { get; set; } = new();
    }
}
