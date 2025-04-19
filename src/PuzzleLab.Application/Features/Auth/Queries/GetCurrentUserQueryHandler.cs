using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Responses;

namespace PuzzleLab.Application.Features.Auth.Queries;

public class GetCurrentUserQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetCurrentUserQuery, Result<GetCurrentUserResponse>>
{
    public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (currentUser is null)
        {
            return Result<GetCurrentUserResponse>.Failure(Error.NotFound("User not found!"));
        }

        return Result<GetCurrentUserResponse>.Success(new GetCurrentUserResponse(
            currentUser.Id,
            currentUser.Username,
            currentUser.Email,
            currentUser.Role
        ));
    }
}