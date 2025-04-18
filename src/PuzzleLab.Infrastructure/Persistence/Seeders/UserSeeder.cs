using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class UserSeeder(DatabaseContext databaseContext)
{
    private readonly UserRepository _userRepository = new(databaseContext);
    private readonly UserFactory _userFactory = new();

    public async Task SeedUsersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Question Packages...");

        if ((await _userRepository.GetAllUsersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Users already seeded.");
            return;
        }

        var defaultPassword = BCrypt.Net.BCrypt.HashPassword("password");

        var admin = _userFactory.CreateUser("admin", "admin@gmail.com", defaultPassword, "Admin");
        await _userRepository.InsertUserAsync(admin, cancellationToken);

        var user1 = _userFactory.CreateUser("user1", "user1@gmail.com", defaultPassword, "User");
        await _userRepository.InsertUserAsync(user1, cancellationToken);

        var user2 = _userFactory.CreateUser("user2", "user2@gmail.com", defaultPassword, "User");
        await _userRepository.InsertUserAsync(user2, cancellationToken);

        var user3 = _userFactory.CreateUser("user3", "user3@gmail.com", defaultPassword, "User");
        await _userRepository.InsertUserAsync(user3, cancellationToken);
    }
}