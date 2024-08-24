using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssassinMageWarrior.Data.Entities;

public class Room
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Leader { get; set; } = string.Empty;

    public virtual List<User>? Users { get; set; }
}
