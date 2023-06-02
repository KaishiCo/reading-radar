namespace ReadingRadar.Contracts.Activities;

public record GetActivitiesResponse(IEnumerable<GetActivityResponse> Activities);
