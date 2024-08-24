using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Room.InviteToRoom;

public interface IInviteToRoomRepository
{
    Task<User?> GetUser(long id);

    Task<bool> VerifyInvites(long id);

    Task SendInvite(Invite invite);
}
