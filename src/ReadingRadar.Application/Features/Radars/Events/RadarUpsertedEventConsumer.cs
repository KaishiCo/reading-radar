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
        var activity = new Activity
        {
            Id = Guid.NewGuid(),
            Status = context.Message.Status,
            Amount = GetAmount(context.Message.Status, context.Message.ChapterEnd, context.Message.ChapterStart),
            BookId = context.Message.BookId,
            Date = context.Message.Date
        };
        // make sure Date is not older than an hour
        var latestActivity = await _activityRepo.GetMostRecentAsync(context.Message.BookId);
        if (latestActivity?.Status == Status.Reading
            && activity.Status == Status.Reading
            && latestActivity.Date > _dateTimeProvider.UtcNow.AddHours(-1)
            && activity.Amount is not null)
        {
            activity.Amount += latestActivity.Amount;
            await _activityRepo.UpdateAsync(activity, latestActivity.Id);
            return;
        }

        await _activityRepo.CreateAsync(activity);
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
