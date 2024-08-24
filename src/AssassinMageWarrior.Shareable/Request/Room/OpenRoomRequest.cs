using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Room;

public record OpenRoomRequest(long Id) : IRequest<OpenRoomResponse>;
