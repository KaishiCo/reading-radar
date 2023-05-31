using MediatR;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Queries;

internal sealed class ListSeriesQueryHandler : IRequestHandler<ListSeriesQuery, IEnumerable<Series>>
{
    private readonly ISeriesRepository _seriesRepo;

    public ListSeriesQueryHandler(ISeriesRepository seriesRepo)
    {
        _seriesRepo = seriesRepo;
    }

    public async Task<IEnumerable<Series>> Handle(ListSeriesQuery request, CancellationToken cancellationToken) =>
        await _seriesRepo.GetAllAsync();
}
