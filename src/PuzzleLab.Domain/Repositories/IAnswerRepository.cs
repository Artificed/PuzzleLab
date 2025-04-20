using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IAnswerRepository
{
    Task<List<Answer>> GetAllAnswersAsync(CancellationToken cancellationToken = default);
    Task ClearAnswersByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken = default);
    Task<List<Answer>> GetAnswersByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken = default);
    Task InsertAnswerAsync(Answer answer, CancellationToken cancellationToken = default);
}