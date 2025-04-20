using PuzzleLab.Shared.DTOs.Question.Requests;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Web.Services.Api.Interfaces;

public interface IQuestionService
{
    Task<GetQuestionsByPackageResponse?> GetQuestionsByPackageIdAsync(
        GetQuestionsByPackageRequest getQuestionsByPackageRequest);

    Task<CreateQuestionResponse?> CreateQuestionAsync(
        CreateQuestionRequest createQuestionRequest);

    Task<UpdateQuestionResponse?> UpdateQuestionAsync(
        UpdateQuestionRequest updateQuestionRequest);

    Task<DeleteQuestionResponse?> DeleteQuestionAsync(
        DeleteQuestionRequest deleteQuestionRequest);
}