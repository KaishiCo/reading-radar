using Microsoft.AspNetCore.Mvc.ModelBinding;
using ReadingRadar.Application.Errors;

namespace ReadingRadar.Api.Mapping;

public static class HttpResultExtensions
{
    public static IResult AsValidationProblemResult(this ValidationError error)
    {
        var errorsByField = error.Errors
            .GroupBy(e => e.Field)
            .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());

        return Results.ValidationProblem(errors: errorsByField);
    }
}
