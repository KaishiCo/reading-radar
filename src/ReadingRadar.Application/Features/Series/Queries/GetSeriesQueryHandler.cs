using MediatR;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Features.Queries;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Queries;

public class GetSeriesQueryHandler : IRequestHandler<GetSeriesQuery, Series?>
{
    private readonly ISeriesRepository _seriesRepo;

    public GetSeriesQueryHandler(ISeriesRepository seriesRepo) =>
        _seriesRepo = seriesRepo;

    public async Task<Series?> Handle(GetSeriesQuery request, CancellationToken cancellationToken) =>
        await _seriesRepo.GetByIdAsync(request.Id);
}
