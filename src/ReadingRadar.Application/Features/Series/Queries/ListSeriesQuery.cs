using MediatR;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Queries;

public record ListSeriesQuery() : IRequest<IEnumerable<Series>>;
