using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Room.ReceiveInvite;

public class ReceiveInviteRepository : IReceiveInviteRepository
{
    private readonly Context _context;

    public ReceiveInviteRepository(Context context) => _context = context;

    public async Task<Invite?> GetInvite(long id)
        => await (from Invite in _context.Invites where Invite.UserId.Equals(id) select Invite).FirstOrDefaultAsync();
}
