using AssassinMageWarrior.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data.Repository.Room.OpenRoom;

public class OpenRoomRepository : IOpenRoomRepository
{
    private readonly Context _context;

    public OpenRoomRepository(Context context) => _context = context;

    public async Task<long> GetRoomId(string leader)
        => await (from Room in _context.Rooms where Room.Leader.Equals(leader) select Room.Id).FirstOrDefaultAsync();

    public async Task<User?> GetUser(long id)
        => await (from User in _context.Users where User.Id.Equals(id) select User).Include(u => u.Room).FirstOrDefaultAsync();

    public async Task OpenRoom(Entities.Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }

    public async Task SetLeader(long userId, long roomId)
    {
        var user = await (from User in _context.Users where User.Id.Equals(userId) select User).Include(u => u.Room).FirstOrDefaultAsync();
        user!.RoomId = roomId;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
