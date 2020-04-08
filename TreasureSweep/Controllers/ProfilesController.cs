using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TreasureSweepGame.Models;
using TreasureSweepGame.ViewModels;
using System.Collections.Generic;

namespace TeasureSweepGame.Controllers
{
  [Authorize]
  public class ProfilesController : Controller
  {
    private readonly TreasureSweepGameContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfilesController(TreasureSweepGameContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      try
      {
        int wins = 0;
        int completedGames = 0;

        Profile thisProfile = _db.Profiles.FirstOrDefault(profile => profile.User == currentUser);
        thisProfile.Games = _db.Games.Where(game => game.P1Id == thisProfile.ProfileId || game.P2Id == thisProfile.ProfileId).OrderByDescending(entry => entry.LastPlayed).ToList();
        if (thisProfile.Games.Count > 0)
        {
          foreach (Game game in thisProfile.Games)
          {
            if (game.IsComplete == true)
            {
              completedGames += 1;
              if (thisProfile.ProfileId == game.WinningPlayer)
              {
                wins += 1;
              }
            }
          }
        }
        int losses = completedGames - wins;
        ViewBag.Losses = losses;
        ViewBag.Wins = wins;
        ViewBag.Profiles = _db.Profiles.ToList();
        return View(thisProfile);
      }
      catch (Exception)
      {
        return RedirectToAction("Create");
      }
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Profile profile)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      profile.User = currentUser;
      if (String.IsNullOrWhiteSpace(profile.Img))
      {
        profile.Img = "/img/rubber-ducky.png";
      }
      _db.Profiles.Add(profile);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
      Profile thisProfile = _db.Profiles.FirstOrDefault(profile => profile.ProfileId == id);
      return View(thisProfile);
    }

    [HttpPost]
    public ActionResult Edit(Profile profile)
    {
      if (String.IsNullOrWhiteSpace(profile.Img))
      {
        profile.Img = "/img/rubber-ducky.png";
      }
      _db.Entry(profile).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    //search for profiles
    public async Task<ActionResult> Search(string playerName)
    {
      try
      {
        if (String.IsNullOrWhiteSpace(playerName) == false)
        {
          string searchName = playerName.ToLower();
          List<Profile> results = _db.Profiles.Where(profile => profile.Name.ToLower().Contains(playerName)).ToList();
          if (results.Count == 0)
          {
            throw new System.InvalidOperationException("User not found");
          }
          var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          var currentUser = await _userManager.FindByIdAsync(userId);
          Profile thisProfile = _db.Profiles.FirstOrDefault(entry => entry.User == currentUser);
          ViewBag.CurrentUserId = thisProfile.ProfileId;
          return View(results);
        }
        else
        {
          throw new System.InvalidOperationException("User not found");
        }
      }
      catch (Exception ex)
      {
        return View("Error", ex.Message);
      }
    }
  }
}