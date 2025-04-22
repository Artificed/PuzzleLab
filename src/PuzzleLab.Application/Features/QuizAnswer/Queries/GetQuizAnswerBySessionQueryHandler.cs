using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizAnswer;
using PuzzleLab.Shared.DTOs.QuizAnswer.Responses;

namespace PuzzleLab.Application.Features.QuizAnswer.Queries;

public class GetQuizAnswerBySessionQueryHandler
    : IRequestHandler<GetQuizAnswerBySessionQuery, Result<GetQuizAnswersBySessionResponse>>
{
    private readonly IQuizAnswerRepository _quizAnswerRepository;

    public GetQuizAnswerBySessionQueryHandler(IQuizAnswerRepository quizAnswerRepository)
    {
        _quizAnswerRepository = quizAnswerRepository;
    }

    public async Task<Result<GetQuizAnswersBySessionResponse>> Handle(
        GetQuizAnswerBySessionQuery request,
        CancellationToken cancellationToken)
    {
        var allAnswers = await _quizAnswerRepository.GetAllQuizAnswersAsync(cancellationToken);

        var sessionAnswers = allAnswers.Where(x => x.QuizSessionId == request.SessionId).ToList();

        var sessionAnswerDtos = sessionAnswers.Select(sa => new QuizAnswerDto()
        {
            SessionId = sa.QuizSessionId,
            QuestionId = sa.QuestionId,
            SelectedAnswerId = sa.SelectedAnswerId
        }).ToList();

        var response = new GetQuizAnswersBySessionResponse(sessionAnswerDtos);

        return Result<GetQuizAnswersBySessionResponse>.Success(response);
    }
}