namespace PuzzleLab.Shared.DTOs.User.Requests;

public class EditUserRequest
{
    public string Id { get; init; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}