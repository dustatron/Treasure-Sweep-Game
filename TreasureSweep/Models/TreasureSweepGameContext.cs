using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TreasureSweepGame.Models
{
  public class TreasureSweepGameContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Game> Games { get; set; }
    public TreasureSweepGameContext(DbContextOptions options) : base(options) { }


  }
}