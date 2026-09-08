using Microsoft.AspNetCore.Identity;

namespace WarehouseManagementSystemApi.Models.Auth
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
