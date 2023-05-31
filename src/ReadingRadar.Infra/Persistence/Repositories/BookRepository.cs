using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Persistence.Repositories;

internal sealed class BookRepository : IBookRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public BookRepository(IDbConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public async Task<bool> CreateAsync(Book book)
    {
        const string sql = """
            INSERT INTO Book (Id, Title, Author, MediaType, Description, Pages, Chapters, ImageLink, PublishDate, SeriesId)
            VALUES (@Id, @Title, @Author, @MediaType, @Description, @Pages, @Chapters, @ImageLink, @PublishDate, @SeriesId)    
        """;

        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(sql, book);
        return result > 0;
    }

    public async Task<bool> ExistsAsync(Guid bookId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteScalarAsync<int>("""
            SELECT EXISTS (SELECT 1 FROM Book WHERE Id = @Id)
        """, new { Id = bookId });

        return result == 1;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<Book>("SELECT * FROM book");
    }
}
