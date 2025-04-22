using PuzzleLab.Shared.DTOs.QuizSchedule.Requests;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;
using PuzzleLab.Web.Services.Api.Core.Interfaces;
using PuzzleLab.Web.Services.Api.Client;

namespace PuzzleLab.Web.Services.Api.Core.Implementations;

public class QuizScheduleService(IApiClient apiClient) : IQuizScheduleService
{
    public async Task<GetAllQuizSchedulesResponse?> GetAllQuizSchedulesAsync()
    {
        var schedules = await apiClient.GetAsync<GetAllQuizSchedulesResponse>("/api/quiz-schedule/all");
        return schedules;
    }

    public async Task<CreateQuizScheduleResponse?> CreateQuizScheduleAsync(
        CreateQuizScheduleRequest createQuizScheduleRequest)
    {
        var schedule = await apiClient.PostAsync<CreateQuizScheduleResponse>(
            "/api/quiz-schedule/create",
            createQuizScheduleRequest);
        return schedule;
    }

    public async Task<UpdateQuizScheduleResponse?> UpdateQuizScheduleAsync(
        UpdateQuizScheduleRequest updateQuizScheduleRequest)
    {
        var schedule = await apiClient.PutAsync<UpdateQuizScheduleResponse>(
            "/api/quiz-schedule/update",
            updateQuizScheduleRequest);
        return schedule;
    }

    public async Task<DeleteQuizScheduleResponse?> DeleteQuizScheduleAsync(
        DeleteQuizScheduleRequest deleteQuizScheduleRequest)
    {
        var response = await apiClient.DeleteAsync<DeleteQuizScheduleResponse>(
            "/api/quiz-schedule/delete",
            deleteQuizScheduleRequest);
        return response;
    }

    public async Task<GetUserQuizScheduleResponse?> GetUserQuizScheduleAsync()
    {
        var schedules =
            await apiClient.GetAsync<GetUserQuizScheduleResponse>(
                $"/api/quiz-schedule/user");
        return schedules;
    }
}