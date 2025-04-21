using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Queries;

public record GetUserQuizScheduleQuery(Guid UserId) : IRequest<Result<GetUserQuizScheduleResponse>>;