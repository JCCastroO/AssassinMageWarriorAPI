using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Data.Repository.Auth.Register;
using AssassinMageWarrior.Domain.Services;
using AssassinMageWarrior.Domain.Validators;
using AssassinMageWarrior.Shareable.Request.Auth;
using AssassinMageWarrior.Shareable.Response.Auth;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssassinMageWarrior.Domain.Handlers.Auth;

public class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly IRegisterRepository _repository;
    private readonly IMapper _mapper;
    private readonly EncryptPassword _encrypt;
    private readonly ILogger<RegisterHandler> _logger;

    public RegisterHandler(IRegisterRepository repository, IMapper mapper, EncryptPassword encrypt, ILogger<RegisterHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _encrypt = encrypt;
        _logger = logger;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando RegisterHandler...");
        await Validate(request);

        var user = _mapper.Map<User>(request.User);
        user.LastLogin = DateOnly.FromDateTime(DateTime.Now.ToLocalTime());
        user.Active = false;
        user.Password = _encrypt.Encrypt(user.Password);
        user.RoomId = 1;

        await _repository.SaveAsync(user);

        return new RegisterResponse("Usuário Cadastrado com Sucesso!");
    }

    public async Task Validate(RegisterRequest request)
    {
        RegisterValitator validator = new();
        var result = validator.Validate(request.User);

        var validEmail = await _repository.VerifyEmail(request.User.Email);
        if (!validEmail)
            throw new ApplicationException("Email já cadastrado!");

        if (!result.IsValid)
        {
            var exception = (from Exception in result.Errors select Exception).FirstOrDefault();
            if (exception is not null)
                throw new ApplicationException(exception.ErrorMessage);
        }
    }
}
