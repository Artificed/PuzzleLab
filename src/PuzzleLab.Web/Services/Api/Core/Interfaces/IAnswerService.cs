using PuzzleLab.Shared.DTOs.Answer.Requests;
using PuzzleLab.Shared.DTOs.Answer.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IAnswerService
{
    Task<GetAnswersByQuestionResponse?> GetAnswersByQuestionIdAsync(GetAnswersByQuestionRequest request);
    Task<SaveAnswersResponse?> SaveAnswersAsync(SaveAnswersRequest request);
}