using FluentValidation.Results;
using ReadingRadar.Application.Errors;

namespace ReadingRadar.Application.Mapping;

internal static class MappingExtensions
{
    public static ValidationError AsValidationError(this IEnumerable<ValidationFailure> failures)
    {
        return new ValidationError(failures.Select(e => (e.PropertyName, e.ErrorMessage)).ToList());
    }
}
