using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuizAnswerRepository
{
    Task InsertQuizAnswerAsync(QuizAnswer quizAnswer, CancellationToken cancellationToken = default);
    Task UpdateQuizAnswerAsync(QuizAnswer quizAnswer, CancellationToken cancellationToken = default);

    Task<QuizAnswer?> GetBySessionAndQuestionAsync(Guid sessionId, Guid questionId,
        CancellationToken cancellationToken = default);

    Task<List<QuizAnswer>> GetAllQuizAnswersAsync(CancellationToken cancellationToken = default);
    Task<QuizAnswer?> GetQuizAnswerByIdAsync(Guid quizAnswerId, CancellationToken cancellationToken = default);
}