using MediatR;
using OneOf;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Enums;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Commands;

internal sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, OneOf<Book, IError>>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<OneOf<Book, IError>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            MediaType = (MediaType)request.MediaType,
            Description = request.Description,
            Pages = request.Pages,
            Chapters = request.Chapters,
            ImageLink = request.ImageLink,
            PublishDate = request.PublishDate,
            SeriesId = request.SeriesId
        };

        await _bookRepository.CreateAsync(book);

        return book;
    }
}
