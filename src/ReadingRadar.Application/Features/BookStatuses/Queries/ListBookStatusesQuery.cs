using MediatR;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.BookStatuses.Queries;

public record ListBookStatusesQuery() : IRequest<IEnumerable<BookStatus>>;
