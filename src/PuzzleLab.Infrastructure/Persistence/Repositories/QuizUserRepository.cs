using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizUserRepository(DatabaseContext databaseContext) : IQuizUserRepository
{
    public async Task<List<QuizUser>> GetAllQuizUsersAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizUsers.ToListAsync(cancellationToken);
    }

    public async Task<QuizUser?> GetQuizUserByIdAsync(Guid quizUserId, CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizUsers.FirstOrDefaultAsync(x => x.Id == quizUserId, cancellationToken);
    }

    public async Task InsertQuizUserAsync(QuizUser quizUser, CancellationToken cancellationToken = default)
    {
        await databaseContext.QuizUsers.AddAsync(quizUser, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}