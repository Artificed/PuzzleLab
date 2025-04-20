using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.Answer.Commands;
using PuzzleLab.Application.Features.Answer.Queries;
using PuzzleLab.Shared.DTOs.Answer.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/answer")]
public class AnswerController(ISender sender) : ControllerBase
{
    [HttpGet("by-question/{questionId}")]
    public async Task<IActionResult> GetQuestionsByPackageId(string questionId, CancellationToken cancellationToken)
    {
        var query = new GetAnswersByQuestionQuery(Guid.Parse(questionId));
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveAnswers([FromBody] SaveAnswersRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SaveAnswersCommand(request.QuestionId, request.AnswerData);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}