using System.ComponentModel.DataAnnotations;

namespace PuzzleLab.Domain.Entities;

public class User(string email, string username, string password, string role)
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Email { get; private set; } = email;
    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
    public string Role { get; private set; } = role;
}