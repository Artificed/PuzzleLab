using System.ComponentModel.DataAnnotations;

namespace PuzzleLab.Domain.Entities;

public class User
{
    [Key] public Guid Id { get; private set; }

    [Required] public string Username { get; private set; }

    [Required] public string Email { get; private set; }

    [Required] public string PasswordHash { get; private set; }

    [Required] public string Role { get; private set; }

    [Required] public DateTime CreatedAt { get; private set; }

    public DateTime? LastLoginAt { get; private set; }

    public virtual ICollection<QuizUser> QuizUsers { get; private set; }
    public virtual ICollection<QuizSession> QuizSessions { get; private set; }

    internal User(Guid id, string username, string email, string passwordHash, string role)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow.AddHours(7);
        QuizUsers = new List<QuizUser>();
        QuizSessions = new List<QuizSession>();
    }

    public void UpdateUsername(string username)
    {
        Username = username;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow.AddHours(7);
    }
}