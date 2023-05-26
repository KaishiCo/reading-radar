using FluentValidation.Results;
using ReadingRadar.Application.Errors;

namespace ReadingRadar.Application.Mapping;

public static class MappingExtensions
{
    public static ValidationError AsValidationError(this IEnumerable<ValidationFailure> failures)
    {
        return new ValidationError(failures.Select(e => (e.PropertyName, e.ErrorMessage)).ToList());
    }
}
