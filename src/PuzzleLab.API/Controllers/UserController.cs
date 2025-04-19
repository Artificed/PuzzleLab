using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.User.Queries;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(ISender sender) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var command = new GetAllUsersQuery();
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}