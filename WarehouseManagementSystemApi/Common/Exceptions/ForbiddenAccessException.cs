namespace WarehouseManagementSystemApi.Common.Exceptions
{
    /// <summary>Thrown when the caller is authenticated but not allowed to proceed, e.g. deactivated account (HTTP 403).</summary>
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message) : base(message) { }
    }
}
