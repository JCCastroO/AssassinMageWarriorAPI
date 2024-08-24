using AssassinMageWarrior.Data.Entities;

namespace AssassinMageWarrior.Data.Repository.Room.UserInRoom;

public interface IUserInRoomRepository
{
    Task<User[]?> GetUsers(long id);
}
