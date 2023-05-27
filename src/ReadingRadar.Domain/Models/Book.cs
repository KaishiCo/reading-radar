using ReadingRadar.Domain.Enums;

namespace ReadingRadar.Domain.Models;

public class Book
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required MediaType MediaType { get; set; }
    public string? Description { get; set; }
    public int? Pages { get; set; }
    public string? ImageLink { get; set; }
    public DateTime? PublishDate { get; set; }
    public Guid? SeriesId { get; set; }
}
