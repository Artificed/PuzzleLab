using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllQuestionsAsync(CancellationToken cancellationToken = default);
    Task<List<Question>> GetQuestionsByPackageIdAsync(Guid packageId, CancellationToken cancellationToken = default);
    Task<Question?> GetQuestionByIdAsync(Guid questionId, CancellationToken cancellationToken = default);
    Task InsertQuestionAsync(Question question, CancellationToken cancellationToken = default);
    Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken = default);
    Task DeleteQuestionAsync(Question question, CancellationToken cancellationToken = default);
}