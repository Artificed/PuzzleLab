using PuzzleLab.Shared.DTOs.Question.Requests;
using PuzzleLab.Shared.DTOs.Question.Responses;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class QuestionService(IApiClient apiClient) : IQuestionService
{
    public async Task<GetQuestionsByPackageResponse?> GetQuestionsByPackageIdAsync(
        GetQuestionsByPackageRequest request)
    {
        var questions =
            await apiClient.GetAsync<GetQuestionsByPackageResponse>($"/api/question/package/{request.PackageId}");
        return questions;
    }

    public async Task<CreateQuestionResponse?> CreateQuestionAsync(
        CreateQuestionRequest createQuestionRequest)
    {
        var question = await apiClient.PostAsync<CreateQuestionResponse>("/api/question/create",
            createQuestionRequest);
        return question;
    }

    public async Task<UpdateQuestionResponse?> UpdateQuestionAsync(
        UpdateQuestionRequest updateQuestionRequest)
    {
        var question = await apiClient.PutAsync<UpdateQuestionResponse>("/api/question/update",
            updateQuestionRequest);
        return question;
    }

    public async Task<DeleteQuestionResponse?> DeleteQuestionAsync(
        DeleteQuestionRequest deleteQuestionRequest)
    {
        var question =
            await apiClient.DeleteAsync<DeleteQuestionResponse?>("/api/question/delete",
                deleteQuestionRequest);
        return question;
    }
}