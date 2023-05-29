namespace ReadingRadar.Contracts.Radars;

public record UpsertRadarRequest(
    int Status,
    int ChaptersCompleted,
    DateTime? CompletionDate
);
