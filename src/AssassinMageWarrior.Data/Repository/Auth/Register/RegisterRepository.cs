using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Auth.Register;

public class RegisterRepository : IRegisterRepository
{
    private readonly Context _context;

    public RegisterRepository(Context context) => _context = context;

    public async Task SaveAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> VerifyEmail(string email)
    {
        var existingEmail = await (from User in _context.Users where User.Email.Equals(email) select User).Include(u => u.Room).ToArrayAsync();
        return existingEmail.Length.Equals(0);
    }
}
