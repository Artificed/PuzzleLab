namespace PuzzleLab.Shared.DTOs.User;

public record UserDto(Guid Id, string Username, string Email, DateTime CreatedAt, DateTime? LastLoginAt);