using ReadingRadar.Application.Errors;

namespace ReadingRadar.Api.Mapping;

public static class EndpointExtensions
{
    public static IResult AsHttpResult(this IError error)
    {
        return error switch
        {
            ValidationError validationErr => MapToValidationProblem(validationErr),
            NotFoundError notFoundErr => Results.NotFound(notFoundErr),
            DuplicateResourceError duplicateResourceErr => MapToConflift(duplicateResourceErr),
            _ => Results.StatusCode(500)
        };
    }

    private static IResult MapToConflift(DuplicateResourceError duplicateResourceErr)
    {
        return Results.Conflict(duplicateResourceErr.Message);
    }

    private static IResult MapToValidationProblem(ValidationError error)
    {
        var errorsByField = error.Errors
            .GroupBy(e => e.Field)
            .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());

        return Results.ValidationProblem(errors: errorsByField);
    }
}
