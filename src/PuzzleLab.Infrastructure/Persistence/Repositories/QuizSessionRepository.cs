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

    public async Task<QuizSession?> GetExistingQuizSessionAsync(Guid quizId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizSessions
            .Include(q => q.Quiz)
            .Include(q => q.User)
            .FirstOrDefaultAsync(x => x.QuizId == quizId && x.UserId == userId, cancellationToken);
    }

    public async Task<List<QuizSession>> GetQuizSessionsByQuizAsync(Guid quizId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizSessions
            .Where(q => q.QuizId == quizId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateQuizSessionAsync(QuizSession quizSession, CancellationToken cancellationToken = default)
    {
        databaseContext.QuizSessions.Update(quizSession);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<QuizSession>> GetQuizSessionsByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizSessions
            .Where(q => q.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}