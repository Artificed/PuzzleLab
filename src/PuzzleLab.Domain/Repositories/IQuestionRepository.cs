using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllQuestionsAsync(CancellationToken cancellationToken = default);
    Task<Question?> GetQuestionByIdAsync(Guid questionId, CancellationToken cancellationToken = default);
    Task InsertQuestionAsync(Question question, CancellationToken cancellationToken = default);
}