namespace PuzzleLab.Shared.DTOs.Auth.Responses;

public record GetCurrentUserResponse(Guid Id, string Username, string Email, string Role);