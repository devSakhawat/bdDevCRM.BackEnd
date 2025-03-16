using System.ComponentModel.DataAnnotations;

namespace bdDevCRM.Shared.DataTransferObjects.Authentication;

public class UserForAuthenticationDto
{
  [Required(ErrorMessage = "User name is required")]
  public string LoginId { get; set; }
  [Required(ErrorMessage = "Password name is required")]
  public string Password { get; set; }

  public bool? IsRememberMe { get; set; }
}