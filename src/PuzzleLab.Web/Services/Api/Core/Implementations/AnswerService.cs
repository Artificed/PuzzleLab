using PuzzleLab.Shared.DTOs.Answer.Requests;
using PuzzleLab.Shared.DTOs.Answer.Responses;
using PuzzleLab.Web.Services.Api.Core.Interfaces;
using PuzzleLab.Web.Services.Api.Client;

namespace PuzzleLab.Web.Services.Api.Core.Implementations;

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