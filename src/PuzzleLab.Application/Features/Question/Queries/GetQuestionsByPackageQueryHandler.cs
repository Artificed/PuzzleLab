using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Question;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Application.Features.Question.Queries;

public class GetQuestionsByPackageQueryHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionsByPackageId, Result<GetQuestionsByPackageResponse>>
{
    public async Task<Result<GetQuestionsByPackageResponse>> Handle(GetQuestionsByPackageId request,
        CancellationToken cancellationToken)
    {
        var questions = await questionRepository.GetQuestionsByPackageIdAsync(request.PackageId, cancellationToken);

        if (!questions.Any())
        {
            return Result<GetQuestionsByPackageResponse>.Failure(
                Error.NotFound($"No questions found for package ID: {request.PackageId}"));
        }

        var questionDtos = questions.Select(q => new QuestionDto(
            q.Id,
            q.QuestionPackageId,
            q.Text,
            q.ImageData,
            q.ImageMimeType,
            q.CreatedAt,
            q.LastModifiedAt
        )).ToList();

        return Result<GetQuestionsByPackageResponse>.Success(new GetQuestionsByPackageResponse(questionDtos));
    }
}