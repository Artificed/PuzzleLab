using Microsoft.AspNetCore.Http.HttpResults;
using PuzzleLab.Shared.DTOs.Auth.Requests;
using PuzzleLab.Shared.DTOs.Auth.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest loginRequest);
    Task<GetCurrentUserResponse?> GetCurrentUserAsync();
}