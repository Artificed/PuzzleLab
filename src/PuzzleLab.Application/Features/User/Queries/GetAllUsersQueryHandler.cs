using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.User;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Queries;

public class GetCurrentUserQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUsersQuery, Result<GetAllUsersResponse>>
{
    public async Task<Result<GetAllUsersResponse>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllUsersAsync(cancellationToken);

        var filteredUsers = users.Where(user => user.Role == "User").ToList();

        var userDtos = filteredUsers.Select(user => new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.CreatedAt,
            user.LastLoginAt
        )).ToList();

        return Result<GetAllUsersResponse>.Success(new GetAllUsersResponse(userDtos));
    }
}