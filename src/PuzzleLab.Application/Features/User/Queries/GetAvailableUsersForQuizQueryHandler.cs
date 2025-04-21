using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.User;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Queries;

public class GetAvailableUsersForQuizQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetAvailableUsersForQuizQuery, Result<GetAvailableUsersForQuizResponse>>
{
    public async Task<Result<GetAvailableUsersForQuizResponse>> Handle(GetAvailableUsersForQuizQuery request,
        CancellationToken cancellationToken)
    {
        var availableUsers = await userRepository.GetAvailableUserForQuizAsync(request.QuizId, cancellationToken);

        var userDtos = availableUsers.Select(user => new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.CreatedAt,
            user.LastLoginAt
        )).ToList();

        return Result<GetAvailableUsersForQuizResponse>.Success(new GetAvailableUsersForQuizResponse(userDtos));
    }
}