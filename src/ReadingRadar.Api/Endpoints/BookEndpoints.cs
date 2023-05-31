using MediatR;
using ReadingRadar.Api.Mapping;
using ReadingRadar.Application.Features.Commands;
using ReadingRadar.Application.Features.Queries;
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
            request.Pages,
            request.Chapters,
            request.ImageLink,
            request.PublishDate,
            request.SeriesId
        );

        var result = await sender.Send(command);

        return result.Match(
            book => Results.Ok(book.AsResponse()),
            error => error.AsHttpResult());
    }

    private static async Task<IResult> ListBooks(ISender sender)
    {
        var items = await sender.Send(new ListBooksQuery());
        return Results.Ok(items.Select(b => b.AsResponse()));
    }
}
