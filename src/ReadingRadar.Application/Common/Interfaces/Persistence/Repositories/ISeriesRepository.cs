using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;

public interface ISeriesRepository
{
    Task<bool> CreateAsync(Series series);
    Task<IEnumerable<Series>> GetAllAsync();
    Task<Series?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(string name);
}
