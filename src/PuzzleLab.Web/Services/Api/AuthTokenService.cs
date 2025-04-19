using Blazored.LocalStorage;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

using PuzzleLab.Web.Services.Api.Interfaces;
using System;
using System.Threading.Tasks;

public class InMemoryAuthTokenService : ITokenProvider
{
    private string? _currentToken = null;
    private readonly Guid _instanceId = Guid.NewGuid();

    public InMemoryAuthTokenService()
    {
    }

    public Task<string?> GetTokenAsync()
    {
        return Task.FromResult(_currentToken);
    }

    public Task SetTokenAsync(string token)
    {
        _currentToken = token;
        return Task.CompletedTask;
    }

    public Task ClearTokenAsync()
    {
        _currentToken = null;
        return Task.CompletedTask;
    }
}

// TODO: IMPLEMENT THIS ONE LATER
// public class AuthTokenService : ITokenProvider
// {
//     private readonly ILocalStorageService _localStorage;
//     private readonly string _tokenKey;
//
//     public AuthTokenService(ILocalStorageService localStorage, string tokenKey = "authorization-token")
//     {
//         _localStorage = localStorage;
//         _tokenKey = tokenKey;
//     }
//
//     public async Task<string?> GetTokenAsync()
//     {
//         try
//         {
//             if (await _localStorage.ContainKeyAsync(_tokenKey))
//             {
//                 return await _localStorage.GetItemAsync<string>(_tokenKey);
//             }
//
//             return null;
//         }
//         catch (Exception ex)
//         {
//             Console.Error.WriteLine($"Error retrieving token: {ex.Message}");
//             return null;
//         }
//     }
//
//     public async Task SetTokenAsync(string token)
//     {
//         try
//         {
//             await _localStorage.SetItemAsync(_tokenKey, token);
//         }
//         catch (Exception ex)
//         {
//             Console.Error.WriteLine($"Error setting token: {ex.Message}");
//         }
//     }
//
//     public async Task ClearTokenAsync()
//     {
//         try
//         {
//             await _localStorage.RemoveItemAsync(_tokenKey);
//         }
//         catch (Exception ex)
//         {
//             Console.Error.WriteLine($"Error removing token: {ex.Message}");
//         }
//     }
// }