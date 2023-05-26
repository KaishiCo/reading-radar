using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Interfaces.Persistence.Repositories;

public interface IBookRepository
{
    Task<bool> CreateAsync(Book book);
    Task<IEnumerable<Book>> GetAllAsync();
}
