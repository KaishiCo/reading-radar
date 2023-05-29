using MediatR;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Radars.Queries;

public class ListRadarsQueryHandler : IRequestHandler<ListRadarsQuery, IEnumerable<Radar>>
{
    private readonly IRadarRepository _radarRepo;

    public ListRadarsQueryHandler(IRadarRepository radarRepo)
        => _radarRepo = radarRepo;

    public async Task<IEnumerable<Radar>> Handle(ListRadarsQuery request, CancellationToken cancellationToken) =>
        await _radarRepo.GetAllAsync();
}
