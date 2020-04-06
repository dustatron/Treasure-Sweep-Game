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

    public ActionResult Index()
    {
      return View();
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

      _db.Profiles.Add(profile);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Profile thisProfile = _db.Profiles.FirstOrDefault(profile => profile.ProfileId == id);
      ViewBag.Games = _db.Games.Where(game => game.P1Id == id || game.P2Id == id).ToList();
      return View(thisProfile);
    }

    public ActionResult Edit(int id)
    {
      Profile thisProfile = _db.Profiles.FirstOrDefault(profile => profile.ProfileId == id);
      return View();
    }

    [HttpPost]
    public ActionResult Edit(Profile profile)
    {
      _db.Entry(profile).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}