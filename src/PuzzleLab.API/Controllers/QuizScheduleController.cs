using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuizSchedule.Queries;

namespace PuzzleLab.API.Controllers;

public class QuizScheduleController(ISender sender) : ControllerBase
{
    [HttpGet("quiz-schedules")]
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
}