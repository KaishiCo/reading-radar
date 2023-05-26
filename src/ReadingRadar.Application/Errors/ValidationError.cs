namespace ReadingRadar.Application.Errors;

public record ValidationError(
    List<(string Field, string Message)> Errors);
