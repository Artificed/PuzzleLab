using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizAnswer.Responses;

namespace PuzzleLab.Application.Features.QuizAnswer.Commands;

public record SaveQuizAnswerCommand(Guid SessionId, Guid QuestionId, string Answer)
    : IRequest<Result<SaveQuizAnswerResponse>>;