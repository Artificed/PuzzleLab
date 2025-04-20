using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.User.Commands;
using PuzzleLab.Application.Features.User.Queries;
using PuzzleLab.Shared.DTOs.Auth.Requests;
using PuzzleLab.Shared.DTOs.User.Requests;

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

    [HttpPost("create")]
    public async Task<IActionResult> CreateNewUser([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request.Username, request.Email, request.Password);

        var result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value); // TODO: Change to created
    }
}