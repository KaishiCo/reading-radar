using ReadingRadar.Domain.Enums;

namespace ReadingRadar.Application.Features.Events;

internal record RadarUpsertedEvent
{
    public required Guid Id { get; init; }
    public required Status Status { get; set; }
    public required Guid BookId { get; set; }
    public required int ChapterStart { get; set; }
    public required int ChapterEnd { get; set; }
    public required DateTime Date { get; set; }
}
