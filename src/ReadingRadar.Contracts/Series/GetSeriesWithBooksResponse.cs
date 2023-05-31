using ReadingRadar.Contracts.Books;

namespace ReadingRadar.Contracts.Series;

public record GetSeriesWithBooksResponse(
    string Name,
    DateTime LastUpdated,
    IEnumerable<GetBookResponse> Books);
