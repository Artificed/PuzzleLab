using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuestionPackage.Commands;
using PuzzleLab.Application.Features.QuestionPackage.Queries;
using PuzzleLab.Shared.DTOs.QuestionPackage.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/question-package")]
public class QuestionPackageController(ISender sender) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllQuestionPackages(CancellationToken cancellationToken)
    {
        var command = new GetAllQuestionPackagesQuery();
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{packageId}")]
    public async Task<IActionResult> GetQuestionPackageById(string packageId, CancellationToken cancellationToken)
    {
        var query = new GetQuestionPackageByIdQuery(Guid.Parse(packageId));
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateQuestionPackage(
        [FromBody] CreateQuestionPackageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuestionPackageCommand(request.Name, request.Description);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateQuestionPackage(
        [FromBody] UpdateQuestionPackageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateQuestionPackageCommand(Guid.Parse(request.Id), request.Name, request.Description);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteQuestionPackage(
        [FromBody] DeleteQuestionPackageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteQuestionPackageCommand(Guid.Parse(request.Id));
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}