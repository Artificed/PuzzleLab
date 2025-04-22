using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Application.Features.QuizSession.Commands;
using PuzzleLab.Shared.DTOs.QuizSession.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/quiz-session")]
public class QuizSessionController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpPost("create-or-get")]
    public async Task<IActionResult> CreateOrGetQuizSession([FromBody] CreateOrGetQuizSessionRequest request,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            var error = Error.Unauthorized("User is not logged in yet!");
            return this.MapErrorToAction(error);
        }

        var command = new CreateOrGetQuizSessionCommand(request.QuizId, Guid.Parse(userId));
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}