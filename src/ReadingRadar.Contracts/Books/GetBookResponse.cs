namespace ReadingRadar.Contracts.Books;

public record GetBookResponse(
    Guid Id,
    string Title,
    string Author,
    int MediaType,
    string? Description,
    int? Pages,
    int? Chapters,
    string? ImageLink,
    DateTime? PublishDate,
    Guid? SeriesId
);
