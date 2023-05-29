using MediatR;
using OneOf;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Enums;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.BookStatuses.Commands;

public class UpsertBookStatusCommandHandler : IRequestHandler<UpsertBookStatusCommand, OneOf<BookStatus, IError>>
{
    private readonly IBookStatusRepository _bookStatusRepo;
    private readonly IBookRepository _bookRepo;

    public UpsertBookStatusCommandHandler(IBookStatusRepository bookStatusRepo, IBookRepository bookRepo)
    {
        _bookStatusRepo = bookStatusRepo;
        _bookRepo = bookRepo;
    }

    public async Task<OneOf<BookStatus, IError>> Handle(UpsertBookStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _bookRepo.ExistsAsync(request.BookId))
            return new NotFoundError(nameof(Book), request.BookId);

        var bookStatus = new BookStatus
        {
            Id = Guid.NewGuid(),
            Status = (Status)request.Status,
            BookId = request.BookId,
            CompletionDate = request.CompletionDate,
            ChaptersCompleted = request.ChaptersCompleted
        };

        await _bookStatusRepo.UpsertAsync(bookStatus);
        return bookStatus;
    }
}
