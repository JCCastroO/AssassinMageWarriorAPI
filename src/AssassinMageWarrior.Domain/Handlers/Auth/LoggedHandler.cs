using AssassinMageWarrior.Data.Repository.Auth.Logged;
using AssassinMageWarrior.Domain.Services;
using AssassinMageWarrior.Shareable.Dto.Auth;
using AssassinMageWarrior.Shareable.Request.Auth;
using AssassinMageWarrior.Shareable.Response.Auth;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Auth;

public class LoggedHandler : IRequestHandler<LoggedRequest, LoggedResponse>
{
    private readonly ILoggedRepository _repository;
    private readonly JwtGenerate _jwt;
    private readonly ILogger<LoggedHandler> _logger;

    public LoggedHandler(JwtGenerate jwt, ILogger<LoggedHandler> logger, ILoggedRepository repository)
    {
        _jwt = jwt;
        _logger = logger;
        _repository = repository;
    }

    public async Task<LoggedResponse> Handle(LoggedRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando LoggedHandler...");
        var principal = _jwt.ValidateToken(request.Token);
        if (principal == null)
            throw new ApplicationException("Houve um problema, tente novamente mais tarde!");

        var userId = principal.FindFirst("Id")?.Value is not null ? long.Parse(principal.FindFirst("Id")?.Value!) : 0;
        var userName = principal.FindFirst("Username")?.Value;

        if (!userId.Equals(0))
            await _repository.SetUserStatusActive(userId);

        var logged = new LoggedDto()
        {
            Id = userId,
            Username = userName!
        };

        return new LoggedResponse(logged);
    }
}
