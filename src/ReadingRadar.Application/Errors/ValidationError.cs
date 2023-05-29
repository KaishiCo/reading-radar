namespace ReadingRadar.Application.Errors;

public record ValidationError(
    List<(string Field, string Message)> Errors) : IError
{
    public string Message => "Validation errors occurred.";
}
