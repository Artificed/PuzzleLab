using PuzzleLab.Shared.DTOs.QuizSession.Requests;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IQuizSessionService
{
    Task<CreateQuizSessionResponse?> CreateQuizSessionAsync(
        CreateQuizSessionRequest createQuizSessionRequest);
}