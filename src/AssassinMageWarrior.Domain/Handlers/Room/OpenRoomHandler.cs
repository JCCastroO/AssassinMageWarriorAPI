using AssassinMageWarrior.Data.Repository.Room.OpenRoom;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Room;

public class OpenRoomHandler : IRequestHandler<OpenRoomRequest, OpenRoomResponse>
{
    private readonly IOpenRoomRepository _repository;
    private readonly ILogger<OpenRoomHandler> _logger;

    public OpenRoomHandler(IOpenRoomRepository repository, ILogger<OpenRoomHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<OpenRoomResponse> Handle(OpenRoomRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando OpenRoomHandler...");
        var user = await _repository.GetUser(request.Id);
        if (user is null)
            throw new ApplicationException("Usuário não encontrado!");

        var room = new Data.Entities.Room()
        {
            Leader = user.Username
        };
        await _repository.OpenRoom(room);

        var roomId = await _repository.GetRoomId(room.Leader);
        if (roomId.Equals(0))
            throw new ApplicationException("Ocorreu um problema ao criar a sala!");

        await _repository.SetLeader(user.Id, roomId);

        return new OpenRoomResponse(roomId);
    }
}
