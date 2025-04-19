using PuzzleLab.Shared.DTOs.User.Responses;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class UserService(IApiClient apiClient) : IUserService
{
    public async Task<GetAllUsersResponse?> GetAllUsersAsync()
    {
        var userDtos = await apiClient.GetAsync<GetAllUsersResponse>("/api/user/all");
        return userDtos;
    }
}