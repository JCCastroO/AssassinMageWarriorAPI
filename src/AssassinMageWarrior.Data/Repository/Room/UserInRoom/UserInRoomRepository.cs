using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Room.UserInRoom;

public class UserInRoomRepository : IUserInRoomRepository
{
    private readonly Context _context;

    public UserInRoomRepository(Context context) => _context = context;

    public async Task<User[]?> GetUsers(long id)
    {
        var user = await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();
        var room = await (from Room in _context.Rooms where Room.Id.Equals(user!.RoomId) select Room).FirstOrDefaultAsync();
        return room!.Users!.ToArray();
    }
}
