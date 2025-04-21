using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;

namespace PuzzleLab.Application.Features.QuizParticipant.Commands;

public record DeleteQuizParticipantCommand(Guid QuizId, Guid UserId) : IRequest<Result<DeleteQuizParticipantResponse>>;