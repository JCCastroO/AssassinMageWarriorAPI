using AssassinMageWarrior.Shareable.Response.Relationship;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Relationship;

public record ReadFriendListRequest(long Id) : IRequest<ReadFriendListResponse>;
