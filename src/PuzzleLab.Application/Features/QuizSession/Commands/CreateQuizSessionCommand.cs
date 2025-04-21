using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Commands;

public record CreateQuizSessionCommand(Guid QuizId, Guid UserId)
    : IRequest<Result<CreateQuizSessionResponse>>;