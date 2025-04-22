using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Commands;

public record FinalizeQuizCommand(Guid SessionId) : IRequest<Result<FinalizeQuizResponse>>;