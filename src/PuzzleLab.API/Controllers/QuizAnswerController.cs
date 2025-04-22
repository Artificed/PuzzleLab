using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuizAnswer.Commands;
using PuzzleLab.Application.Features.QuizAnswer.Queries;
using PuzzleLab.Shared.DTOs.QuizAnswer.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/quiz-answer")]
public class QuizAnswerController(ISender sender) : ControllerBase
{
    [HttpPost("save")]
    public async Task<IActionResult> SaveQuizAnswer([FromBody] SaveQuizAnswerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SaveQuizAnswerCommand(request.SessionId, request.QuestionId, request.Answer);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetQuizAnswerBySession(string sessionId, CancellationToken cancellationToken)
    {
        var query = new GetQuizAnswerBySessionQuery(Guid.Parse(sessionId));
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}