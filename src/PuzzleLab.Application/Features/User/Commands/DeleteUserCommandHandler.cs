using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Application.Features.User.Commands;

public class DeleteUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            return Result.Failure(Error.NotFound("User not found"));
        }

        await userRepository.DeleteUserAsync(user);

        return Result.Success();
    }
}