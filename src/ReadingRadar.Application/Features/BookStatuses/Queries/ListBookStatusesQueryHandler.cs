using MediatR;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.BookStatuses.Queries;

public class ListBookStatusesQueryHandler : IRequestHandler<ListBookStatusesQuery, IEnumerable<BookStatus>>
{
    private readonly IBookStatusRepository _bookStatusRepo;

    public ListBookStatusesQueryHandler(IBookStatusRepository bookStatusRepo)
        => _bookStatusRepo = bookStatusRepo;

    public async Task<IEnumerable<BookStatus>> Handle(ListBookStatusesQuery request, CancellationToken cancellationToken) =>
        await _bookStatusRepo.GetAllAsync();
}
