using PuzzleLab.Shared.DTOs.QuizSession.Requests;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;
using PuzzleLab.Web.Services.Api.Client;
using PuzzleLab.Web.Services.Api.Core.Interfaces;

namespace PuzzleLab.Web.Services.Api.Core.Implementations;

public class QuizSessionService(IApiClient apiClient) : IQuizSessionService
{
    public async Task<CreateQuizSessionResponse?> CreateQuizSessionAsync(
        CreateQuizSessionRequest createQuizSessionRequest)
    {
        var response = await apiClient.PostAsync<CreateQuizSessionResponse>(
            "/api/quiz-session/create", createQuizSessionRequest);
        return response;
    }
}