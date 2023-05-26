using MediatR;
using OneOf;
using ReadingRadar.Application.Errors;
using ReadingRadar.Application.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Books.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, OneOf<Book, ValidationError>>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository) =>
        _bookRepository = bookRepository;

    public async Task<OneOf<Book, ValidationError>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            MediaType = request.MediaType,
            Description = request.Description,
            PageCount = request.PageCount,
            ImageLink = request.ImageLink,
            PublishDate = request.PublishDate,
            SeriesId = request.SeriesId
        };

        await _bookRepository.CreateAsync(book);

        return book;
    }
}
