namespace ReadingRadar.Contracts.Radars;

public record GetRadarsResponse(
    IEnumerable<GetRadarResponse> Items
);
