using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Data.Repository.Auth.Login;
using AssassinMageWarrior.Domain.Services;
using AssassinMageWarrior.Domain.Validators;
using AssassinMageWarrior.Shareable.Request.Auth;
using AssassinMageWarrior.Shareable.Response.Auth;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Auth;

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly ILoginRepository _repository;
    private readonly IMapper _mapper;
    private readonly EncryptPassword _encrypt;
    private readonly JwtGenerate _jwt;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(ILoginRepository repository, IMapper mapper, EncryptPassword encrypt, JwtGenerate jwt, ILogger<LoginHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _encrypt = encrypt;
        _jwt = jwt;
        _logger = logger;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando LoginHandler...");
        await Validate(request);

        var user = _mapper.Map<User>(request.User);
        user.Password = _encrypt.Encrypt(user.Password);

        var existingUser = await _repository.ExistingUser(user);
        if (existingUser is null)
            throw new ApplicationException("Login inválido!");

        var result = await _repository.UpdateLastLogin(existingUser.Id);
        result.FriendList = string.Empty;
        result.Password = string.Empty;
        var accessToken = _jwt.Generate(result);

        return new LoginResponse(accessToken);
    }

    private async Task Validate(LoginRequest request)
    {
        LoginValidator validator = new();
        var result = validator.Validate(request.User);

        if (!result.IsValid)
        {
            var exception = (from Exception in result.Errors select Exception).FirstOrDefault();
            if (exception is not null)
                throw new ApplicationException(exception.ErrorMessage);
        }
        await Task.Run(() => { });
    }
}
