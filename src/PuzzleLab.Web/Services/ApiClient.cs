using System.Text.Json;
using PuzzleLab.Web.Interfaces;

namespace PuzzleLab.Web.Services;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<T?> GetAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<T>(requestUrl, _jsonSerializerOptions, cancellationToken);
        }
        catch (HttpRequestException e)
        {
            Console.Error.WriteLine($"API GET request failed: {e.Message} | Status Code: {e.StatusCode}");
            return default;
        }
        catch (JsonException e)
        {
            Console.Error.WriteLine($"API GET JSON deserialization failed: {e.Message}");
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await httpClient.PostAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"API POST request failed: {ex.Message} | Status Code: {ex.StatusCode}");
            return default;
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"API POST JSON deserialization failed: {ex.Message}");
            return default;
        }
    }

    public async Task PostAsync(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await httpClient.PostAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"API POST request failed: {ex.Message} | Status Code: {ex.StatusCode}");
            throw;
        }
    }

    public async Task<T?> PutAsync<T>(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await httpClient.PutAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"API PUT request failed: {ex.Message} | Status Code: {ex.StatusCode}");
            return default;
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"API PUT JSON deserialization failed: {ex.Message}");
            return default;
        }
    }

    public async Task PutAsync(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await httpClient.PutAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"API PUT request failed: {ex.Message} | Status Code: {ex.StatusCode}");
            throw;
        }
    }

    public async Task DeleteAsync(string requestUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.DeleteAsync(requestUrl, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"API DELETE request failed: {ex.Message} | Status Code: {ex.StatusCode}");
            throw;
        }
    }
}