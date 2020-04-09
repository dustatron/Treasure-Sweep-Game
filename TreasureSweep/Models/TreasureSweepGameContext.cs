using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TreasureSweepGame.Models
{
  public class TreasureSweepGameContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Game> Games { get; set; }
    public TreasureSweepGameContext(DbContextOptions options) : base(options) { }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //   base.OnModelCreating(builder);
    //   // Customize the ASP.NET Identity model and override the defaults if needed.
    //   // For example, you can rename the ASP.NET Identity table names and more.
    //   // Add your customizations after calling base.OnModelCreating(builder);

    //   builder.Entity<ApplicationUser>() //Use your application user class here
    //          .ToTable("TreasureSweepter"); //Set the table name here
    // }

  }
}