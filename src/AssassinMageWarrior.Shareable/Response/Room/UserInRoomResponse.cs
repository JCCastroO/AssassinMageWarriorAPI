using AssassinMageWarrior.Data.Entities;
using AssassinMageWarrior.Shareable.Dto.Room;

namespace AssassinMageWarrior.Shareable.Response.Room;

public record UserInRoomResponse(UserInRoomDto[] Users);
