namespace AssassinMageWarrior.Data.Repository.Auth.Logged;

public interface ILoggedRepository
{
    Task SetUserStatusActive(long id);
}
