using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizAnswer;
using PuzzleLab.Shared.DTOs.QuizAnswer.Responses;

namespace PuzzleLab.Application.Features.QuizAnswer.Commands;

public class SaveQuizAnswerCommandHandler(
    IQuizSessionRepository quizSessionRepository,
    IQuizSessionQuestionRepository quizSessionQuestionRepository,
    IAnswerRepository answerRepository,
    IQuizAnswerRepository quizAnswerRepository,
    QuizAnswerFactory quizAnswerFactory
) : IRequestHandler<SaveQuizAnswerCommand, Result<SaveQuizAnswerResponse>>
{
    public async Task<Result<SaveQuizAnswerResponse>> Handle(
        SaveQuizAnswerCommand request,
        CancellationToken cancellationToken
    )
    {
        var quizSession = await quizSessionRepository.GetQuizSessionByIdAsync(request.SessionId, cancellationToken);
        if (quizSession is null)
            return Result<SaveQuizAnswerResponse>.Failure(Error.NotFound("Quiz session not found!"));

        var quizSessionQuestion = await quizSessionQuestionRepository.GetQuizSessionQuestionBySessionAndQuestionIdAsync(
            request.SessionId, request.QuestionId, cancellationToken);
        if (quizSessionQuestion is null)
            return Result<SaveQuizAnswerResponse>.Failure(Error.NotFound("Quiz session question not found!"));

        var correctAnswer = await answerRepository.GetQuestionAnswer(request.QuestionId, cancellationToken);
        if (correctAnswer is null)
            return Result<SaveQuizAnswerResponse>.Failure(Error.NotFound("Correct answer not found!"));

        bool isCorrect = request.SelectedAnswerId == correctAnswer.Id;

        var existing = await quizAnswerRepository
            .GetBySessionAndQuestionAsync(quizSession.Id, request.QuestionId, cancellationToken);

        if (existing is not null)
        {
            existing.UpdateSelectedAnswer(correctAnswer.Id, isCorrect);
            await quizAnswerRepository.UpdateQuizAnswerAsync(existing, cancellationToken);
        }
        else
        {
            var newAnswer = quizAnswerFactory.CreateQuizAnswer(quizSession.Id, quizSessionQuestion.QuestionId,
                correctAnswer.Id, isCorrect);
            await quizAnswerRepository.InsertQuizAnswerAsync(newAnswer, cancellationToken);
        }

        var responseData = new QuizAnswerDto
        {
            SessionId = quizSession.Id,
            QuestionId = quizSessionQuestion.QuestionId,
            SelectedAnswerId = correctAnswer.Id
        };

        return Result<SaveQuizAnswerResponse>.Success(new SaveQuizAnswerResponse(responseData));
    }
}