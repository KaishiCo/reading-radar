using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;

namespace ReadingRadar.Infra.Persistence;

public class DbInitializer : IDbInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DbInitializer(IDbConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

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
                SeriesId UUID)
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
            CREATE TABLE IF NOT EXISTS Series(
                Id UUID PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                LastUpdated DATE NOT NULL)
        """);
    }
}
