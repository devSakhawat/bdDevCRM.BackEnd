using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bdDevCRM.Presentation.Controllers;


public class BuggyController : BaseApiController
{
  public BuggyController(IServiceManager serviceManager) : base(serviceManager)
  {

  }

  [HttpGet("unauthorized")]
  public IActionResult GetUnauthorized()
  {
    return Unauthorized();
  }

  [HttpGet("badrequest")]
  public IActionResult GetBadRequest()
  {
    return BadRequest("Not a good request");
  }

  [HttpGet("notfound")]
  public IActionResult GetNotFound()
  {
    return NotFound();
  }

  [HttpGet("internalerror")]
  public IActionResult GetInternalError()
  {
    throw new Exception("This is a test exception");
  }

  [HttpPost("validationerror")]
  public IActionResult GetValidationError(UsersDto product)
  {
    return Ok();
  }

  //[Authorize]
  [HttpGet("secret")]
  public IActionResult GetSecret()
  {
    var name = User.FindFirst(ClaimTypes.Name)?.Value;
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    return Ok("Hello " + name + " with the id of " + id);
  }

  //[Authorize(Roles = "Admin")]
  [HttpGet("admin-secret")]
  public IActionResult GetAdminSecret()
  {
    var name = User.FindFirst(ClaimTypes.Name)?.Value;
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var isAdmin = User.IsInRole("Admin");
    var roles = User.FindFirstValue(ClaimTypes.Role);

    return Ok(new
    {
      name,
      id,
      isAdmin,
      roles
    });
  }

  //[Authorize(Roles = "Admin")]
  [HttpGet("GenericNotFoundException")]
  public IActionResult GenericNotFoundException()
  {
    throw new GenericNotFoundException("Menu", "MenuId", "10");
  }

  [HttpGet("GenericListNotFoundException")]
  public IActionResult GenericListNotFoundException()
  {
    throw new GenericListNotFoundException("Menu");
  }

  [HttpGet("IdParametersBadRequestException")]
  public IActionResult IdParametersBadRequestException()
  {
    throw new IdParametersBadRequestException();
  }

  [HttpGet("CollectionByIdsBadRequestException")]
  public IActionResult CollectionByIdsBadRequestException()
  {
    throw new CollectionByIdsBadRequestException("Menus");
  }

  [HttpGet("NullModelBadRequestException")]
  public IActionResult NullModelBadRequestException()
  {
    throw new NullModelBadRequestException(new MenuDto().GetType().Name.ToString());
  }

  [HttpGet("ArgumentOutOfRangeException")]
  public IActionResult ArgumentOutOfRangeException(int moduleId)
  {
    throw new ArgumentOutOfRangeException(nameof(moduleId), "Module ID must be a positive integer.");

  }

  //[HttpGet("NotFoundException")]
  //public IActionResult NotFoundException(int moduleId)
  //{
  //   return NotFoundException();

  //}
}

