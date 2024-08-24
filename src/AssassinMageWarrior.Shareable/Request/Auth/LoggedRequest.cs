using AssassinMageWarrior.Shareable.Response.Auth;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Auth;

public record LoggedRequest(string Token) : IRequest<LoggedResponse>;
