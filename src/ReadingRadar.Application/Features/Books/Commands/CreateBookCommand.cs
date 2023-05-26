using MediatR;
using OneOf;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Books.Commands;

public record CreateBookCommand(
    string Title,
    string Author,
    int MediaType,
    string? Description,
    int? PageCount,
    string? ImageLink,
    DateTime? PublishDate,
    Guid? SeriesId
) : IRequest<OneOf<Book, ValidationError>>;
