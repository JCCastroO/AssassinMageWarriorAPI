using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Data.Repository.Room.UserInRoom;
using AssassinMageWarrior.Shareable.Dto.Room;
using AssassinMageWarrior.Shareable.Request.Room;
using AssassinMageWarrior.Shareable.Response.Room;
using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Room;

public class UsersInRoomHandler : IRequestHandler<UsersInRoomRequest, UserInRoomResponse>
{
    private readonly IUserInRoomRepository _repository;
    private readonly ILogger<UsersInRoomHandler> _logger;

    public UsersInRoomHandler(ILogger<UsersInRoomHandler> logger, IUserInRoomRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<UserInRoomResponse> Handle(UsersInRoomRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando UsersInRoomHandler...");
        var users = await _repository.GetUsers(request.Id);
        if (users is null || users.Length.Equals(0))
            throw new ApplicationException("Sala vazia!");

        var usersInRoom = (from Users in users select new UserInRoomDto
        {
            Id = Users.Id,
            Username = Users.Username
        }).ToArray();

        return new UserInRoomResponse(usersInRoom);
    }
}
