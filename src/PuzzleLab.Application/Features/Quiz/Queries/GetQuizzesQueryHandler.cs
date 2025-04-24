using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Quiz;
using PuzzleLab.Shared.DTOs.Quiz.Responses;

namespace PuzzleLab.Application.Features.Quiz.Queries;

public class GetQuizzesQueryHandler(
    IQuizRepository quizRepository
)
    : IRequestHandler<GetQuizzesQuery, Result<GetQuizzesResponse>>
{
    public async Task<Result<GetQuizzesResponse>> Handle(GetQuizzesQuery request,
        CancellationToken cancellationToken)
    {
        var allQuizzes = await quizRepository.GetAllQuizzesAsync(cancellationToken);

        var quizDtos = allQuizzes.Select(q => new QuizDto()
        {
            QuizId = q.Id,
            QuizName = q.QuestionPackage.Name
        }).ToList();

        var responseData = new GetQuizzesResponse(quizDtos);

        return Result<GetQuizzesResponse>.Success(new GetQuizzesResponse(quizDtos));
    }
}