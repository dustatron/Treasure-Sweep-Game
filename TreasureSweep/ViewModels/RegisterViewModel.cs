using System.ComponentModel.DataAnnotations;

namespace TreasureSweepGame.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}