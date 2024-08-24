using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssassinMageWarrior.Data.Entities;

public class Invite
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Inviter { get; set; } = string.Empty;

    [Required]
    public long UserId { get; set; }

    [Required]
    public long RoomId { get; set; }

    [Required]
    public DateOnly SendTime { get; set; }
}
