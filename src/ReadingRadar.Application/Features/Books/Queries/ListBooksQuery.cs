using MediatR;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Queries;

public record ListBooksQuery : IRequest<IEnumerable<Book>>;
