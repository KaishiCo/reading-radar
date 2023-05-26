namespace ReadingRadar.Contracts.Books;

public record CreateBookRequest(
    string Title,
    string Author,
    int MediaType,
    string? Description,
    int? PageCount,
    string? ImageLink,
    DateTime? PublishDate,
    Guid? SeriesId);
