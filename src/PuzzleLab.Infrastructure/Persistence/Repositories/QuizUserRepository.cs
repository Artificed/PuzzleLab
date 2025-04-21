using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizUserRepository(DatabaseContext databaseContext) : IQuizUserRepository
{
    public async Task<List<QuizUser>> GetAllQuizUsersAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizUsers.Include(qu => qu.Quiz).ToListAsync(cancellationToken);
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

    public async Task<QuizUser?> GetByUserIdAndQuizIdAsync(Guid userId, Guid quizId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizUsers
            .FirstOrDefaultAsync(q => q.UserId == userId && q.QuizId == quizId, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid quizUserId, CancellationToken cancellationToken = default)
    {
        var quizUser = await databaseContext.QuizUsers.FindAsync([quizUserId], cancellationToken);
        if (quizUser != null)
        {
            databaseContext.QuizUsers.Remove(quizUser);
            await databaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}