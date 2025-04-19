using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.Application.Features.Auth.Commands;
using PuzzleLab.API.Extensions;
using PuzzleLab.Shared.DTOs.Requests;

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
}