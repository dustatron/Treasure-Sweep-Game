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
using Newtonsoft.Json;

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

    public async Task<ActionResult> Turn(int id)
    {
      Game currentGame = _db.Games.FirstOrDefault(entry => entry.GameId == id);

      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Profile currentProfile = _db.Profiles.FirstOrDefault(entry => entry.User == currentUser);

      string firstBoard = currentGame.P1Board;
      string secondBoard = currentGame.P2Board;

      int[,] p1Board = JsonConvert.DeserializeObject<int[,]>(firstBoard);
      int[,] p2Board = JsonConvert.DeserializeObject<int[,]>(secondBoard);

      if (currentGame.P1Id == currentProfile.ProfileId)
      {
        ViewBag.P1Board = p1Board;
        ViewBag.P2Target = Game.Scrub(p2Board);
        ViewBag.CurrentPlayer = 1;
      }
      else if (currentGame.P2Id == currentProfile.ProfileId)
      {
        ViewBag.P2Board = p2Board;
        ViewBag.P1Target = Game.Scrub(p1Board);
        ViewBag.CurrentPlayer = 2;
      }

      if (currentGame.IsComplete == true)
      {
        ViewBag.PlayerName = currentProfile.Name;
        ViewBag.PlayerId = currentProfile.ProfileId;
        ViewBag.CurrentPlayerName = currentProfile.Name;
        if (currentProfile.ProfileId == currentGame.P1Id)
        {
          Profile otherProfile = _db.Profiles.FirstOrDefault(entry => entry.ProfileId == currentGame.P2Id);
          ViewBag.OtherPlayerName = otherProfile.Name;
        }
        else
        {
          Profile otherProfile = _db.Profiles.FirstOrDefault(entry => entry.ProfileId == currentGame.P1Id);
          ViewBag.OtherPlayerName = otherProfile.Name;
        }
      }

      if ((currentGame.TurnCount % 2 == 1 && currentGame.P1Id == currentProfile.ProfileId) || (currentGame.TurnCount % 2 == 0 && currentGame.P2Id == currentProfile.ProfileId))
      {
        ViewBag.IsYourTurn = false;
      }
      else
      {
        ViewBag.IsYourTurn = true;
      }
      return View(currentGame);
    }

    [HttpPost]
    public ActionResult Play(int playerId, int gameId, int x, int y)
    {
      Game currentGame = _db.Games.FirstOrDefault(entry => entry.GameId == gameId);

      int[,] board = currentGame.TakeTurn(x, y, playerId);
      string boardJson = JsonConvert.SerializeObject(board);
      if (playerId == currentGame.P1Id)
      {
        currentGame.P2Board = boardJson;
        currentGame.CurrentPlayer = currentGame.P2Id;
      }
      else if (playerId == currentGame.P2Id)
      {
        currentGame.P1Board = boardJson;
        currentGame.CurrentPlayer = currentGame.P1Id;
      }
      currentGame.TurnCount++;

      _db.Entry(currentGame).State = EntityState.Modified;
      _db.SaveChanges();

      return RedirectToAction("Turn", new { id = gameId });
    }
  }
}