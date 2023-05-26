using MediatR;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Books.Queries;

public record ListBooksQuery : IRequest<IEnumerable<Book>>;
