using AssassinMageWarrior.Shareable.Response.Room;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Room;

public record AcceptInviteRequest(long Id) : IRequest<AcceptInviteResponse>;