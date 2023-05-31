using MediatR;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Queries;

internal sealed class ListBooksQueryHandler : IRequestHandler<ListBooksQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _bookRepository;

    public ListBooksQueryHandler(IBookRepository bookRepository) =>
        _bookRepository = bookRepository;

    Task<IEnumerable<Book>> IRequestHandler<ListBooksQuery, IEnumerable<Book>>.Handle(ListBooksQuery request, CancellationToken cancellationToken)
    {
        return _bookRepository.GetAllAsync();
    }
}
