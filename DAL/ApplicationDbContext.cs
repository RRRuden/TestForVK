using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UsersState { get; set; }
    public DbSet<UserGroup> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>()
            .HasData(new UserGroup { Id = 1, Code = Group.User, Description = "Regular app user" },
                new UserGroup { Id = 2, Code = Group.Admin, Description = "Application administrator" });

        modelBuilder.Entity<UserState>().HasData(new UserState {
            Id = 1, Code = State.Active, Description = "User active" },
            new UserState { Id = 2, Code = State.Blocked, Description = "User is blocked" });
    }
}