using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemWeb.Models.Auth
{
    // ---------- Forms (MVC view models) ----------

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Keep me signed in")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }

    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,128}$",
            ErrorMessage = "Min 8 characters with uppercase, lowercase, number and special character.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } = "Employee";
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,128}$",
            ErrorMessage = "Min 8 characters with uppercase, lowercase, number and special character.")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm new password is required.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "New password and confirm password do not match.")]
        [Display(Name = "Confirm new password")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }

    public class ProfilePageViewModel
    {
        public UserProfileModel Profile { get; set; } = new();
        public List<SessionModel> Sessions { get; set; } = new();
    }

    // ---------- API contract (mirrors the API's DTOs) ----------

    /// <summary>The API wraps every successful response as { Success, Message, Data, ... }.</summary>
    public class ApiEnvelope<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }

    /// <summary>Shape of the API's ErrorResponse.</summary>
    public class ApiErrorModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
    }

    public class AuthTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiresAtUtc { get; set; }
        public Guid SessionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }

    public class UserProfileModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public bool IsActive { get; set; }
        public DateTime? LastLoginAtUtc { get; set; }
    }

    public class SessionModel
    {
        public Guid Id { get; set; }
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastActivityUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public bool IsCurrent { get; set; }
    }

    /// <summary>Result of a call to the auth endpoints of the API. Never throws for 4xx.</summary>
    public class AuthResult<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public string? ErrorMessage { get; init; }
        public int StatusCode { get; init; }

        public static AuthResult<T> Ok(T? data, int status = 200) =>
            new() { Success = true, Data = data, StatusCode = status };

        public static AuthResult<T> Fail(string message, int status) =>
            new() { Success = false, ErrorMessage = message, StatusCode = status };
    }
}
