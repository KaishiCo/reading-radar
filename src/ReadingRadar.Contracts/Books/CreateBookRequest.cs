namespace ReadingRadar.Contracts.Books;

public record CreateBookRequest(
    string Title,
    string Author,
    string Language,
    int MediaType,
    string? Description,
    int? Pages,
    int? Chapters,
    string? ImageLink,
    DateTime? PublishDate,
    Guid? SeriesId);
