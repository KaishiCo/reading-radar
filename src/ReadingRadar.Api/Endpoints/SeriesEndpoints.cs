using MediatR;
using ReadingRadar.Api.Mapping;
using ReadingRadar.Application.Features.Commands;
using ReadingRadar.Application.Features.Queries;
using ReadingRadar.Contracts.Series;

namespace ReadingRadar.Api.Endpoints;

public static class SeriesEndpoints
{
    public static void MapSeriesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/series", CreateSeries);
        app.MapGet("/api/series", GetSeries);
        app.MapGet("/api/series/{id:guid}", GetSeriesById);
    }

    private static async Task<IResult> CreateSeries(CreateSeriesRequest request, ISender sender)
    {
        var result = await sender.Send(new CreateSeriesCommand(request.Name));

        return result.Match(
            series => Results.Ok(series),
            error => error.AsHttpResult());
    }

    private static async Task<IResult> GetSeries(ISender sender)
    {
        var items = await sender.Send(new ListSeriesQuery());
        return Results.Ok(items.AsSeriesResponses());
    }

    private static async Task<IResult> GetSeriesById(Guid id, ISender sender)
    {
        var series = await sender.Send(new GetSeriesQuery(id));
        return series is not null
            ? Results.Ok(series.AsSeriesWithBooksResponse())
            : Results.NotFound();
    }
}
