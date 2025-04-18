using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.DTOs.Requests;
using PuzzleLab.Application.Features.Auth.Commands;
using PuzzleLab.API.Extensions;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password, request.ConfirmPassword);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}