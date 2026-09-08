using Microsoft.AspNetCore.Identity;
using WarehouseManagementSystemApi.Common.Constant;
using WarehouseManagementSystemApi.Models.Auth;

namespace WarehouseManagementSystemApi.Extensions
{
    public class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var config = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<object>>();

            foreach (var roleName in Roles.All)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new AppRole
                    {
                        Name = roleName,
                        Description = $"{roleName} role"
                    };

                    var res = await roleManager.CreateAsync(role);

                    if (!res.Succeeded)
                    {
                        logger.LogError(
                            "Failed to create role {Role}: {Errors}",
                            roleName,
                            string.Join("; ", res.Errors.Select(e => e.Description)));
                    }
                }
            }

            var anyAdminExists = (await userManager.GetUsersInRoleAsync(Roles.Admin)).Any();
            if (anyAdminExists)
            {
                return;
            }

            var seedEmail = config["SeedAdmin:Email"];
            var seedPassword = config["SeedAdmin:Password"];

            if (string.IsNullOrWhiteSpace(seedEmail) || string.IsNullOrWhiteSpace(seedPassword))
            {
                logger.LogWarning(
                    "No Admin account exists and SeedAdmin:Email/Password are not configured. " +
                    "Set them (User Secrets locally, App Settings in Azure) and restart to bootstrap the first Admin.");
                return;
            }

            var admin = new AppUser
            {
                Name = "System Administrator",
                Email = seedEmail,
                UserName = seedEmail
            };

            var result = await userManager.CreateAsync(admin, seedPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Roles.Admin);
                logger.LogInformation("Bootstrap Admin account created for {Email}", seedEmail);
            }
            else
            {
                logger.LogError(
                    "Failed to create bootstrap Admin: {Errors}",
                    string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
