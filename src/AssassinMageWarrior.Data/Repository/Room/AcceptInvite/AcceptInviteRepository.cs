using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Room.AcceptInvite;

public class AcceptInviteRepository : IAcceptInviteRepository
{
    private readonly Context _context;

    public AcceptInviteRepository(Context context) => _context = context;

    public async Task<Invite?> GetInvite(long id)
        => await (from Invite in _context.Invites where Invite.Id.Equals(id) select Invite).FirstOrDefaultAsync();

    public async Task RemoveInvite(long id)
    {
        var invite = await (from Invite in _context.Invites where Invite.Id.Equals(id) select Invite).FirstOrDefaultAsync();
        _context.Invites.Remove(invite!);
        await _context.SaveChangesAsync();
    }

    public async Task SetUserRoom(long userId, long roomId)
    {
        var user = await (from User in _context.Users where User.Id.Equals(userId) select User).FirstOrDefaultAsync();
        var room = await (from Room in _context.Rooms where Room.Id.Equals(user!.RoomId) select Room).FirstOrDefaultAsync();
        user!.RoomId = roomId;
        _context.Users.Update(user);
        _context.Rooms.Remove(room!);
        await _context.SaveChangesAsync();
    }
}
