using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TreasureSweepGame.Models
{
  public class TreasureSweepGameContext : IdentityDbContext<ApplicationUser>
  {
    public TreasureSweepGameContext(DbContextOptions options) : base(options) { }
  }
}