using MediatR;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;

namespace ReadingRadar.Application.Features.Commands;

internal sealed class DeleteRadarCommandHandler : IRequestHandler<DeleteRadarCommand, bool>
{
    private readonly IRadarRepository _radarRepo;

    public DeleteRadarCommandHandler(IRadarRepository radarRepo) =>
        _radarRepo = radarRepo;

    public async Task<bool> Handle(DeleteRadarCommand request, CancellationToken cancellationToken) =>
        await _radarRepo.DeleteByBookIdAsync(request.BookId);
}
