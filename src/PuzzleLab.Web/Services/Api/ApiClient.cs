using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.Web.Services.Api.Interfaces;
using PuzzleLab.Web.Services.Ui;

namespace PuzzleLab.Web.Services.Api;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private async Task ShowErrorFromResponse(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        try
        {
            var problem =
                await response.Content.ReadFromJsonAsync<ProblemDetails>(_jsonSerializerOptions, cancellationToken);
            if (problem != null)
            {
                ToastService.ShowError(problem.Detail ?? "An error occurred", problem.Title ?? "Error");
            }
            else
            {
                ToastService.ShowError("Unknown error occurred", "Error");
            }
        }
        catch (Exception e)
        {
            ToastService.ShowError($"Unable to parse error details: {e.Message}", "Error");
        }
    }

    public async Task<T?> GetAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.GetAsync(requestUrl, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                await ShowErrorFromResponse(response, cancellationToken);
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken);
        }
        catch (HttpRequestException e)
        {
            ToastService.ShowError(e.Message, "Request Error!");
            return default;
        }
        catch (JsonException e)
        {
            ToastService.ShowError(e.Message, "JSON deserialization failed");
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await httpClient.PostAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                await ShowErrorFromResponse(response, cancellationToken);
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken);
        }
        catch (JsonException e)
        {
            ToastService.ShowError(e.Message, "JSON deserialization failed");
            return default;
        }
    }

    public async Task PostAsync(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await httpClient.PostAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                await ShowErrorFromResponse(response, cancellationToken);
            }
        }
        catch (JsonException e)
        {
            ToastService.ShowError(e.Message, "JSON deserialization failed");
        }
    }

    public async Task<T?> PutAsync<T>(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                await ShowErrorFromResponse(response, cancellationToken);
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken);
        }
        catch (JsonException e)
        {
            ToastService.ShowError(e.Message, "JSON deserialization failed");
            return default;
        }
    }

    public async Task PutAsync(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync(requestUrl, data, _jsonSerializerOptions, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                await ShowErrorFromResponse(response, cancellationToken);
            }
        }
        catch (JsonException e)
        {
            ToastService.ShowError(e.Message, "JSON deserialization failed");
        }
    }

    public async Task DeleteAsync(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var json = JsonSerializer.Serialize(data, _jsonSerializerOptions);
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                await ShowErrorFromResponse(response, cancellationToken);
            }
        }
        catch (HttpRequestException e)
        {
            ToastService.ShowError(e.Message, "Request Error!");
        }
        catch (JsonException e)
        {
            ToastService.ShowError(e.Message, "JSON serialization failed");
        }
    }

    public async Task<T?> DeleteAsync<T>(string requestUrl, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            var json = JsonSerializer.Serialize(data, _jsonSerializerOptions);
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                await ShowErrorFromResponse(response, cancellationToken);
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>(_jsonSerializerOptions, cancellationToken);
        }
        catch (HttpRequestException e)
        {
            ToastService.ShowError(e.Message, "Request Error!");
            return default;
        }
        catch (JsonException e)
        {
            ToastService.ShowError(e.Message, "JSON deserialization failed");
            return default;
        }
    }
}