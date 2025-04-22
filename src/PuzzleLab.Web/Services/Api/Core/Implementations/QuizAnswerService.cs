using PuzzleLab.Shared.DTOs.QuizAnswer.Requests;
using PuzzleLab.Shared.DTOs.QuizAnswer.Responses;
using PuzzleLab.Web.Services.Api.Client;
using PuzzleLab.Web.Services.Api.Core.Interfaces;

namespace PuzzleLab.Web.Services.Api.Core.Implementations;

public class QuizAnswerService(IApiClient apiClient) : IQuizAnswerService
{
    public async Task<SaveQuizAnswerResponse?> SaveQuizAnswerAsync(SaveQuizAnswerRequest request)
    {
        var questions = await apiClient.PostAsync<SaveQuizAnswerResponse>("/api/quiz-answer/save", request);
        return questions;
    }

    public async Task<GetQuizAnswersBySessionResponse?> GetQuizAnswersBySessionIdAsync(
        GetQuizAnswersBySessionRequest request)
    {
        var questions =
            await apiClient.GetAsync<GetQuizAnswersBySessionResponse>($"/api/quiz-answer/{request.SessionId}");
        return questions;
    }
}