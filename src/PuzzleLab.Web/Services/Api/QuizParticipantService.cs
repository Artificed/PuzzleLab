using System.Net.Http.Json;
using PuzzleLab.Shared.DTOs.QuizUser.Requests;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.Api;

public class QuizParticipantsService(IApiClient apiClient) : IQuizParticipantsService
{
    private const string BaseUrl = "api/quiz-participant";

    public async Task<GetQuizParticipantsResponse?> GetParticipantsByQuizIdAsync(Guid quizId)
    {
        var response = await apiClient.GetAsync<GetQuizParticipantsResponse>($"/api/quiz-participant/quiz/{quizId}");
        return response;
    }

    public async Task<CreateQuizParticipantResponse?> CreateParticipantAsync(CreateQuizParticipantRequest request)
    {
        var response =
            await apiClient.PostAsync<CreateQuizParticipantResponse>("/api/quiz-participant/create", request);
        return response;
    }

    public async Task<DeleteQuizParticipantResponse?> DeleteParticipantAsync(DeleteQuizParticipantRequest request)
    {
        var response =
            await apiClient.DeleteAsync<DeleteQuizParticipantResponse>("/api/quiz-participant/delete", request);
        return response;
    }
}