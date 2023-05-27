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
    }
}
