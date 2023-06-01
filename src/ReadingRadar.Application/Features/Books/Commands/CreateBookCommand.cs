using MediatR;
using OneOf;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Commands;

public record CreateBookCommand(
    string Title,
    string Author,
    string Language,
    int MediaType,
    string? Description,
    int? Pages,
    int? Chapters,
    string? ImageLink,
    DateTime? PublishDate,
    Guid? SeriesId
) : IRequest<OneOf<Book, IError>>;
