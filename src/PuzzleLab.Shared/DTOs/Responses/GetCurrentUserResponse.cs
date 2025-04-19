namespace PuzzleLab.Shared.DTOs.Responses;

public record GetCurrentUserResponse(Guid Id, string Username, string Email, string Role);