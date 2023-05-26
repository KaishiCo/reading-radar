using MediatR;
using ReadingRadar.Application.Features.Books.Commands;
using ReadingRadar.Application.Features.Books.Queries;
using ReadingRadar.Contracts.Books;

namespace ReadingRadar.Api.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/books", CreateBook);
        app.MapGet("/api/books", ListBooks);
    }

    private static async Task<IResult> CreateBook(CreateBookRequest request, ISender sender)
    {
        var command = new CreateBookCommand(
            request.Title,
            request.Author,
            request.MediaType,
            request.Description,
            request.PageCount,
            request.ImageLink,
            request.PublishDate,
            request.SeriesId
        );

        var result = await sender.Send(command);

        return result.Match(
            book => Results.Ok(book),
            error => Results.BadRequest(error)
        );
    }

    private static async Task<IResult> ListBooks(ISender sender)
    {
        var items = await sender.Send(new ListBooksQuery());

        return Results.Ok(items);
    }
}
