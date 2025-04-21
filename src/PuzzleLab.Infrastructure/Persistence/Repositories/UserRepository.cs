using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.User;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class UserRepository(DatabaseContext databaseContext) : IUserRepository
{
    public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Users.ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await databaseContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await databaseContext.Users
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task InsertUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await databaseContext.Users.AddAsync(user, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        databaseContext.Users.Update(user);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(User user, CancellationToken cancellationToken = default)
    {
        databaseContext.Users.Remove(user);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<User>> GetAvailableUserForQuizAsync(Guid quizId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.Users
            .Where(user => user.Role == "User")
            .Where(user => !user.QuizUsers.Any(qu => qu.QuizId == quizId))
            .ToListAsync(cancellationToken);
    }
}