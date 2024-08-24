using AssassinMageWarrior.Shareable.Dto.Auth;
using AssassinMageWarrior.Shareable.Response.Auth;
using MediatR;

namespace AssassinMageWarrior.Shareable.Request.Auth;

public record RegisterRequest(RegisterDto User) : IRequest<RegisterResponse>;
