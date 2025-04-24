using PuzzleLab.Shared.DTOs.Quiz.Responses;
using PuzzleLab.Web.Services.Api.Core.Interfaces;
using PuzzleLab.Web.Services.Api.Client;

namespace PuzzleLab.Web.Services.Api.Core.Implementations;

public class QuizService(IApiClient apiClient) : IQuizService
{
    public async Task<GetQuizzesResponse?> GetQuizzesAsync()
    {
        var response = await apiClient.GetAsync<GetQuizzesResponse>("/api/quiz/all");
        return response;
    }
}