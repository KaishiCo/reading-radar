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
    }

    private static async Task<IResult> ListRadars(ISender sender)
    {
        var items = await sender.Send(new ListRadarsQuery());
        return Results.Ok(items);
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
}
