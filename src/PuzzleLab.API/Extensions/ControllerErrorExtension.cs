using Microsoft.AspNetCore.Mvc;
using PuzzleLab.Application.Common.Models;

namespace PuzzleLab.API.Extensions;

public static class ControllerErrorExtension
{
    public static IActionResult MapErrorToAction(this ControllerBase controller, Error error)
    {
        var details = new ProblemDetails
        {
            Title = error.Code,
            Detail = error.Message,
        };

        switch (error.Code)
        {
            case "NotFound":
                details.Status = StatusCodes.Status404NotFound;
                return controller.NotFound(details);
            case "Validation":
                details.Status = StatusCodes.Status400BadRequest;
                return controller.BadRequest(details);
            case "Unauthorized":
                details.Status = StatusCodes.Status401Unauthorized;
                return controller.Unauthorized(details);
            default:
                details.Status = StatusCodes.Status500InternalServerError;
                details.Title = "Internal Server Error";
                details.Detail = "An unexpected internal error occurred.";
                return controller.StatusCode(StatusCodes.Status500InternalServerError, details);
        }
    }
}