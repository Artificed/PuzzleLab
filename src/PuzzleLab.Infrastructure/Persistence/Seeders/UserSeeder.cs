using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class UserSeeder(
    IUserRepository userRepository,
    UserFactory userFactory)
{
    public async Task SeedUsersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Users...");

        if ((await userRepository.GetAllUsersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Users already seeded.");
            return;
        }

        var defaultPassword = BCrypt.Net.BCrypt.HashPassword("password");

        var admin = userFactory.CreateUser("admin", "admin@gmail.com", defaultPassword, "Admin");
        await userRepository.InsertUserAsync(admin, cancellationToken);

        var user1 = userFactory.CreateUser("user1", "user1@gmail.com", defaultPassword, "User");
        await userRepository.InsertUserAsync(user1, cancellationToken);

        var user2 = userFactory.CreateUser("user2", "user2@gmail.com", defaultPassword, "User");
        await userRepository.InsertUserAsync(user2, cancellationToken);

        var user3 = userFactory.CreateUser("user3", "user3@gmail.com", defaultPassword, "User");
        await userRepository.InsertUserAsync(user3, cancellationToken);
    }
}