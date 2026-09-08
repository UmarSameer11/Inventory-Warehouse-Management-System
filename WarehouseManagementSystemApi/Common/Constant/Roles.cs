namespace WarehouseManagementSystemApi.Common.Constant
{
    public class Roles
    {
        public const string Admin = "Admin";

        public const string Manager = "Manager";

        public const string Employee = "Employee";

        public static readonly string[] All = { Admin, Manager, Employee };

        public static bool IsValid(string role) =>
            !string.IsNullOrWhiteSpace(role) && All.Contains(role, StringComparer.OrdinalIgnoreCase);
    }
}
