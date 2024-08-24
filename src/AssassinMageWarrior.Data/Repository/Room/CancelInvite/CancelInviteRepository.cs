
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Room.CancelInvite;

public class CancelInviteRepository : ICancelInviteRepository
{
    private readonly Context _context;

    public CancelInviteRepository(Context context) => _context = context;

    public async Task RemoveInvite(long id)
    {
        var invite = await (from Invite in _context.Invites where Invite.Id.Equals(id) select Invite).FirstOrDefaultAsync();
        _context.Invites.Remove(invite!);
        await _context.SaveChangesAsync();
    }
}
