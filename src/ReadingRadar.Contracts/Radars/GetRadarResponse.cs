using ReadingRadar.Contracts.Books;

namespace ReadingRadar.Contracts.Radars;

public record GetRadarResponse(
    int Status,
    int ChaptersCompleted,
    DateTime? CompletionDate,
    GetBookResponse Book
);
