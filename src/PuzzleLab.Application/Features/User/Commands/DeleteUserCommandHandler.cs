using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Commands;

public class DeleteUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<DeleteUserCommand, Result<DeleteUserResponse>>
{
    public async Task<Result<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            return Result<DeleteUserResponse>.Failure(Error.NotFound("User not found"));
        }

        await userRepository.DeleteUserAsync(user);

        return Result<DeleteUserResponse>.Success(new DeleteUserResponse(user.Username));
    }
}