namespace ReadingRadar.Contracts.Activities;

public record GetActivityResponse(
    Guid Id,
    int Status,
    int? Amount,
    Guid BookId,
    DateTime Date

);
