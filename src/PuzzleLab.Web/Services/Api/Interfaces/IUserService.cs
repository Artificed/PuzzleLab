using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Web.Services.Api.Interfaces;

public interface IUserService
{
    Task<GetAllUsersResponse?> GetAllUsersAsync();
}