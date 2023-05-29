namespace ReadingRadar.Application.Errors;

public record NotFoundError(string ResourceName, Guid ResourceId) : IError
{
    public string Message => $"{ResourceName} with id {ResourceId} was not found.";
}
