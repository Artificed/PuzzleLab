using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Quiz.Responses;

namespace PuzzleLab.Application.Features.Quiz.Queries;

public class GetQuizzesQuery() : IRequest<Result<GetQuizzesResponse>>;