using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.Question.Commands;
using PuzzleLab.Application.Features.Question.Queries;
using PuzzleLab.Shared.DTOs.Question.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/question")]
public class QuestionController(ISender sender) : ControllerBase
{
    [HttpGet("package/{packageId}")]
    public async Task<IActionResult> GetQuestionsByPackageId(string packageId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(packageId, out var parsedPackageId))
        {
            return BadRequest("Invalid Package ID format.");
        }

        var query = new GetQuestionsByPackageId(parsedPackageId);
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateNewQuestion([FromBody] CreateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuestionCommand(
            request.QuestionPackageId,
            request.Text,
            request.ImageData,
            request.ImageMimeType);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteQuestion([FromBody] DeleteQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteQuestionCommand(request.Id);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateQuestionCommand(
            request.Id,
            request.Text,
            request.ImageData,
            request.ImageMimeType);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}