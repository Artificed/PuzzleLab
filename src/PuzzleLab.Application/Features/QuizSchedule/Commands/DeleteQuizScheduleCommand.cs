using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Commands;

public record DeleteQuizScheduleCommand(Guid QuizId) : IRequest<Result<DeleteQuizScheduleResponse>>;