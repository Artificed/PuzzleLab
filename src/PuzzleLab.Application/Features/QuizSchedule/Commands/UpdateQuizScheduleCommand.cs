using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Commands;

public record UpdateQuizScheduleCommand(Guid QuizId, Guid QuestionPackageId, DateTime StartTime, DateTime EndTime)
    : IRequest<Result<UpdateQuizScheduleResponse>>;