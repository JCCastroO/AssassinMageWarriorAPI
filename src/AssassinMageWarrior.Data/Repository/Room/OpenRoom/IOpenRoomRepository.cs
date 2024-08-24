using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Room.OpenRoom;

public interface IOpenRoomRepository
{
    Task<User?> GetUser(long id);

    Task OpenRoom(Entities.Room room);

    Task<long> GetRoomId(string leader);

    Task SetLeader(long userId, long roomId);
}
