
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Auth.Logged;

public class LoggedRepository : ILoggedRepository
{
    private readonly Context _context;

    public LoggedRepository(Context context) => _context = context;

    public async Task SetUserStatusActive(long id)
    {
        var existingUser = await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();
        existingUser!.Active = true;
        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();
    }
}
