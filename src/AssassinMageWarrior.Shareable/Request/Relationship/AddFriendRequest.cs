using AssassinMageWarrior.Shareable.Response.Relationship;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Relationship;

public record AddFriendRequest(string FriendEmail, long UserId) : IRequest<AddFriendResponse>;
