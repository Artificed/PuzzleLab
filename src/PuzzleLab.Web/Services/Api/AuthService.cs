using PuzzleLab.Shared.DTOs.Requests;
using PuzzleLab.Shared.DTOs.Responses;
using PuzzleLab.Web.Interfaces;
using PuzzleLab.Web.Services.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class AuthService(IApiClient apiClient) : IAuthService
{
    public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
    {
        var token = await apiClient.PostAsync<LoginResponse>("/api/auth/login", loginRequest);

        Console.WriteLine(token);

        return token;
    }
}