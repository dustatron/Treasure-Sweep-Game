using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TreasureSweepGame.Models;
using TreasureSweepGame.ViewModels;
using System.Security.Claims;

namespace TeasureSweepGame.Controllers
{
  [Authorize]
  public class GamesController : Controller
  {
    private readonly TreasureSweepGameContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public GamesController(TreasureSweepGameContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    public ActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> Create()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Profile currentProfile = _db.Profiles.FirstOrDefault(entry => entry.User == currentUser);
      ViewBag.ProfileId = currentProfile.ProfileId;
      return View();
    }

    [HttpPost]
    public ActionResult Create(Game game)
    {
      try
      {
        Profile playerTwo = _db.Profiles.FirstOrDefault(profile => profile.ProfileId == game.P2Id);

        if (playerTwo != null)
        {
          _db.Games.Add(game);
          _db.SaveChanges();
          return RedirectToAction("Details", new { id = game.GameId });
        }
        else
        {
          throw new System.InvalidOperationException("User was not found");
        }
      }
      catch (Exception ex)
      {
        return View("Error", ex.Message);
      }
    }

    public ActionResult Details(int id)
    {
      Game thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      Profile playerOne = _db.Profiles.FirstOrDefault(entry => entry.ProfileId == thisGame.P1Id);
      ViewBag.PlayerOne = playerOne.Name;
      Profile playerTwo = _db.Profiles.FirstOrDefault(entry => entry.ProfileId == thisGame.P2Id);
      ViewBag.PlayerTwo = playerTwo.Name;
      return View(thisGame);
    }
    public ActionResult Error(string message)
    {
      return View(message);
    }
  }
}