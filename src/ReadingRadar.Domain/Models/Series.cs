namespace ReadingRadar.Domain.Models;

public class Series
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required DateTime LastUpdated { get; set; }
    public List<Book> Books { get; set; } = new();
}
