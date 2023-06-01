using MassTransit;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Events;

internal sealed class RadarUpsertedEventConsumer : IConsumer<RadarUpsertedEvent>
{
    private readonly IActivityRepository _activityRepo;

    public RadarUpsertedEventConsumer(IActivityRepository activityRepo)
        => _activityRepo = activityRepo;

    public async Task Consume(ConsumeContext<RadarUpsertedEvent> context)
    {
        await _activityRepo.CreateAsync(new Activity
        {
            Id = Guid.NewGuid(),
            Status = context.Message.Status,
            Amount = context.Message.ChapterEnd - context.Message.ChapterStart,
            BookId = context.Message.BookId,
            Date = context.Message.Date
        });
    }
}
