using AssassinMageWarrior.Data.Repository.Room.CancelInvite;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Room;

public class CancelInviteHandler : IRequestHandler<CancelInviteRequest, CancelInviteResponse>
{
    private readonly ILogger<CancelInviteHandler> _logger;
    private readonly ICancelInviteRepository _repository;

    public CancelInviteHandler(ILogger<CancelInviteHandler> logger, ICancelInviteRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<CancelInviteResponse> Handle(CancelInviteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando CancelInviteHandler...");
        await _repository.RemoveInvite(request.Id);

        return new CancelInviteResponse();
    }
}
