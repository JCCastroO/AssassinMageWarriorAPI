using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Room.CloseRoom;

public interface ICloseRoomRepository
{
    Task<User?> GetUser(long id);

    Task DeleteRoom(long id);
}
