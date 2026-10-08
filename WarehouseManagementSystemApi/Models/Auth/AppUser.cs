using Microsoft.AspNetCore.Identity;

namespace WarehouseManagementSystemApi.Models.Auth
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        /// <summary>Deactivated users cannot log in or refresh tokens.</summary>
        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginAtUtc { get; set; }
    }
}
