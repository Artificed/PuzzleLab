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

    [HttpGet("all/available-for-quiz/{quizId}")]
    public async Task<IActionResult> GetAvailableUsersForQuiz(string quizId, CancellationToken cancellationToken)
    {
        var command = new GetAvailableUsersForQuizQuery(Guid.Parse(quizId));
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

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] EditUserRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(Guid.Parse(request.Id), request.Username, request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(Guid.Parse(request.Id));
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}