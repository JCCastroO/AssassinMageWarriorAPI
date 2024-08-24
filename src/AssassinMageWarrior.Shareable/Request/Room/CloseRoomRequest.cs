using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Room;

public record CloseRoomRequest(long Id) : IRequest<CloseRoomResponse>;
