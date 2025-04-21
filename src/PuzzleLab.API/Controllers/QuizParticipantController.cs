using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuzzleLab.API.Extensions;
using PuzzleLab.Application.Features.QuizParticipant.Commands;
using PuzzleLab.Application.Features.QuizParticipant.Queries;
using PuzzleLab.Shared.DTOs.QuizUser.Requests;

namespace PuzzleLab.API.Controllers;

[ApiController]
[Route("api/quiz-participant")]
public class QuizParticipantController(ISender sender) : ControllerBase
{
    [HttpGet("/quiz/{quizId}")]
    public async Task<IActionResult> GetQuizParticipants(string quizId, CancellationToken cancellationToken)
    {
        var query = new GetQuizParticipantsQuery(Guid.Parse(quizId));
        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return this.MapErrorToAction(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateParticipant([FromBody] CreateQuizParticipantRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuizParticipantCommand(request.QuizId, request.UserId);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteParticipant([FromBody] DeleteQuizParticipantRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteQuizParticipantCommand(request.QuizId, request.UserId);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return this.MapErrorToAction(result.Error);
        }

        return Ok(result.Value);
    }
}