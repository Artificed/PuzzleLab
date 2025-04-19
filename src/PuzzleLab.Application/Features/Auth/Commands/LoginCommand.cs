using MediatR;
using PuzzleLab.Application.Common;
using PuzzleLab.Shared.DTOs.Responses;

namespace PuzzleLab.Application.Features.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;