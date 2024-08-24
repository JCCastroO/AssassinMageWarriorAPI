using AssassinMageWarrior.Data.Repository.Room.AcceptInvite;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Room;

public class AcceptInviteHandler : IRequestHandler<AcceptInviteRequest, AcceptInviteResponse>
{
    private readonly ILogger<AcceptInviteHandler> _logger;
    private readonly IAcceptInviteRepository _repository;

    public AcceptInviteHandler(ILogger<AcceptInviteHandler> logger, IAcceptInviteRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<AcceptInviteResponse> Handle(AcceptInviteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando AcceptInviteHandler...");
        var invite = await _repository.GetInvite(request.Id);
        if (invite is null)
            throw new ApplicationException("Convite não encontrado!");

        await _repository.SetUserRoom(invite.UserId, invite.RoomId);
        await _repository.RemoveInvite(invite.Id);

        return new AcceptInviteResponse();
    }
}
