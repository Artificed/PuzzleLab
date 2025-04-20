namespace PuzzleLab.Web.Services.Api.Interfaces;

public interface IApiClient
{
    Task<T?> GetAsync<T>(string requestUri, CancellationToken cancellationToken = default);
    Task<T?> PostAsync<T>(string requestUri, object data, CancellationToken cancellationToken = default);
    Task<T?> PutAsync<T>(string requestUri, object data, CancellationToken cancellationToken = default);
    Task PostAsync(string requestUri, object data, CancellationToken cancellationToken = default);
    Task PutAsync(string requestUri, object data, CancellationToken cancellationToken = default);
    Task DeleteAsync(string requestUri, object data, CancellationToken cancellationToken = default);
    Task<T?> DeleteAsync<T>(string requestUri, object data, CancellationToken cancellationToken = default);
}