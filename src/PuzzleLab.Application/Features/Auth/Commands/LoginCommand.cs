using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Auth.Responses;

namespace PuzzleLab.Application.Features.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;