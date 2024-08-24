using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Room.CloseRoom;

public class CloseRoomRepository : ICloseRoomRepository
{
    private readonly Context _context;

    public CloseRoomRepository(Context context) => _context = context;

    public async Task DeleteRoom(long id)
    {
        var user = await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();
        var room = await (from Room in _context.Rooms where Room.Id.Equals(user!.RoomId) select Room).FirstOrDefaultAsync();
        user!.RoomId = 1;
        _context.Users.Update(user);
        _context.Rooms.Remove(room!);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUser(long id)
        => await (from User in _context.Users where User.Id.Equals(id) select User).FirstOrDefaultAsync();
}
