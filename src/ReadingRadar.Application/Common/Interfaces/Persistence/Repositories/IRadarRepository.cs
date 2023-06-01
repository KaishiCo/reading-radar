using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;

public interface IRadarRepository
{
    Task<bool> CreateAsync(Radar radar);
    Task<(bool created, int oldChaptersCompleted)> UpsertAsync(Radar radar);
    Task<IEnumerable<Radar>> GetAllAsync();
    Task<Radar?> GetByBookIdAsync(Guid bookId);
    Task<bool> ExistsAsync(Guid bookId);
    Task<bool> DeleteByBookIdAsync(Guid bookId);
}
