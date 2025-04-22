using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuizAnswer.Commands;
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
}