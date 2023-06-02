using MassTransit;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Common.Interfaces.Services;
using ReadingRadar.Domain.Enums;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Events;

internal sealed class RadarUpsertedEventConsumer : IConsumer<RadarUpsertedEvent>
{
    private readonly IActivityRepository _activityRepo;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RadarUpsertedEventConsumer(IActivityRepository activityRepo, IDateTimeProvider dateTimeProvider)
    {
        _activityRepo = activityRepo;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Consume(ConsumeContext<RadarUpsertedEvent> context)
    {
        var newActivity = new Activity
        {
            Id = Guid.NewGuid(),
            Status = context.Message.Status,
            Amount = GetAmount(context.Message.Status, context.Message.ChapterEnd, context.Message.ChapterStart),
            BookId = context.Message.BookId,
            Date = context.Message.Date
        };

        var latestActivity = await _activityRepo.GetMostRecentAsync(context.Message.BookId);
        if (latestActivity?.Status == Status.Reading
            && newActivity.Status == Status.Reading
            && latestActivity.Date > _dateTimeProvider.UtcNow.AddHours(-1)
            && newActivity.Amount.GetValueOrDefault() - latestActivity.Amount.GetValueOrDefault() > 0)
        {
            newActivity.Amount += latestActivity.Amount;
            await _activityRepo.UpdateAsync(newActivity, latestActivity.Id);
            return;
        }

        if (newActivity.Status != Status.Reading && latestActivity?.Status == newActivity.Status)
            return;

        await _activityRepo.CreateAsync(newActivity);
    }

    private static int? GetAmount(Status status, int chapterEnd, int chapterStart)
    {
        return status switch
        {
            Status.Reading or Status.Completed => chapterEnd - chapterStart,
            _ => null
        };
    }
}
