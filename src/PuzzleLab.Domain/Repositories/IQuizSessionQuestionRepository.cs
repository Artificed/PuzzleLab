using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuizSessionQuestionRepository
{
    Task InsertQuizSessionQuestionAsync(QuizSessionQuestion quizSessionQuestion,
        CancellationToken cancellationToken = default);

    Task UpdateQuizSessionQuestionAsync(QuizSessionQuestion quizSessionQuestion,
        CancellationToken cancellationToken = default);

    Task<List<QuizSessionQuestion>> GetQuizSessionQuestionsAsync(Guid sessionId,
        CancellationToken cancellationToken = default);

    Task<QuizSessionQuestion?> GetQuizSessionQuestionByIdAsync(Guid quizSessionQuestionId,
        CancellationToken cancellationToken = default);

    Task<QuizSessionQuestion?> GetQuizSessionQuestionByIdAsync(Guid sessionId, int questionOrder,
        CancellationToken cancellationToken = default);
}