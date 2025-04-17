using System.ComponentModel.DataAnnotations;

namespace PuzzleLab.Domain.Entities;

public class User(string email, string username, string password)
{
    [Key]
    public int Id { get; private set; }
    public string Email { get; private set; } = email;
    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
}
