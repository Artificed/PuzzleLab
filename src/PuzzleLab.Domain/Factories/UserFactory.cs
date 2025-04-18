using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class UserFactory
{
    public User CreateUser(string username, string email, string password, string userRole)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        return new User(
            Guid.NewGuid(),
            username,
            email,
            hashedPassword,
            userRole
        );
    }
}