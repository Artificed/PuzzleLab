using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Answer;
using PuzzleLab.Shared.DTOs.Answer.Responses;

namespace PuzzleLab.Application.Features.Answer.Commands;

public class SaveAnswersCommandHandler(IAnswerRepository answerRepository)
    : IRequestHandler<SaveAnswersCommand, Result<SaveAnswersResponse>>
{
    private readonly AnswerFactory _answerFactory = new();

    public async Task<Result<SaveAnswersResponse>> Handle(SaveAnswersCommand request,
        CancellationToken cancellationToken)
    {
        if (request.AnswerData.Count(a => a.IsCorrect) != 1)
        {
            return Result<SaveAnswersResponse>.Failure(new Error("Bad Request",
                "There must be exactly one correct answer provided."));
        }

        await answerRepository.ClearAnswersByQuestionIdAsync(Guid.Parse(request.QuestionId), cancellationToken);

        foreach (var answer in request.AnswerData)
        {
            var newAnswer = _answerFactory.CreateAnswer(Guid.Parse(request.QuestionId), answer.Text, answer.IsCorrect);
            await answerRepository.InsertAnswerAsync(newAnswer, cancellationToken);
        }

        var savedAnswers = await answerRepository.GetAnswersByQuestionIdAsync(Guid.Parse(request.QuestionId),
            cancellationToken);

        var savedAnswersDto = savedAnswers.Select(a => new AnswerDto()
        {
            Id = a.Id,
            QuestionId = a.QuestionId,
            Text = a.Text,
            IsCorrect = a.IsCorrect
        }).ToList();

        return Result<SaveAnswersResponse>.Success(new SaveAnswersResponse() { SavedAnswers = savedAnswersDto });
    }
}