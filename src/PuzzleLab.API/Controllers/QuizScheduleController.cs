using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuizSchedule.Commands;
using PuzzleLab.Application.Features.QuizSchedule.Queries;
using PuzzleLab.Shared.DTOs.QuizSchedule.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/quiz-schedule")]
public class QuizScheduleController(ISender sender) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllQuizSchedules(CancellationToken cancellationToken)
    {
        var query = new GetAllQuizSchedulesQuery();
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateQuizSchedule([FromBody] CreateQuizScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuizScheduleCommand(request.QuestionPackageId, request.StartTime, request.EndTime);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteQuizSchedule([FromBody] DeleteQuizScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteQuizScheduleCommand(request.QuizId);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateQuizSchedule([FromBody] UpdateQuizScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateQuizScheduleCommand(
            request.QuizId,
            request.QuestionPackageId,
            request.StartTime,
            request.EndTime);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserQuizSchedules(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserQuizScheduleQuery(Guid.Parse(userId));
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}