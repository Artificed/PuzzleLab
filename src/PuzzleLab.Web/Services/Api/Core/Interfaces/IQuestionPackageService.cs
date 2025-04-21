using PuzzleLab.Shared.DTOs.QuestionPackage.Requests;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IQuestionPackageService
{
    Task<GetAllQuestionPackagesResponse?> GetAllQuestionPackagesAsync();

    Task<GetQuestionPackageByIdResponse?> GetQuestionPackageByIdAsync(
        GetQuestionPackageByIdRequest getQuestionPackageByIdRequest);

    Task<CreateQuestionPackageResponse?> CreateQuestionPackageAsync(
        CreateQuestionPackageRequest createQuestionPackageRequest);

    Task<UpdateQuestionPackageResponse?> UpdateQuestionPackageAsync(
        UpdateQuestionPackageRequest updateQuestionPackageRequest);

    Task<DeleteQuestionPackageResponse?> DeleteQuestionPackageAsync(
        DeleteQuestionPackageRequest deleteQuestionPackageRequest);
}