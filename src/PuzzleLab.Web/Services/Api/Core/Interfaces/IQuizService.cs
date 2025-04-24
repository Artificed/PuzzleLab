using PuzzleLab.Shared.DTOs.Quiz.Responses;

namespace PuzzleLab.Web.Services.Api.Core.Interfaces;

public interface IQuizService
{
    Task<GetQuizzesResponse?> GetQuizzesAsync();
}