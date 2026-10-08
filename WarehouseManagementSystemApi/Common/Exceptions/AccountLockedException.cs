namespace WarehouseManagementSystemApi.Common.Exceptions
{
    /// <summary>Thrown when an account is temporarily locked after too many failed logins (HTTP 423).</summary>
    public class AccountLockedException : Exception
    {
        public AccountLockedException(string message) : base(message) { }
    }
}
