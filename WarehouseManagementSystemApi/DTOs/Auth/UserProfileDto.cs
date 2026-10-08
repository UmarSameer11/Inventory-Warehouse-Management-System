namespace WarehouseManagementSystemApi.DTOs.Auth
{
    public class UserProfileDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
        public bool IsActive { get; set; }
        public DateTime? LastLoginAtUtc { get; set; }
    }
}
