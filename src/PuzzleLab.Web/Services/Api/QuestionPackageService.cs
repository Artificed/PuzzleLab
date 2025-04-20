using PuzzleLab.Shared.DTOs.QuestionPackage.Requests;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class QuestionPackageService(IApiClient apiClient) : IQuestionPackageService
{
    public async Task<GetAllQuestionPackagesResponse?> GetAllQuestionPackagesAsync()
    {
        var questionPackageDtos = await apiClient.GetAsync<GetAllQuestionPackagesResponse>("/api/question-package/all");
        return questionPackageDtos;
    }

    public async Task<CreateQuestionPackageResponse?> CreateQuestionPackageAsync(
        CreateQuestionPackageRequest createQuestionPackageRequest)
    {
        var questionPackage = await apiClient.PostAsync<CreateQuestionPackageResponse>("/api/question-package/create",
            createQuestionPackageRequest);
        return questionPackage;
    }

    public async Task<UpdateQuestionPackageResponse?> UpdateQuestionPackageAsync(
        UpdateQuestionPackageRequest updateQuestionPackageRequest)
    {
        var questionPackage = await apiClient.PutAsync<UpdateQuestionPackageResponse>("/api/question-package/update",
            updateQuestionPackageRequest);
        return questionPackage;
    }

    public async Task<DeleteQuestionPackageResponse?> DeleteQuestionPackageAsync(
        DeleteQuestionPackageRequest deleteQuestionPackageRequest)
    {
        var questionPackage =
            await apiClient.DeleteAsync<DeleteQuestionPackageResponse?>("/api/question-package/delete",
                deleteQuestionPackageRequest);
        return questionPackage;
    }
}