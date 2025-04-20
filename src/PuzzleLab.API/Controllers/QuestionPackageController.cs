using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuestionPackage.Queries;

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
}