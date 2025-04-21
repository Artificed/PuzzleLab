using PuzzleLab.Shared.DTOs.QuizUser.Requests;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;

namespace PuzzleLab.Web.Services.Api.Interfaces;

public interface IQuizParticipantsService
{
    Task<GetQuizParticipantsResponse?> GetParticipantsByQuizIdAsync(Guid quizId);
    Task<CreateQuizParticipantResponse?> CreateParticipantAsync(CreateQuizParticipantRequest request);
    Task<DeleteQuizParticipantResponse?> DeleteParticipantAsync(DeleteQuizParticipantRequest request);
}