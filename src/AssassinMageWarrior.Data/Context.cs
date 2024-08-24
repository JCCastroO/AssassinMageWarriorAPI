using AssassinMageWarrior.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssassinMageWarrior.Data;

public class Context : IdentityDbContext<IdentityUser>
{
    public new DbSet<User> Users { get; set; } = default!;
    public DbSet<Room> Rooms { get; set; } = default!;
    public DbSet<Invite> Invites { get; set; } = default!;

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasOne(u => u.Room)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoomId);
        builder.Entity<Room>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Room)
            .HasForeignKey(u => u.RoomId);
        base.OnModelCreating(builder);
    }
}
