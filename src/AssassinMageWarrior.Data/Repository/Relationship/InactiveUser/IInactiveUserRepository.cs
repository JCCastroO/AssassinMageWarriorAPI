namespace AssassinMageWarrior.Data.Repository.Relationship.InactiveUser;

public interface IInactiveUserRepository
{
    Task SetUserStatusInactive(long id);
}
