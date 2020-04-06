using System;
using System.Collections.Generic;

namespace TreasureSweepGame.Models
{
  public class Profile
  {
    public Profile()
    {
      this.Games = new HashSet<Game>();
    }
    public int ProfileId { get; set; }
    public string Name { get; set; }
    public string TagLine { get; set; }
    public string Img { get; set; }
    public virtual ApplicationUser User { get; set; }
    public ICollection<Game> Games { get; set; }
  }
}