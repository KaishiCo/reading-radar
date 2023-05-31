using MediatR;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Queries;

public record GetSeriesQuery(Guid Id) : IRequest<Series?>;
