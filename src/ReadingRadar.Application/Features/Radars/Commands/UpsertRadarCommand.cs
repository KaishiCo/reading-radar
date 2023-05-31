using MediatR;
using OneOf;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Commands;

public record UpsertRadarCommand(
    Guid BookId,
    int Status,
    int ChaptersCompleted,
    DateTime? CompletionDate
) : IRequest<OneOf<Radar, IError>>;
