using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizAnswer.Responses;

namespace PuzzleLab.Application.Features.QuizAnswer.Queries;

public record GetQuizAnswerBySessionQuery(Guid SessionId)
    : IRequest<Result<GetQuizAnswersBySessionResponse>>;