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

    public async Task<UpdateUserResponse?> UpdateUserAsync(EditUserRequest updateUserRequest)
    {
        var user = await apiClient.PutAsync<UpdateUserResponse>("/api/user/update", updateUserRequest);
        return user;
    }

    public async Task<DeleteUserResponse?> DeleteUserAsync(DeleteUserRequest deleteUserRequest)
    {
        var user = await apiClient.DeleteAsync<DeleteUserResponse?>("/api/user/delete", deleteUserRequest);
        return user;
    }

    public async Task<GetAvailableUsersForQuizResponse?> GetAvailableUsersForQuizAsync(
        GetAvailableUsersForQuizRequest getAvailableUsersForQuizRequest)
    {
        var userDtos =
            await apiClient.GetAsync<GetAvailableUsersForQuizResponse>(
                $"/api/user/all/available-for-quiz/{getAvailableUsersForQuizRequest.Id}");
        return userDtos;
    }
}