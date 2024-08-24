using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Room.InviteToRoom;

public class InviteToRoomRepository : IInviteToRoomRepository
{
    private readonly Context _context;

    public InviteToRoomRepository(Context context) => _context = context;

    public async Task<User?> GetUser(long id)
        => await (from User in _context.Users where User.Id.Equals(id) select User).FirstOrDefaultAsync();

    public async Task SendInvite(Invite invite)
    {
        _context.Invites.Add(invite);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> VerifyInvites(long id)
    {
        var invite = await (from Invite in _context.Invites where Invite.UserId.Equals(id) select Invite).FirstOrDefaultAsync();
        return invite is not null;
    }
}
