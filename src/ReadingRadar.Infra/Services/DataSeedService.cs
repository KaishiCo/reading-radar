using Bogus;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Common.Interfaces.Services;
using ReadingRadar.Domain.Enums;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Services;

internal sealed class DataSeedService : IDataSeedService
{
    private readonly ISeriesRepository _seriesRepo;
    private readonly IBookRepository _bookRepo;
    private readonly List<Guid?> _seriesIds = new()
    {
        Guid.NewGuid(),
        Guid.NewGuid(),
        Guid.NewGuid()
    };

    public DataSeedService(ISeriesRepository seriesRepository, IBookRepository bookRepo)
    {
        _seriesRepo = seriesRepository;
        _bookRepo = bookRepo;
    }

    public async Task SeedAsync()
    {
        var series = GenerateSeries();

        foreach (var item in series)
        {
            await _seriesRepo.CreateAsync(item);
        }

        foreach (var book in GenerateBooks())
        {
            await _bookRepo.CreateAsync(book);
        }
    }

    private IEnumerable<Series> GenerateSeries()
    {
        var seriesNames = new List<string> { "dxd", "danjoru", "konosuba" };
        var counter = 0;

        return new Faker<Series>()
            .RuleFor(s => s.Id, _ => _seriesIds[counter])
            .RuleFor(s => s.Name, _ => seriesNames[counter++])
            .RuleFor(s => s.LastUpdated, f => f.Date.Past())
            .Generate(_seriesIds.Count);
    }

    private IEnumerable<Book> GenerateBooks()
    {
        return new Faker<Book>()
            .RuleFor(b => b.Id, _ => Guid.NewGuid())
            .RuleFor(b => b.Title, f => f.Lorem.Sentence())
            .RuleFor(b => b.Author, f => f.Name.FullName())
            .RuleFor(b => b.MediaType, f => f.PickRandom<MediaType>())
            .RuleFor(b => b.Description, f => f.Lorem.Paragraph())
            .RuleFor(b => b.Pages, f => f.Random.Int(0, 1000))
            .RuleFor(b => b.Chapters, f => f.Random.Int(0, 100))
            .RuleFor(b => b.ImageLink, f => f.Image.PicsumUrl())
            .RuleFor(b => b.PublishDate, f => f.Date.Past())
            .RuleFor(b => b.SeriesId, f => f.PickRandom(_seriesIds).OrDefault(f))
            .Generate(10);
    }
}
