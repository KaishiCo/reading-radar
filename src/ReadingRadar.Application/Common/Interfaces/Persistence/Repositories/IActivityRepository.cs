using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;

public interface IActivityRepository
{
    Task<bool> CreateAsync(Activity activity);
    Task<IEnumerable<Activity>> GetAllAsync();
}
