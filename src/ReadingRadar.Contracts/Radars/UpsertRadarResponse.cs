namespace ReadingRadar.Contracts.Radars;

public record UpsertRadarResponse(
    int Status,
    int ChaptersCompleted,
    DateTime? CompletionDate
);
