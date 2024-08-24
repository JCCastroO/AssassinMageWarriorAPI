using AssassinMageWarrior.Data.Repository.Relationship.InactiveUser;
using AssassinMageWarrior.Domain.Services;
using AssassinMageWarrior.Shareable.Request.Relationship;
using AssassinMageWarrior.Shareable.Response.Relationship;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Relationship;

public class InactiveUserHandler : IRequestHandler<InactiveUserRequest, InactiveUserResponse>
{
    private readonly IInactiveUserRepository _repository;
    private readonly ILogger<InactiveUserHandler> _logger;
    private readonly JwtGenerate _jwt;

    public InactiveUserHandler(IInactiveUserRepository repository, ILogger<InactiveUserHandler> logger, JwtGenerate jwt)
    {
        _repository = repository;
        _logger = logger;
        _jwt = jwt;
    }

    public async Task<InactiveUserResponse> Handle(InactiveUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando InactiveUserHandler...");
        var principal = _jwt.ValidateToken(request.Token);
        if (principal == null)
            throw new ApplicationException("Houve um problema, tente novamente mais tarde!");

        var userId = principal.FindFirst("Id")?.Value is not null ? long.Parse(principal.FindFirst("Id")?.Value!) : 0;
        if (!userId.Equals(0))
            await _repository.SetUserStatusInactive(userId);

        return new InactiveUserResponse();
    }
}
