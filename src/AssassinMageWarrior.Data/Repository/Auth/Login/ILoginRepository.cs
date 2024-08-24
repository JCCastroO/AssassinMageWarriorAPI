using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Auth.Login;

public interface ILoginRepository
{
    Task<User?> ExistingUser(User user);

    Task<User> UpdateLastLogin(long id);
}
