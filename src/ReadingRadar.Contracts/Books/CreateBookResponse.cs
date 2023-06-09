namespace ReadingRadar.Contracts.Books;

public record CreateBookResponse(
    Guid Id,
    string Title,
    string Author,
    string Language,
    int MediaType,
    string? Description,
    int? PageCount,
    string? ImageLink,
    DateTime? PublishDate,
    Guid? SeriesId
);
