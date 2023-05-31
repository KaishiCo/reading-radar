using MediatR;
using OneOf;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Common.Interfaces.Services;
using ReadingRadar.Application.Errors;
using ReadingRadar.Domain.Models;

namespace ReadingRadar.Application.Features.Commands;

internal sealed class CreateSeriesCommandHandler : IRequestHandler<CreateSeriesCommand, OneOf<Series, IError>>
{
    private readonly ISeriesRepository _seriesRepo;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateSeriesCommandHandler(ISeriesRepository seriesRepo, IDateTimeProvider dateTimeProvider)
    {
        _seriesRepo = seriesRepo;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<OneOf<Series, IError>> Handle(CreateSeriesCommand request, CancellationToken cancellationToken)
    {
        if (await _seriesRepo.ExistsAsync(request.Name))
            return new DuplicateResourceError($"A {nameof(Series)} with this name already exists.");

        var series = new Series
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            LastUpdated = _dateTimeProvider.UtcNow
        };

        await _seriesRepo.CreateAsync(series);

        return series;
    }
}
