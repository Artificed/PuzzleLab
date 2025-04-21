using PuzzleLab.Shared.DTOs.QuizSchedule.Requests;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IQuizScheduleService
{
    Task<GetAllQuizSchedulesResponse?> GetAllQuizSchedulesAsync();

    Task<CreateQuizScheduleResponse?> CreateQuizScheduleAsync(
        CreateQuizScheduleRequest createQuizScheduleRequest);

    Task<UpdateQuizScheduleResponse?> UpdateQuizScheduleAsync(
        UpdateQuizScheduleRequest updateQuizScheduleRequest);

    Task<DeleteQuizScheduleResponse?> DeleteQuizScheduleAsync(
        DeleteQuizScheduleRequest deleteQuestionRequest);

    Task<GetUserQuizScheduleResponse?> GetUserQuizScheduleAsync(
        GetUserQuizScheduleRequest getUserQuizScheduleRequest);
}