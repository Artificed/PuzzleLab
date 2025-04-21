using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;

namespace PuzzleLab.Application.Features.QuizParticipant.Commands;

public record CreateQuizParticipantCommand(Guid QuizId, Guid UserId) : IRequest<Result<CreateQuizParticipantResponse>>;