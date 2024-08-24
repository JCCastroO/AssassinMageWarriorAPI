using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Room;

public record InviteToRoomRequest(long UserId, long FrienId): IRequest<InviteToRoomResponse>;
