using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Room;

public record ReceiveInviteRequest(long Id) : IRequest<ReceiveInviteResponse>;
