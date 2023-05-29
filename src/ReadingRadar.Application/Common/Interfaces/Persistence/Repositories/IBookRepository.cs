using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;

public interface IBookRepository
{
    Task<bool> CreateAsync(Book book);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<bool> ExistsAsync(Guid bookId);
}
