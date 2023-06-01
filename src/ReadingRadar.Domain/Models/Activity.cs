using ReadingRadar.Domain.Enums;

namespace ReadingRadar.Domain.Models;

public class Activity
{
    public required Guid Id { get; init; }
    public required Status Status { get; set; }
    public int? Amount { get; set; }
    public required Guid BookId { get; set; }
    public required DateTime Date { get; set; }
}
