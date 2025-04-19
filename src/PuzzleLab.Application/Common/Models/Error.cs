namespace PuzzleLab.Application.Common.Models;

public record Error(string Code, string Message)
{
    public static readonly Error None = new Error(string.Empty, string.Empty);

    public static Error NotFound(string message) => new Error("NotFound", message);
    public static Error Validation(string message) => new Error("Validation", message);
    public static Error Unauthorized(string message) => new Error("Unauthorized", message);
}