using Dapper;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Persistence.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ActivityRepository(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<bool> CreateAsync(Activity activity)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(@"
            INSERT INTO Activity(Id, Status, Amount, BookId, Date)
            VALUES(@Id, @Status, @Amount, @BookId, @Date)
        ", activity);

        return result > 0;
    }

    public async Task<IEnumerable<Activity>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<Activity>("""
            SELECT Id, Status, Amount, BookId, Date
            FROM Activity
        """);
    }

    public async Task<Activity?> GetMostRecentAsync(Guid bookId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<Activity>("""
            SELECT Id, Status, Amount, Date
            FROM Activity
            WHERE BookId = @BookId
            ORDER BY Date DESC
            LIMIT 1;
        """, new { BookId = bookId });
    }

    public async Task<bool> UpdateAsync(Activity activity, Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(@"
            UPDATE Activity
            SET Status = @Status,
                Amount = @Amount,
                Date = @Date
            WHERE Id = @Id
        ", new { activity.Status, activity.Amount, activity.Date, Id = id });

        return result > 0;
    }
}
