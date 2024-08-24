
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Relationship.InactiveUser;

public class InactiveUserRepository : IInactiveUserRepository
{
    private readonly Context _context;

    public InactiveUserRepository(Context context) => _context = context;

    public async Task SetUserStatusInactive(long id)
    {
        var existingUser = await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();
        existingUser!.Active = false;
        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();
    }
}
