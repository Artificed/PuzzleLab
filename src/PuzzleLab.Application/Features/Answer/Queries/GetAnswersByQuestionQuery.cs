using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Answer.Responses;

namespace PuzzleLab.Application.Features.Answer.Queries;

public record GetAnswersByQuestionQuery(Guid QuestionId) : IRequest<Result<GetAnswersByQuestionResponse>>;