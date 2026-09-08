using Microsoft.AspNetCore.Identity;

namespace WarehouseManagementSystemApi.Models.Auth
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
