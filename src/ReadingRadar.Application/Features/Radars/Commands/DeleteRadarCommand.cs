using MediatR;

namespace ReadingRadar.Application.Features.Radars.Commands;

public record DeleteRadarCommand(
    Guid BookId
) : IRequest<bool>;
