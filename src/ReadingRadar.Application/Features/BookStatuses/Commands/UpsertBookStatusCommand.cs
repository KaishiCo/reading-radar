using MediatR;
using OneOf;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.BookStatuses.Commands;

public record UpsertBookStatusCommand(
    Guid BookId,
    int Status,
    int ChaptersCompleted,
    DateTime? CompletionDate
) : IRequest<OneOf<BookStatus, IError>>;
