using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Queries;

public record GetAllQuizSchedulesQuery() : IRequest<Result<GetAllQuizSchedulesResponse>>;