using MediatR;

namespace ReadingRadar.Application.Features.Commands;

public record DeleteRadarCommand(
    Guid BookId
) : IRequest<bool>;
