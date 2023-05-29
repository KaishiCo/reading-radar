namespace ReadingRadar.Contracts.BookStatuses;

public record UpsertBookStatusRequest(
    int Status,
    int ChaptersCompleted,
    DateTime? CompletionDate
);
