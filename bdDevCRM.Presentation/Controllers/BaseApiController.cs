//using Microsoft.AspNetCore.Mvc;

//namespace bdDevCRM.Presentation.Controllers;

using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[Route(RouteConstants.BaseRoute)]
[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//[ServiceFilter(typeof(LogActionAttribute))]
//[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
[EnableCors]
public class BaseApiController : ControllerBase
{

}