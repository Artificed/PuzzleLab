using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuizSession.Commands;
using PuzzleLab.Shared.DTOs.QuizSession.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/quiz-session")]
public class QuizSessionController(ISender sender) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateQuizSession([FromBody] CreateQuizSessionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuizSessionCommand(request.QuizId, request.UserId);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}