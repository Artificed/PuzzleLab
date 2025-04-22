using PuzzleLab.Shared.DTOs.QuizAnswer.Requests;
using PuzzleLab.Shared.DTOs.QuizAnswer.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IQuizAnswerService
{
    Task<SaveQuizAnswerResponse?> SaveQuizAnswerAsync(SaveQuizAnswerRequest saveQuizAnswerRequest);
}