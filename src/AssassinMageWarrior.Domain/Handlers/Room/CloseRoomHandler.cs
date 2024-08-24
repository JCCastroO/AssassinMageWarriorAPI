using AssassinMageWarrior.Data.Repository.Room.CloseRoom;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Room;

public class CloseRoomHandler : IRequestHandler<CloseRoomRequest, CloseRoomResponse>
{
    private readonly ICloseRoomRepository _repository;
    private readonly ILogger<CloseRoomHandler> _logger;

    public CloseRoomHandler(ICloseRoomRepository repository, ILogger<CloseRoomHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CloseRoomResponse> Handle(CloseRoomRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando CloseRoomHandler...");
        var user = await _repository.GetUser(request.Id);
        if (user is null)
            throw new ApplicationException("Usuario não encontrado!");

        if (!user.RoomId.Equals(6))
            await _repository.DeleteRoom(user.Id);

        return new CloseRoomResponse();
    }
}
