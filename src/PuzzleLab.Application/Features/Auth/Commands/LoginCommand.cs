using MediatR;
using PuzzleLab.Application.Common;

namespace PuzzleLab.Application.Features.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<Result<string>>;