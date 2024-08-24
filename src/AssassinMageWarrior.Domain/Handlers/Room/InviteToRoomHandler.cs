using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Data.Repository.Room.InviteToRoom;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Room;

public class InviteToRoomHandler : IRequestHandler<InviteToRoomRequest, InviteToRoomResponse>
{
    private readonly ILogger<InviteToRoomHandler> _logger;
    private readonly IInviteToRoomRepository _repository;

    public InviteToRoomHandler(ILogger<InviteToRoomHandler> logger, IInviteToRoomRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<InviteToRoomResponse> Handle(InviteToRoomRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando InviteToRoomHandler...");
        var user = await _repository.GetUser(request.UserId);
        if (user is null)
            throw new ApplicationException("Usuário não encontrado!");

        var friend = await _repository.GetUser(request.FrienId);
        if (friend is null)
            throw new ApplicationException("Amigo não encontrado!");

        var existingInvite = await _repository.VerifyInvites(friend.Id);
        if (existingInvite)
        {

            return new InviteToRoomResponse();
        }

        var invite = new Invite()
        {
            Inviter = user.Username,
            UserId = friend.Id,
            RoomId = user.RoomId,
            SendTime = DateOnly.FromDateTime(DateTime.Now.ToLocalTime())
        };
        await _repository.SendInvite(invite);

        return new InviteToRoomResponse();
    }
}
