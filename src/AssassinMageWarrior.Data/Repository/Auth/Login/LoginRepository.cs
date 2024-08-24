using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Auth.Login;

public class LoginRepository : ILoginRepository
{
    private readonly Context _context;

    public LoginRepository(Context context) => _context = context;

    public async Task<User> UpdateLastLogin(long id)
    {
        var user = await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();
        user!.LastLogin = DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
        user.Active = true;
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> ExistingUser(User user)
        => await (from User in _context.Users where User.Username.Equals(user.Username) && User.Email.Equals(user.Email) && User.Password.Equals(user.Password) select User).Include(u => u.Room).Include(u => u.Room).FirstOrDefaultAsync();
}
