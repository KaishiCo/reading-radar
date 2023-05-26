using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Infra.Persistence.Repositories;

public class InMemBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    public Task<bool> CreateAsync(Book book)
    {
        _books.Add(book);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return Task.FromResult(_books.AsEnumerable());
    }
}
