namespace ReadingRadar.Application.Errors;

public record DuplicateResourceError(string Message) : IError;
