using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Persistence.Repositories;

internal sealed class RadarRepository : IRadarRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public RadarRepository(IDbConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public async Task<bool> CreateAsync(Radar radar)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            INSERT INTO Radar(Id, Status, ChaptersCompleted, BookId, CompletionDate)
            VALUES(@Id, @Status, @ChaptersCompleted, @BookId, @CompletionDate)
        """, radar);

        return result > 0;
    }

    public async Task<(bool created, int oldChaptersCompleted)> UpsertAsync(Radar radar)
    {
        var existingRadar = await GetByBookIdAsync(radar.BookId);
        if (existingRadar is null)
            return (await CreateAsync(radar), 0);

        using var connection = await _connectionFactory.CreateConnectionAsync();

        var created = await connection.ExecuteAsync("""
            UPDATE Radar SET
                Status = @Status,
                ChaptersCompleted = @ChaptersCompleted,
                CompletionDate = @CompletionDate
            WHERE BookId = @BookId
        """, radar) > 0;

        return (created, existingRadar.ChaptersCompleted);
    }

    public async Task<IEnumerable<Radar>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        const string query = @"
        SELECT r.*, b.*
        FROM Radar r
        JOIN Book b ON r.BookId = b.Id";

        return await connection.QueryAsync<Radar, Book, Radar>(query, (radar, book) =>
        {
            radar.Book = book;
            return radar;
        });
    }

    public async Task<Radar?> GetByBookIdAsync(Guid bookId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryFirstOrDefaultAsync<Radar>("""
            SELECT * FROM Radar WHERE BookId = @BookId
        """, new { BookId = bookId });
    }

    public async Task<bool> ExistsAsync(Guid bookId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteScalarAsync<int>("""
            SELECT EXISTS (SELECT 1 FROM Radar WHERE BookId = @BookId)
        """, new { BookId = bookId });

        return result > 0;
    }

    public async Task<bool> DeleteByBookIdAsync(Guid bookId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            DELETE FROM Radar WHERE BookId = @BookId
        """, new { BookId = bookId });

        return result > 0;
    }
}
