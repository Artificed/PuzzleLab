using Microsoft.AspNetCore.Http.HttpResults;
using PuzzleLab.Shared.DTOs.Requests;
using PuzzleLab.Shared.DTOs.Responses;

namespace PuzzleLab.Web.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest loginRequest);
}