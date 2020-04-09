using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
// using AspNet.Identity.SQLite;

using System.IO;

namespace TreasureSweepGame.Models
{
  public class TreasureSweepGameFactory : IDesignTimeDbContextFactory<TreasureSweepGameContext>
  {

    TreasureSweepGameContext IDesignTimeDbContextFactory<TreasureSweepGameContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<TreasureSweepGameContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseSqlite(connectionString);

      return new TreasureSweepGameContext(builder.Options);
    }
  }
}