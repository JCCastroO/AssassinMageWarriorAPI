using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Auth.Register;

public interface IRegisterRepository
{
    Task<bool> VerifyEmail(string email);

    Task SaveAsync(User user);
}
