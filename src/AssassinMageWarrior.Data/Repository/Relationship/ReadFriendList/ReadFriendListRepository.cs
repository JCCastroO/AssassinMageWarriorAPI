using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Relationship.ReadFriendList;

public class ReadFriendListRepository : IReadFriendListRepository
{
    private readonly Context _context;

    public ReadFriendListRepository(Context context) => _context = context;

    public async Task<string?> GetFriendList(long id)
    {
        var user = await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();
        return user?.FriendList;
    }

    public async Task<User?> GetUsers(long id)
        => await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();
}
