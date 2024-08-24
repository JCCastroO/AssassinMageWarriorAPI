using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Room;

public record CancelInviteRequest(long Id) : IRequest<CancelInviteResponse>;