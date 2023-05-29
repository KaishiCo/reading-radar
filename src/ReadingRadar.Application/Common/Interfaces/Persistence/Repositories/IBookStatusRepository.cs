using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;

public interface IBookStatusRepository
{
    Task<bool> CreateAsync(BookStatus bookStatus);
    Task<bool> UpsertAsync(BookStatus bookStatus);
    Task<IEnumerable<BookStatus>> GetAllAsync();
    Task<BookStatus?> GetByBookIdAsync(Guid bookId);
    Task<bool> ExistsAsync(Guid bookId);
}
