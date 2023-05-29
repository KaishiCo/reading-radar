using FluentValidation;
using ReadingRadar.Domain.Enums;

namespace ReadingRadar.Application.Features.BookStatuses.Commands;

public class UpsertBookStatusCommandValidator : AbstractValidator<UpsertBookStatusCommand>
{
    public UpsertBookStatusCommandValidator()
    {
        RuleFor(x => x.BookId).NotEmpty();

        RuleFor(x => x.Status)
            .Must(x => Enum.IsDefined(typeof(Status), x))
            .WithMessage($"Must be a valid status type: {GetStatuses()}");
    }

    private static string GetStatuses()
    {
        var statuses = Enum.GetValues<Status>();

        return string.Join(", ", statuses.Select(x => $"{(int)x} ({x})"));
    }
}