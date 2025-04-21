using PuzzleLab.Shared.DTOs.User.Requests;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IUserService
{
    Task<GetAllUsersResponse?> GetAllUsersAsync();
    Task<CreateUserResponse?> CreateUserAsync(CreateUserRequest createUserRequest);
    Task<UpdateUserResponse?> UpdateUserAsync(EditUserRequest updateUserRequest);
    Task<DeleteUserResponse?> DeleteUserAsync(DeleteUserRequest deleteUserRequest);

    Task<GetAvailableUsersForQuizResponse?> GetAvailableUsersForQuizAsync(
        GetAvailableUsersForQuizRequest getAvailableUsersForQuizRequest);
}