using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizUser;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;

namespace PuzzleLab.Application.Features.QuizParticipant.Queries;

public class GetQuizParticipantsQueryHandler(
    IQuizUserRepository quizUserRepository,
    IUserRepository userRepository)
    : IRequestHandler<GetQuizParticipantsQuery, Result<GetQuizParticipantsResponse>>
{
    public async Task<Result<GetQuizParticipantsResponse>> Handle(GetQuizParticipantsQuery request,
        CancellationToken cancellationToken)
    {
        var allQuizUsers = await quizUserRepository.GetAllQuizUsersAsync(cancellationToken);
        var quizUsersForQuiz = allQuizUsers.Where(q => q.QuizId == request.QuizId).ToList();

        var participants = new List<QuizParticipantDto>();

        foreach (var quizUser in quizUsersForQuiz)
        {
            var user = await userRepository.GetUserByIdAsync(quizUser.UserId, cancellationToken);
            if (user != null)
            {
                participants.Add(new QuizParticipantDto(
                    quizUser.Id,
                    quizUser.QuizId,
                    quizUser.UserId,
                    user.Username
                ));
            }
        }

        return Result<GetQuizParticipantsResponse>.Success(new GetQuizParticipantsResponse(participants));
    }
}