using ReadingRadar.Application.Errors;

namespace ReadingRadar.Api.Mapping;

public static class EndpointExtensions
{
    public static IResult AsHttpResult(this IError error)
    {
        return error switch
        {
            ValidationError validationErr => MapToValidationProblem(validationErr),
            NotFoundError notFoundError => Results.NotFound(notFoundError),
            _ => Results.StatusCode(500)
        };
    }
    private static IResult MapToValidationProblem(ValidationError error)
    {
        var errorsByField = error.Errors
            .GroupBy(e => e.Field)
            .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());

        return Results.ValidationProblem(errors: errorsByField);
    }
}
