using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Application.Features.Question.Commands;

public record UpdateQuestionCommand(Guid Id, string Text)
    : IRequest<Result<UpdateQuestionResponse>>;