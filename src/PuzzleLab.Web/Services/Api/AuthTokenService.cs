using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class AuthTokenService(ProtectedSessionStorage sessionStorage, string tokenKey) : ITokenProvider
{
    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var result = await sessionStorage.GetAsync<string>(tokenKey);
            return result.Success ? result.Value : null;
        }
        catch (InvalidOperationException e)
        {
            Console.Error.WriteLine($"Error accessing protected session storage: {e.Message}");
            return null;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Unexpected error accessing protected session storage: {e.Message}");
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        try
        {
            await sessionStorage.SetAsync(tokenKey, token);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error setting token in protected session storage: {e.Message}");
        }
    }
}