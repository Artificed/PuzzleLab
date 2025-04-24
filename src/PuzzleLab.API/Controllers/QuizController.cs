using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.Answer.Commands;
using PuzzleLab.Application.Features.Answer.Queries;
using PuzzleLab.Application.Features.Quiz.Queries;
using PuzzleLab.Shared.DTOs.Answer.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/quiz")]
public class QuizController(ISender sender) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllQuizzes(CancellationToken cancellationToken)
    {
        var query = new GetQuizzesQuery();
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}