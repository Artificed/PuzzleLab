using PuzzleLab.Shared.DTOs.Answer.Requests;
using PuzzleLab.Shared.DTOs.Answer.Responses;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class AnswerService(IApiClient apiClient) : IAnswerService
{
    public async Task<GetAnswersByQuestionResponse?> GetAnswersByQuestionIdAsync(GetAnswersByQuestionRequest request)
    {
        var response =
            await apiClient.GetAsync<GetAnswersByQuestionResponse?>($"/api/answer/by-question/{request.QuestionId}");
        return response;
    }

    public async Task<SaveAnswersResponse?> SaveAnswersAsync(SaveAnswersRequest request)
    {
        var response =
            await apiClient.PostAsync<SaveAnswersResponse?>($"/api/answer/save", request);
        return response;
    }
}