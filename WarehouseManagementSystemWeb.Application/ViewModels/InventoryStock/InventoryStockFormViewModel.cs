using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock
{
    /// <summary>
    /// Shared dropdown data for the Create and Update forms.
    /// </summary>
    public abstract class InventoryStockFormViewModel
    {
        [JsonIgnore]
        public List<SelectListItem> Warehouses { get; set; } = new();

        [JsonIgnore]
        public List<SelectListItem> Products { get; set; } = new();

        [JsonIgnore]
        public List<BatchOptionViewModel> Batches { get; set; } = new();
    }
}
