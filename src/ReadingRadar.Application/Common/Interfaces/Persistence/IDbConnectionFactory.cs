using System.Data;

namespace ReadingRadar.Application.Common.Interfaces.Persistence;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
