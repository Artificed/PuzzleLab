using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizAnswerRepository : IQuizAnswerRepository
{
    private readonly DatabaseContext _db;

    public QuizAnswerRepository(DatabaseContext databaseContext)
        => _db = databaseContext;

    public async Task InsertQuizAnswerAsync(QuizAnswer quizAnswer, CancellationToken cancellationToken = default)
    {
        await _db.QuizAnswers.AddAsync(quizAnswer, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateQuizAnswerAsync(QuizAnswer quizAnswer, CancellationToken cancellationToken = default)
    {
        _db.QuizAnswers.Update(quizAnswer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<QuizAnswer?> GetBySessionAndQuestionAsync(
        Guid sessionId,
        Guid questionId,
        CancellationToken cancellationToken = default)
    {
        return await _db.QuizAnswers
            .FirstOrDefaultAsync(x =>
                    x.QuizSessionId == sessionId &&
                    x.QuestionId == questionId,
                cancellationToken);
    }

    public async Task<List<QuizAnswer>> GetAllQuizAnswersAsync(CancellationToken cancellationToken = default)
    {
        return await _db.QuizAnswers.ToListAsync(cancellationToken);
    }

    public async Task<QuizAnswer?> GetQuizAnswerByIdAsync(
        Guid quizAnswerId,
        CancellationToken cancellationToken = default)
    {
        return await _db.QuizAnswers
            .FirstOrDefaultAsync(x => x.Id == quizAnswerId, cancellationToken);
    }
}