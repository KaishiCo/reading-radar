using MassTransit;
using MediatR;
using OneOf;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Common.Interfaces.Services;
using ReadingRadar.Application.Errors;
using ReadingRadar.Application.Features.Events;
using ReadingRadar.Domain.Enums;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Commands;

internal sealed class UpsertRadarCommandHandler : IRequestHandler<UpsertRadarCommand, OneOf<Radar, IError>>
{
    private readonly IRadarRepository _radarRepo;
    private readonly IBookRepository _bookRepo;
    private readonly IPublishEndpoint _bus;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpsertRadarCommandHandler(IRadarRepository radarRepo, IBookRepository bookRepo, IPublishEndpoint bus, IDateTimeProvider dateTimeProvider)
    {
        _radarRepo = radarRepo;
        _bookRepo = bookRepo;
        _bus = bus;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<OneOf<Radar, IError>> Handle(UpsertRadarCommand request, CancellationToken cancellationToken)
    {
        if (!await _bookRepo.ExistsAsync(request.BookId))
            return new NotFoundError(nameof(Book), request.BookId);

        var radar = new Radar
        {
            Id = Guid.NewGuid(),
            Status = (Status)request.Status,
            BookId = request.BookId,
            CompletionDate = request.CompletionDate,
            ChaptersCompleted = request.ChaptersCompleted
        };

        var (_, oldChaptersCompleted) = await _radarRepo.UpsertAsync(radar);

        await _bus.Publish(new RadarUpsertedEvent
        {
            Id = radar.Id,
            Status = radar.Status,
            BookId = radar.BookId,
            ChapterStart = oldChaptersCompleted,
            ChapterEnd = radar.ChaptersCompleted,
            Date = _dateTimeProvider.UtcNow
        }, cancellationToken);

        return radar;
    }
}
