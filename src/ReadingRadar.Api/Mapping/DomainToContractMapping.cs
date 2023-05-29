using ReadingRadar.Contracts.Radars;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Api.Mapping;

public static class DomainToContractMapping
{
    public static UpsertRadarResponse AsResponse(this Radar radar) =>
            new(
                (int)radar.Status,
                radar.ChaptersCompleted,
                radar.CompletionDate);
}
