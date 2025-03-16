using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Authentication;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Reflection;
using System.Security.Claims;

namespace bdDevCRM.Presentation.Controllers.Authentication;

//[Route("api/[controller]")]
//[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthenticationController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _memoryCache;

  public AuthenticationController(IServiceManager serviceManager, IMemoryCache memoryCache)
  {
    _serviceManager = serviceManager;
    _memoryCache = memoryCache;
  }

  [HttpPost(RouteConstants.Login)]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  [AllowAnonymous]
  [IgnoreMediaTypeValidation]
  public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
  {
    if (!_serviceManager.CustomAuthentication.ValidateUser(user)) return Unauthorized();

    var token = await _serviceManager.CustomAuthentication.CreateToken(user);
    return Ok(token);
  }

  #region LoginFrom mvc

  //[HttpPost("validateLogin")]
  //public async Task<IActionResult> ValidateUserLogin(string loginId, string password, bool isRememberMe)
  //{
  //  var res = "";
  //  var user = "";
  //  try
  //  {
  //    var replacements = new Dictionary<char, char> { //!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~
  //                  { '+', ' ' }, // Add more replacements as needed
  //     };

  //    string encryptedpwd = "";
  //    if (!CommonHelper.IsEncrypted(password))
  //    {
  //      encryptedpwd = EncryptDecryptHelper.Encrypt(password);
  //      string pwdc = CommonHelper.ReplaceMultipleSpecificSpecialCharacters(encryptedpwd, replacements);
  //      encryptedpwd = "enc_" + pwdc;

  //    }
  //    else
  //    {
  //      encryptedpwd = password;
  //    }

  //    #region Front end part
  //    //var cookie = new HttpCookie("passwordRemember");
  //    //cookie.Values["userid"] = loginId;
  //    //cookie.Values["pwd"] = encryptedpwd;

  //    //if (isRememberMe)
  //    //{
  //    //  cookie.Expires = DateTime.Now.AddDays(15);
  //    //  cookie.Values["isRemember"] = "1";
  //    //}
  //    //else
  //    //{
  //    //  cookie.Values["isRemember"] = "0";
  //    //  cookie.Expires = DateTime.Now.AddDays(-1);
  //    //}
  //    //Response.Cookies.Add(cookie);
  //    #endregion Front end part

  //    var rep = new Dictionary<char, char>
  //              {
  //                  //!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~
  //                  { ' ', '+' },
  //                  // Add more replacements as needed
  //              };

  //    if (CommonHelper.IsEncrypted(password))
  //    {
  //      string sub = password.Substring("enc_".Length);
  //      string pwdc = CommonHelper.ReplaceMultipleSpecificSpecialCharacters(sub, rep);
  //      string dcrpwd = EncryptDecryptHelper.Decrypt(pwdc);
  //      password = dcrpwd;
  //    }

  //    AssemblyInfoDto objAssemblyInfo = await _serviceManager.SystemSettings.GetAssemblyInfoResult();
  //    var isValid = false;

  //    //Checking Currect Password with Entering Password
  //    if (objAssemblyInfo.AssemblyInfoId == 12)
  //    {
  //      var userInfo = await _serviceManager.Users.GetUserByLoginIdAsync(loginId, false);
  //      var currentPassword = userInfo.Password;
  //      string dycryptPass = EncryptDecryptHelper.Decrypt(currentPassword);

  //      if (password != dycryptPass)
  //      {
  //        return "Wrong Password! Please Enter Currect Password.";
  //      }
  //    }

  //    user = _serviceManager.aut.ValidateUserLogin(loginId, password, objasm, isValid);


  //    if ((user.Split('^')[0] == "Success") || (user.Split('^')[0] == "CHANGESHORT") ||
  //        (user.Split('^')[0] == "CHANGELEAVE") || (user.Split('^')[0] == "CHANGESuccess") ||
  //        (user.Split('^')[0] == "LATE") || (user.Split('^')[0] == "SHORT") || (user.Split('^')[0] == "LEAVE"))
  //    {
  //      var currentUser = loginService.GetCurrentUser(user);

  //      Session["themeName"] = currentUser.Theme;
  //      Session["CurrentUser"] = currentUser;
  //      if (user.Split('^')[0] == "SHORT" || user.Split('^')[0] == "LEAVE")
  //      {
  //        var attendanceLog =
  //            (AttendanceLog)JsonConvert.DeserializeObject(user.Split('^')[12], typeof(AttendanceLog));
  //        Session["Attendance"] = attendanceLog;
  //      }
  //      else
  //      {
  //        Session["Attendance"] = null;
  //      }

  //      var lvEmail = System.Web.HttpContext.Current.Session["LeaveApprovalEmail"];
  //      if (lvEmail != null)
  //      {
  //        res = "lvEmail";
  //        return res;
  //      }

  //      var osEmail = System.Web.HttpContext.Current.Session["OnsiteClientEmail"];
  //      if (osEmail != null)
  //      {
  //        res = "osEmail";
  //        return res;
  //      }

  //      var mvEmail = System.Web.HttpContext.Current.Session["MovementLogAuth"];
  //      if (mvEmail != null)
  //      {
  //        res = "mvEmail";
  //        return res;
  //      }
  //      var atEmail = System.Web.HttpContext.Current.Session["AttendanceAdjustmentEmail"];
  //      if (atEmail != null)
  //      {
  //        res = "atEmail";
  //        return res;
  //      }
  //      var vhcEmail = System.Web.HttpContext.Current.Session["RequisitionEmail"];
  //      if (vhcEmail != null)
  //      {
  //        res = "vhcEmail";
  //        return res;
  //      }
  //      var performacneEmail = System.Web.HttpContext.Current.Session["performanceReviewEmail"];
  //      if (performacneEmail != null)
  //      {
  //        res = "prEmail";
  //        return res;
  //      }
  //      var performacneEmailForBG = System.Web.HttpContext.Current.Session["performanceReviewEmailForBG"];
  //      if (performacneEmailForBG != null)
  //      {
  //        res = "prEmailForBG";
  //        return res;
  //      }
  //      var performanceEvaluationEmailForBG = System.Web.HttpContext.Current.Session["performanceEvaluationEmailForBG"];
  //      if (performanceEvaluationEmailForBG != null)
  //      {
  //        res = "prevalutionEmailForBG";
  //        return res;
  //      }
  //      var surveyEmail = System.Web.HttpContext.Current.Session["SurveyEmail"];
  //      if (surveyEmail != null)
  //      {
  //        res = "SurveyEmail";
  //        return res;
  //      }
  //      var proEmpEmail = System.Web.HttpContext.Current.Session["PromotedEmployeeReviewEmail"];
  //      if (proEmpEmail != null)
  //      {
  //        res = "proEmpEmail";
  //        return res;
  //      }

  //      var JCPEmail = System.Web.HttpContext.Current.Session["JobConfirmationEmail"];
  //      if (JCPEmail != null)
  //      {
  //        res = "jobConMail";
  //        return res;
  //      }

  //      var JobVacancySession = System.Web.HttpContext.Current.Session["selectedJobVacanchyForActionDataForBG"];
  //      if (JobVacancySession != null)
  //      {
  //        res = "jobVacancy";
  //        return res;
  //      }
  //    }
  //    else if (user == "CompanyInActive")
  //    {
  //      return user;
  //    }
  //    else
  //    {
  //      Session["CurrentUser"] = null;
  //    }
  //    res = "Success"; //For Audit trail
  //  }
  //  catch (Exception ex)
  //  {
  //    res = ex.Message;
  //    return res;
  //  }
  //  var struser = ((Users)(Session["CurrentUser"]));
  //  if (struser != null)
  //  {
  //    //Audittail
  //    var audit = hendler.GetAuditInfo(struser.UserId, struser.UserName + " is try to login", "Login", res);


  //    aService.SendAudit(audit);
  //  }
  //  return user.Split('^')[0];
  //}
  #endregion LoginFrom mvc


  //[HttpGet("getUserInfo")]
  [HttpGet(RouteConstants.GetUserInfo)]
  public async Task<IActionResult> GetUserInfo()
  {
    var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    var loginId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(loginId))
    {
      return StatusCode(StatusCodes.Status401Unauthorized, new { message = "User ID not found in token." });
    }

    var user = await _serviceManager.Users.GetUserByLoginIdAsync(loginId, false);
    if (user == null)
    {
      return StatusCode(StatusCodes.Status404NotFound, new { message = "User not found." });
    }

    var UserId = User.FindFirst("UserId")?.Value;
    var cacheKey = $"User_{user.UserId}";
    _memoryCache.Set(cacheKey, user, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(300) });

    user.Password = "";

    return Ok(user);
  }

  //[HttpPost("logout")]
  [HttpPost(RouteConstants.Logout)]
  [IgnoreMediaTypeValidation]
  public async Task<IActionResult> Logout()
  {
    try
    {
      var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

      await _serviceManager.TokenBlacklist.AddToBlacklistAsync(token);

      // Clear user-specific cache entries
      var userIdClaim = User.FindFirst("UserId")?.Value;
      if (!string.IsNullOrEmpty(userIdClaim))
      {
        var cacheKey = $"User_{userIdClaim}";
        if (_memoryCache.TryGetValue(cacheKey, out _))
        {
          _memoryCache.Remove(cacheKey); // Remove the specific cache entry
        }
      }

      // Clear the entire memory cache
      ClearMemoryCache();

      return Ok(new { message = "Logged out successfully." });
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred during logout." });
    }
  }

  private void ClearMemoryCache()
  {
    var field = typeof(MemoryCache).GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance);
    if (field != null)
    {
      var entries = field.GetValue(_memoryCache) as IDictionary;
      if (entries != null)
      {
        foreach (var key in entries.Keys.Cast<object>().ToList())
        {
          _memoryCache.Remove(key);
        }
      }
    }
  }
}
