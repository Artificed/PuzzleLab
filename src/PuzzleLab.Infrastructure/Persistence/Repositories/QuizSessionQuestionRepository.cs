using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizSessionQuestionRepository(DatabaseContext databaseContext) : IQuizSessionQuestionRepository
{
    public async Task InsertQuizSessionQuestionAsync(QuizSessionQuestion quizSessionQuestion,
        CancellationToken cancellationToken = default)
    {
        await databaseContext.QuizSessionQuestions.AddAsync(quizSessionQuestion, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateQuizSessionQuestionAsync(QuizSessionQuestion quizSessionQuestion,
        CancellationToken cancellationToken = default)
    {
        databaseContext.QuizSessionQuestions.Update(quizSessionQuestion);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<QuizSessionQuestion>> GetQuizSessionQuestionsAsync(Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizSessionQuestions
            .Where(q => q.QuizSessionId == sessionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<QuizSessionQuestion?> GetQuizSessionQuestionByIdAsync(Guid quizSessionQuestionId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizSessionQuestions
            .FirstOrDefaultAsync(q => q.Id == quizSessionQuestionId, cancellationToken);
    }
}