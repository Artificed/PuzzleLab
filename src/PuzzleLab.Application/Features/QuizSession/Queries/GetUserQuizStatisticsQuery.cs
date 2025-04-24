using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Queries;

public record GetUserQuizStatisticsQuery(Guid UserId)
    : IRequest<Result<GetUserQuizStatisticsResponse>>;