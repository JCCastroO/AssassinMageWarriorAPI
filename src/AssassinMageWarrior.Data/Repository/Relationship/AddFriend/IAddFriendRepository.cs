using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Relationship.AddFriend;

public interface IAddFriendRepository
{
    Task<User?> ExistingFriend(string email);

    Task<User?> ExistingUser(long id);

    Task UpdateFriendList(long id, string friendList);

    Task<object> VerifyList();
}
