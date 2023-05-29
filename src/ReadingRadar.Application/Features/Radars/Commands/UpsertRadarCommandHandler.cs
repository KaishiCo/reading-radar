using MediatR;
using OneOf;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Errors;
using ReadingRadar.Application.Features.Radars.Commands;
using ReadingRadar.Domain.Enums;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.BookStatuses.Commands;

public class UpsertRadarCommandHandler : IRequestHandler<UpsertRadarCommand, OneOf<Radar, IError>>
{
    private readonly IRadarRepository _radarRepo;
    private readonly IBookRepository _bookRepo;

    public UpsertRadarCommandHandler(IRadarRepository radarRepo, IBookRepository bookRepo)
    {
        _radarRepo = radarRepo;
        _bookRepo = bookRepo;
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

        await _radarRepo.UpsertAsync(radar);
        return radar;
    }
}
