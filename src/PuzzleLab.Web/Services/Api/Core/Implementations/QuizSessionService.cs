using PuzzleLab.Shared.DTOs.QuizSession.Requests;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;
using PuzzleLab.Web.Services.Api.Client;
using PuzzleLab.Web.Services.Api.Core.Interfaces;

namespace PuzzleLab.Web.Services.Api.Core.Implementations;

public class QuizSessionService(IApiClient apiClient) : IQuizSessionService
{
    public async Task<CreateOrGetQuizSessionResponse?> CreateOrGetQuizSessionAsync(
        CreateOrGetQuizSessionRequest createOrGetQuizSessionRequest)
    {
        var response = await apiClient.PostAsync<CreateOrGetQuizSessionResponse>(
            "/api/quiz-session/create-or-get", createOrGetQuizSessionRequest);
        return response;
    }

    public async Task<GetCurrentQuestionResponse?> GetCurrentQuestionAsync(
        GetCurrentQuestionRequest getCurrentQuestionRequest)
    {
        var response = await apiClient.GetAsync<GetCurrentQuestionResponse>(
            $"api/quiz-session/{getCurrentQuestionRequest.QuizId}/{getCurrentQuestionRequest.QuestionIndex}");
        return response;
    }

    public async Task<FinalizeQuizResponse?> FinalizeQuizAsync(
        FinalizeQuizRequest finalizeQuizRequest)
    {
        var response = await apiClient.PostAsync<FinalizeQuizResponse>(
            "/api/quiz-session/finalize", finalizeQuizRequest);
        return response;
    }
}