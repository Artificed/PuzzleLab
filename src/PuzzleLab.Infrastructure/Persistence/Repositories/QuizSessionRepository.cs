using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizSessionRepository(DatabaseContext databaseContext) : IQuizSessionRepository
{
    public async Task<List<QuizSession>> GetAllQuizSessionsAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizSessions.ToListAsync(cancellationToken);
    }

    public async Task<QuizSession?> GetQuizSessionByIdAsync(Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizSessions.FirstOrDefaultAsync(x => x.Id == sessionId, cancellationToken);
    }

    public async Task InsertQuizSessionAsync(QuizSession session, CancellationToken cancellationToken = default)
    {
        await databaseContext.QuizSessions.AddAsync(session, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}