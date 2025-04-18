using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IAnswerRepository
{
    Task<List<Answer>> GetAllAnswersAsync(CancellationToken cancellationToken = default);
    Task<Answer?> GetAnswerById(Guid answerId, CancellationToken cancellationToken = default);
    Task InsertAnswerAsync(Answer answer, CancellationToken cancellationToken = default);
}