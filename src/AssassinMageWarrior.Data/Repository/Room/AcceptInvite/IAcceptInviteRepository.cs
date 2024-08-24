using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Room.AcceptInvite;

public interface IAcceptInviteRepository
{
    Task<Invite?> GetInvite(long id);

    Task SetUserRoom(long userId, long roomId);

    Task RemoveInvite(long id);
}
