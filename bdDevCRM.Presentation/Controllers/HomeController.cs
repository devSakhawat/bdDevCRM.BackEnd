using Microsoft.AspNetCore.Mvc;

namespace bdDevCRM.Presentation.Controllers;

public class HomeController : ControllerBase
{
  [HttpGet]
  public IActionResult Index()
  {
    return Ok("Welcome to bdDevCRM");
  }
}
