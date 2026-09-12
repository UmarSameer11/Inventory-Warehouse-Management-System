namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IApiService
    {
        //Task<T?> GetAsync<T>(string endpoint); 

        //Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);

        //Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request);

        //Task<bool> DeleteAsync(string endpoint);

        Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default); 
        Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default); 
        Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default); 
        Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default);
    }
}

