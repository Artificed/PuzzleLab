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
    [HttpPost("create-or-get")]
    public async Task<IActionResult> CreateOrGetQuizSession([FromBody] CreateOrGetQuizSessionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrGetQuizSessionCommand(request.QuizId, request.UserId);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}