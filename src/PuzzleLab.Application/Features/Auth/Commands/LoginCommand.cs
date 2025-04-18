using MediatR;
using PuzzleLab.Application.Common;
using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Application.Features.Auth.Commands;

public record LoginCommand(string Email, string Password, string ConfirmPassword) : IRequest<Result<User>>;