using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Room.ReceiveInvite;

public interface IReceiveInviteRepository
{
    Task<Invite?> GetInvite(long id);
}
