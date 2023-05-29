using MediatR;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Radars.Queries;

public record ListRadarsQuery() : IRequest<IEnumerable<Radar>>;
