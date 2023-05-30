using MediatR;
using ReadingRadar.Api.Mapping;
using ReadingRadar.Application.Features.Radars.Commands;
using ReadingRadar.Application.Features.Radars.Queries;
using ReadingRadar.Contracts.Radars;

namespace ReadingRadar.Api.Endpoints;

public static class RadarsEndpoints
{
    public static void MapBookStatusesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/radars", ListRadars);
        app.MapPut("/api/radars/{bookId:guid}", UpsertRadar);
        app.MapDelete("/api/radars/{bookId:guid}", DeleteRadar);
    }


    private static async Task<IResult> ListRadars(ISender sender)
    {
        var items = await sender.Send(new ListRadarsQuery());
        return Results.Ok(items.AsRadarsResponse());
    }

    private static async Task<IResult> UpsertRadar(Guid bookId, UpsertRadarRequest request, ISender sender)
    {
        var command = new UpsertRadarCommand(
            BookId: bookId,
            Status: request.Status,
            ChaptersCompleted: request.ChaptersCompleted,
            CompletionDate: request.CompletionDate);

        var result = await sender.Send(command);

        return result.Match(
            radar => Results.Ok(radar.AsResponse()),
            error => error.AsHttpResult());
    }

    private static async Task<IResult> DeleteRadar(Guid bookId, ISender sender)
    {
        var deleted = await sender.Send(new DeleteRadarCommand(bookId));

        return deleted
            ? Results.Ok()
            : Results.NotFound();
    }
}
