using System.Runtime.InteropServices.JavaScript;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.Application.Features.Auth.Commands;
using PuzzleLab.API.Extensions;
using PuzzleLab.Shared.DTOs.Requests;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Application.Features.Auth.Queries;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            var error = Error.Unauthorized("User is not logged in yet!");
            return this.MapErrorToAction(error);
        }

        var command = new GetCurrentUserQuery(Guid.Parse(userId));
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}