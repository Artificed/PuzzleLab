using PuzzleLab.Shared.DTOs.Auth.Requests;
using PuzzleLab.Shared.DTOs.Auth.Responses;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class AuthService(IApiClient apiClient) : IAuthService
{
    public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
    {
        var token = await apiClient.PostAsync<LoginResponse>("/api/auth/login", loginRequest);
        return token;
    }

    public async Task<GetCurrentUserResponse?> GetCurrentUserAsync()
    {
        var user = await apiClient.GetAsync<GetCurrentUserResponse>("/api/auth/me");
        return user;
    }
}