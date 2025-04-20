using PuzzleLab.Shared.DTOs.User.Requests;
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

    public async Task<CreateUserResponse?> CreateUserAsync(CreateUserRequest createUserRequest)
    {
        var user = await apiClient.PostAsync<CreateUserResponse>("/api/user/create", createUserRequest);
        return user;
    }
}