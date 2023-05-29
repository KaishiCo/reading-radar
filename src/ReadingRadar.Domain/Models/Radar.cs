using ReadingRadar.Domain.Enums;

namespace ReadingRadar.Domain.Models;

public class Radar
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required Status Status { get; set; }
    public int ChaptersCompleted { get; set; }
    public DateTime? CompletionDate { get; set; }
    public required Guid BookId { get; set; }
}
