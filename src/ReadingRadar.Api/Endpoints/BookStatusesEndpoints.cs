using MediatR;
using ReadingRadar.Api.Mapping;
using ReadingRadar.Application.Features.BookStatuses.Commands;
using ReadingRadar.Application.Features.BookStatuses.Queries;
using ReadingRadar.Contracts.BookStatuses;

namespace ReadingRadar.Api.Endpoints;

public static class BookStatusesEndpoints
{
    public static void MapBookStatusesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/bookStatuses", ListBookStatuses);
        app.MapPut("/api/books/{bookId:guid}/bookStatus", UpsertBookStatus);
    }

    private static async Task<IResult> ListBookStatuses(ISender sender)
    {
        var items = await sender.Send(new ListBookStatusesQuery());
        return Results.Ok(items);
    }

    private static async Task<IResult> UpsertBookStatus(Guid bookId, UpsertBookStatusRequest request, ISender sender)
    {
        var command = new UpsertBookStatusCommand(
            BookId: bookId,
            Status: request.Status,
            ChaptersCompleted: request.ChaptersCompleted,
            CompletionDate: request.CompletionDate);

        var result = await sender.Send(command);

        return result.Match(
            bookStatus => Results.Ok(bookStatus),
            error => error.AsHttpResult());
    }
}
