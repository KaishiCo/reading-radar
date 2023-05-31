namespace ReadingRadar.Contracts.Series;

public record GetSeriesResponses(
    IEnumerable<GetSeriesResponse> Items);
