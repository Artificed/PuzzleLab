using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Answer;
using PuzzleLab.Shared.DTOs.Answer.Responses;

namespace PuzzleLab.Application.Features.Answer.Commands;

public record SaveAnswersCommand(string QuestionId, List<CreateAnswerDto> AnswerData)
    : IRequest<Result<SaveAnswersResponse>>;