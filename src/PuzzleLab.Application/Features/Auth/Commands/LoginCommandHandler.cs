using MediatR;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Application.Features.Auth.Commands;

public class LoginCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginCommand, User>
{
    public async Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmail(request.Username, cancellationToken);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        if (user.PasswordHash != request.Password) // fix later
        {
            throw new Exception("Invalid password");
        }

        return user;
    }
}
