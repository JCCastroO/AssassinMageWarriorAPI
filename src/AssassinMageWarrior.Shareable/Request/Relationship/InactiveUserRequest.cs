using AssassinMageWarrior.Shareable.Response.Relationship;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Relationship;

public record InactiveUserRequest(string Token) : IRequest<InactiveUserResponse>;
