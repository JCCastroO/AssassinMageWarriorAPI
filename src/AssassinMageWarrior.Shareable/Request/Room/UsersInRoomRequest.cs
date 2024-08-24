using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Room;

public record UsersInRoomRequest(long Id) : IRequest<UserInRoomResponse>;
