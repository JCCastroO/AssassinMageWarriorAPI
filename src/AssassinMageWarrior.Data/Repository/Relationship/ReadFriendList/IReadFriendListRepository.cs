using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Relationship.ReadFriendList;

public interface IReadFriendListRepository
{
    Task<User?> GetUsers(long id);

    Task<string?> GetFriendList(long id);
}
