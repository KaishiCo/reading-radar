using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Services;

namespace ReadingRadar.Infra.Persistence;

public class DbInitializer : IDbInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IDataSeedService _dataSeedService;

    public DbInitializer(IDbConnectionFactory connectionFactory, IDataSeedService dataSeedService)
    {
        _connectionFactory = connectionFactory;
        _dataSeedService = dataSeedService;
    }

    public async Task InitializeAsync(bool seed)
    {
        await CreateTables();

        if (seed)
            await _dataSeedService.SeedAsync();
    }

    private async Task CreateTables()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Series(
                Id UUID PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                LastUpdated DATE NOT NULL)
        """);

        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Book(
                Id UUID PRIMARY KEY,
                Title VARCHAR(255) NOT NULL,
                Author VARCHAR(255) NOT NULL,
                MediaType int NOT NULL,
                Description TEXT,
                Pages INT,
                Chapters INT,
                ImageLink VARCHAR(255),
                PublishDate DATE,
                SeriesId UUID,
                FOREIGN KEY (SeriesId) REFERENCES Series(Id))
        """);

        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Radar(
                Id UUID PRIMARY KEY,
                Status int NOT NULL,
                ChaptersCompleted INT NOT NULL,
                BookId UUID NOT NULL,
                CompletionDate DATE,
                FOREIGN KEY (BookId) REFERENCES Book(Id))
        """);

        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Activity(
                Id UUID PRIMARY KEY,
                Status INT NOT NULL,
                Amount INT,
                BookId UUID NOT NULL,
                Date TIMESTAMP NOT NULL,
                FOREIGN KEY (BookId) REFERENCES Book(Id))
        """);
    }
}
