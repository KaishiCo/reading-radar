using MediatR;
using ReadingRadar.Api.Mapping;
using ReadingRadar.Application.Features.Queries;

namespace ReadingRadar.Api.Endpoints;

public static class ActivitiesEndpoints
{
    public static void MapActivitiesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/activities", GetActivities);
    }

    private static async Task<IResult> GetActivities(ISender sender)
    {
        var items = await sender.Send(new GetActivitiesQuery());
        return Results.Ok(items.AsActivitiesResponse());
    }
}
