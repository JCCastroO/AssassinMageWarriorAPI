using AssassinMageWarrior.Shareable.Dto.Auth;
using FluentValidation;

namespace AssassinMageWarrior.Domain.Validators;

public class RegisterValitator : AbstractValidator<RegisterDto>
{
    public RegisterValitator()
    {
        RuleFor(r => r.Username).NotEmpty().WithMessage("É obrigatório preencher um nome de usuário!");
        RuleFor(r => r.Email).NotEmpty().WithMessage("É obrigatório preencher um email!");
        RuleFor(r => r.Email).EmailAddress().WithMessage("Email inválido!");
        RuleFor(r => r.Password).NotEmpty().WithMessage("É obrigatório preencher uma senha!");
        RuleFor(r => r.Password.Length).GreaterThanOrEqualTo(6).WithMessage("É obrigatório que a senha tenha 6 caractéres ou mais!");
    }
}
