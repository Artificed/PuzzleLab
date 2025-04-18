using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuizAnswerRepository
{
    Task InsertQuizAnswerAsync(QuizAnswer quizAnswer, CancellationToken cancellationToken = default);
    Task<List<QuizAnswer>> GetAllQuizAnswersAsync(CancellationToken cancellationToken = default);
    Task<QuizAnswer?> GetQuizAnswerById(Guid quizAnswerId, CancellationToken cancellationToken = default);
}