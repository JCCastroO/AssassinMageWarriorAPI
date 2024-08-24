using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssassinMageWarrior.Data.Entities;

public class User
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public DateOnly LastLogin { get; set; }

    [Required]
    public bool Active { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string FriendList { get; set; } = string.Empty;

    public long RoomId { get; set; } = 1;

    public virtual Room? Room { get; set; }
}
