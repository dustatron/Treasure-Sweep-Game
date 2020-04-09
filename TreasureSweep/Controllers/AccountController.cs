using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TreasureSweepGame.Models;
using TreasureSweepGame.ViewModels;

namespace TreasureSweepGame.Controllers
{
  public class AccountController : Controller
  {
    private readonly TreasureSweepGameContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TreasureSweepGameContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Register(string message)
    {
      if (message != null)
      {
        ViewBag.Message = message;
      }
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      try
      {
        if (String.IsNullOrWhiteSpace(model.Email) || String.IsNullOrWhiteSpace(model.Password) || model.Password != model.ConfirmPassword)
        {
          throw new System.InvalidOperationException("invalid input");
        }
        else
        {
          var user = new ApplicationUser { UserName = model.Email };
          IdentityResult result = await _userManager.CreateAsync(user, model.Password);
          if (result.Succeeded)
          {
            return RedirectToAction("Login");
          }
          else
          {
            string message = "Registration unsuccessful. Please try again.";
            return View("Register", new { message = message });
          }
        }
      }
      catch (Exception)
      {
        return View();
      }
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      try
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          return RedirectToAction("Details", "Profiles");
        }
        else
        {
          return View();
        }
      }
      catch (Exception)
      {
        return View();
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}