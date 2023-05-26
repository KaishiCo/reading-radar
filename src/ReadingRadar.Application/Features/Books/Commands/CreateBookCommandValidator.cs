using FluentValidation;
using ReadingRadar.Domain.Enums;

namespace ReadingRadar.Application.Features.Books.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage("Author is required.")
            .MaximumLength(100)
            .WithMessage("Author must not exceed 100 characters.");

        RuleFor(x => x.MediaType)
            .Must(x => Enum.IsDefined(typeof(MediaType), x))
            .WithMessage($"Media type must be a valid media type: {GetMediaTypes()}");
    }

    private static string GetMediaTypes()
    {
        var mediaTypes = Enum.GetValues<MediaType>();

        return string.Join(", ", mediaTypes.Select(x => $"{(int)x} ({x})"));
    }
}
