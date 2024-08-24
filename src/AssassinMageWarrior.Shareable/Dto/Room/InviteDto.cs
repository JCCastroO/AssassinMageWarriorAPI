using System.ComponentModel.DataAnnotations;

namespace AssassinMageWarrior.Shareable.Dto.Room;

public class InviteDto
{
    public long Id { get; set; }
    public string Inviter { get; set; } = string.Empty;
    public long UserId { get; set; }
    public long RoomId { get; set; }
    public DateOnly SendTime { get; set; }
}
