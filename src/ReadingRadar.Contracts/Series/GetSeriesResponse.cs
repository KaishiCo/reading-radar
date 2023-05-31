namespace ReadingRadar.Contracts.Series;

public record GetSeriesResponse(
    Guid Id,
    string Name,
    DateTime LastUpdated);
