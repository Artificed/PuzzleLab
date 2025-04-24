using PuzzleLab.Shared.DTOs.QuizSession.Requests;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IQuizSessionService
{
    Task<CreateOrGetQuizSessionResponse?> CreateOrGetQuizSessionAsync(
        CreateOrGetQuizSessionRequest createOrGetQuizSessionRequest);

    Task<GetCurrentQuestionResponse?> GetCurrentQuestionAsync(
        GetCurrentQuestionRequest getCurrentQuestionRequest);

    Task<FinalizeQuizResponse?> FinalizeQuizAsync(
        FinalizeQuizRequest finalizeQuizRequest);

    Task<GetUserQuizStatisticsResponse?> GetUserQuizStatisticsAsync(
        GetUserQuizStatisticsRequest getUserQuizStatisticsRequest);

    Task<GetQuizResultResponse?> GetQuizResultAsync(
        GetQuizResultRequest getQuizResultRequest);
}