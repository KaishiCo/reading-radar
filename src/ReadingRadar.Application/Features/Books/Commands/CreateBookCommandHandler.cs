using FluentValidation;
using MediatR;
using OneOf;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Enums;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Books.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, OneOf<Book, ValidationError>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<CreateBookCommand> _validator;

    public CreateBookCommandHandler(IBookRepository bookRepository, IValidator<CreateBookCommand> validator)
    {
        _bookRepository = bookRepository;
        _validator = validator;
    }

    public async Task<OneOf<Book, ValidationError>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            MediaType = (MediaType)request.MediaType,
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
