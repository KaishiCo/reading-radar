using System.Data;
using Npgsql;
using ReadingRadar.Application.Common.Interfaces.Persistence;

namespace ReadingRadar.Infra.Persistence;

internal sealed class PgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public PgsqlConnectionFactory(string connectionString) =>
        _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
