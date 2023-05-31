using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Persistence.Repositories;

internal sealed class SeriesRepository : ISeriesRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SeriesRepository(IDbConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public async Task<bool> CreateAsync(Series series)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var results = await connection.ExecuteAsync("""
            INSERT INTO Series(Id, Name, LastUpdated)
            VALUES (@Id, @Name, @LastUpdated)
        """, series);

        return results > 0;
    }

    public async Task<bool> ExistsAsync(string name)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var results = await connection.ExecuteScalarAsync<int>("""
            SELECT 1 FROM Series WHERE Name = @Name
        """, new { Name = name });

        return results == 1;
    }

    public async Task<IEnumerable<Series>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<Series>("""
            SELECT * FROM Series
        """);
    }

    public async Task<Series?> GetByIdAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var series = await connection.QueryFirstOrDefaultAsync<Series>("""
            SELECT Name, LastUpdated FROM Series WHERE Id = @Id
        """, new { Id = id });

        if (series is not null)
        {
            var books = await connection.QueryAsync<Book>("""
                SELECT * FROM Book WHERE SeriesId = @Id
            """, new { Id = id });

            series.Books.AddRange(books);
        }

        return series;
    }
}
