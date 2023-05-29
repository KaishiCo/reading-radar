using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Persistence.Repositories;

public class RadarRepository : IRadarRepository
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

    public async Task<bool> UpsertAsync(Radar radar)
    {
        if (!await ExistsAsync(radar.BookId))
            return await CreateAsync(radar);

        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            UPDATE Radar SET
                Status = @Status,
                ChaptersCompleted = @ChaptersCompleted,
                CompletionDate = @CompletionDate
            WHERE BookId = @BookId
        """, radar);

        return result > 0;
    }

    public async Task<IEnumerable<Radar>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<Radar>("SELECT * FROM Radar");
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
}
