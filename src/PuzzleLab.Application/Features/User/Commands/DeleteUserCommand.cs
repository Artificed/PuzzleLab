using MediatR;
using PuzzleLab.Application.Common.Models;

namespace PuzzleLab.Application.Features.User.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<Result>;