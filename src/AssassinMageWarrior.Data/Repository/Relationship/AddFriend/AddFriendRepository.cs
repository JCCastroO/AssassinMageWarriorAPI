using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Relationship.AddFriend;

public class AddFriendRepository : IAddFriendRepository
{
    private readonly Context _context;

    public AddFriendRepository(Context context) => _context = context;

    public async Task<User?> ExistingFriend(string email)
        => await (from Users in _context.Users where Users.Email.Equals(email) select Users).Include(u => u.Room).FirstOrDefaultAsync();

    public async Task<User?> ExistingUser(long id)
        => await (from Users in _context.Users where Users.Id.Equals(id) select Users).Include(u => u.Room).FirstOrDefaultAsync();

    public async Task UpdateFriendList(long id, string friendList)
    {
        var user = await (from Users in _context.Users where Users.Id.Equals(id) select Users).Include(u => u.Room).FirstOrDefaultAsync();
        user!.FriendList = friendList;
        await _context.SaveChangesAsync();
    }

    public async Task<object> VerifyList()
    {
        var list = await (from Users in _context.Users select Users).Include(u => u.Room).ToArrayAsync();

        return list;
    }
}
