using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Persistence.Repositories;

public class BookStatusRepository : IBookStatusRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public BookStatusRepository(IDbConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public async Task<bool> CreateAsync(BookStatus bookStatus)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            INSERT INTO BookStatus(Id, Status, ChaptersCompleted, BookId, CompletionDate)
            VALUES(@Id, @Status, @ChaptersCompleted, @BookId, @CompletionDate)
        """, bookStatus);

        return result > 0;
    }

    public async Task<bool> UpsertAsync(BookStatus bookStatus)
    {
        if (!await ExistsAsync(bookStatus.BookId))
            return await CreateAsync(bookStatus);

        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            UPDATE BookStatus SET
                Status = @Status,
                ChaptersCompleted = @ChaptersCompleted,
                CompletionDate = @CompletionDate
            WHERE BookId = @BookId
        """, bookStatus);

        return result > 0;
    }

    public async Task<IEnumerable<BookStatus>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<BookStatus>("SELECT * FROM BookStatus");
    }

    public async Task<BookStatus?> GetByBookIdAsync(Guid bookId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryFirstOrDefaultAsync<BookStatus>("""
            SELECT * FROM BookStatus WHERE BookId = @BookId
        """, new { BookId = bookId });
    }

    public async Task<bool> ExistsAsync(Guid bookId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteScalarAsync<int>("""
            SELECT EXISTS (SELECT 1 FROM BookStatus WHERE BookId = @BookId)
        """, new { BookId = bookId });

        return result > 0;
    }
}
