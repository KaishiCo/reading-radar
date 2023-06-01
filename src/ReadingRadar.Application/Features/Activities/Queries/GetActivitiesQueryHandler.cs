using MediatR;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Queries;

internal sealed class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, IEnumerable<Activity>>
{
    private readonly IActivityRepository _activityRepo;

    public GetActivitiesQueryHandler(IActivityRepository activityRepo) =>
        _activityRepo = activityRepo;

    public async Task<IEnumerable<Activity>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken) =>
        await _activityRepo.GetAllAsync();
}
