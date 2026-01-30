using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.AuthorizeAttribiutes;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace bdDevCRM.Presentation.Controllers.Authentication;

//[Route("api/[controller]")]
//[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthenticationController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _memoryCache;

  public AuthenticationController(IServiceManager serviceManager, IMemoryCache memoryCache) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
    _memoryCache = memoryCache;
  }

	[HttpPost(RouteConstants.Login)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	[AllowAnonymous]
	[IgnoreMediaTypeValidation]
	public IActionResult Authenticate([FromBody] UserForAuthenticationDto user)
	{
		if (!_serviceManager.CustomAuthentication.ValidateUser(user))
			throw new UsernamePasswordMismatchException();

		var tokenResponse = _serviceManager.CustomAuthentication.CreateToken(user);

		// Set refresh token in HTTP-only cookie
		SetRefreshTokenCookie(tokenResponse.RefreshToken, tokenResponse.RefreshTokenExpiry);

		var userDto = _serviceManager.Users.GetUserByLoginIdAsync(user.LoginId.Trim(), false);

		if (userDto != null)
		{
			userDto.Password = "";
            userDto.HrRecordId = userDto.EmployeeId;

			var cacheKey = $"User_{userDto.UserId}";
			var cacheOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromHours(5))
				.SetAbsoluteExpiration(TimeSpan.FromHours(5));

			if (_memoryCache.TryGetValue(cacheKey, out _))
				_memoryCache.Remove(cacheKey);

			_memoryCache.Set(cacheKey, userDto, cacheOptions);
		}

		// Return response without exposing refresh token
		var response = new
		{
			AccessToken = tokenResponse.AccessToken,
			AccessTokenExpiry = tokenResponse.AccessTokenExpiry,
			TokenType = tokenResponse.TokenType,
			ExpiresIn = tokenResponse.ExpiresIn
		};

		return Ok(ResponseHelper.Success(response, "Login successful"));
	}

	[HttpGet(RouteConstants.GetUserInfo)]
	[AuthorizeUser]
	public IActionResult GetUserInfo()
	{
		var currentUser = HttpContext.GetCurrentUser();

        if (currentUser == null)
        {
			var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			var loginId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(loginId)) return StatusCode(StatusCodes.Status401Unauthorized, new { message = "User ID not found in token." });

			// UsersDto
			UsersDto? userDto = _serviceManager.Users.GetUserByLoginIdAsync(loginId, false);
			if (userDto == null) return StatusCode(StatusCodes.Status404NotFound, new { message = "User not found." });
			userDto.Password = "";
			userDto.HrRecordId = userDto.EmployeeId;
			var UserId = User.FindFirst("UserId")?.Value;
			var cacheKey = $"User_{userDto.UserId}";
			// Check if the user is already in the cache then destroy the cache
			if (_memoryCache.TryGetValue(cacheKey, out _)) _memoryCache.Remove(cacheKey);

			// Set the user in the cache with a 5-hours expiration
			var cacheEntryOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromHours(5))
				.SetAbsoluteExpiration(TimeSpan.FromHours(5));
			_memoryCache.Set(cacheKey, userDto, cacheEntryOptions);
			//_memoryCache.Set(cacheKey, user, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5) });


			userDto.Password = "";
			return Ok(ResponseHelper.Success(currentUser, "User info retrieved"));
		}
        if (currentUser.HrRecordId == null || currentUser.HrRecordId == 0) currentUser.HrRecordId = currentUser.EmployeeId;

		// Password clear (security)
		currentUser.Password = "";

		return Ok(ResponseHelper.Success(currentUser, "User info retrieved"));
	}


	[HttpPost(RouteConstants.RefreshToken)]
  [AllowAnonymous]
  [IgnoreMediaTypeValidation]
  public async Task<IActionResult> RefreshToken()
  {
    // Get refresh token from cookie
    if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
      return Unauthorized(ResponseHelper.Unauthorized("Refresh token not found"));

    var ipAddress = GetClientIpAddress();
    
    var tokenResponse = await _serviceManager.CustomAuthentication.RefreshTokenAsync(refreshToken, ipAddress);

    // Set new refresh token in cookie
    SetRefreshTokenCookie(tokenResponse.RefreshToken, tokenResponse.RefreshTokenExpiry);

    // Return new access token
    var response = new
    {
      AccessToken = tokenResponse.AccessToken,
      AccessTokenExpiry = tokenResponse.AccessTokenExpiry,
      TokenType = tokenResponse.TokenType,
      ExpiresIn = tokenResponse.ExpiresIn
    };

    return Ok(ResponseHelper.Success(response, "Token refreshed successfully"));
  }

  [HttpPost(RouteConstants.RevokeToken)]
  [AuthorizeUser]
  [IgnoreMediaTypeValidation]
  public async Task<IActionResult> RevokeToken()
  {
    if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
      return BadRequest(ResponseHelper.BadRequest("No refresh token found"));

    var ipAddress = GetClientIpAddress();
    
    var result = await _serviceManager.CustomAuthentication.RevokeTokenAsync(refreshToken, ipAddress);

    if (!result)
      return BadRequest(ResponseHelper.BadRequest("Invalid or already revoked token"));

    ClearRefreshTokenCookie();

    return Ok(ResponseHelper.Success<object>(null, "Token revoked successfully"));
  }



	//[HttpGet(RouteConstants.GetUserInfo)]
	//[AllowAnonymous]
	//public IActionResult GetUserInfo()
	//{
	//  var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
	//  var loginId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	//  if (string.IsNullOrEmpty(loginId)) return StatusCode(StatusCodes.Status401Unauthorized, new { message = "User ID not found in token." });

	//  // UsersDto
	//  UsersDto? user = _serviceManager.Users.GetUserByLoginIdAsync(loginId, false);
	//  if (user == null) return StatusCode(StatusCodes.Status404NotFound, new { message = "User not found." });

	//  var UserId = User.FindFirst("UserId")?.Value;
	//  var cacheKey = $"User_{user.UserId}";
	//  // Check if the user is already in the cache then destroy the cache
	//  if (_memoryCache.TryGetValue(cacheKey, out _)) _memoryCache.Remove(cacheKey);

	//  // Set the user in the cache with a 5-hours expiration
	//  var cacheEntryOptions = new MemoryCacheEntryOptions()
	//      .SetSlidingExpiration(TimeSpan.FromHours(5))
	//      .SetAbsoluteExpiration(TimeSpan.FromHours(5));
	//  _memoryCache.Set(cacheKey, user, cacheEntryOptions);
	//  //_memoryCache.Set(cacheKey, user, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5) });


	//  user.Password = "";
	//  return Ok(user);
	//}


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


	//[HttpPost("logout")]
	[HttpPost(RouteConstants.Logout)]
  [AuthorizeUser]
  [IgnoreMediaTypeValidation]
  public async Task<IActionResult> Logout()
  {
    try
    {
      var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

      await _serviceManager.TokenBlacklist.AddToBlacklistAsync(token);

      var userId = HttpContext.GetUserId();
      
      if (userId != 0)
      {
        var ipAddress = GetClientIpAddress();
        
        // Revoke all user tokens
        await _serviceManager.CustomAuthentication.RevokeAllUserTokensAsync(userId, ipAddress);
      }

      // Clear cookie
      ClearRefreshTokenCookie();

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

      return Ok(ResponseHelper.Success<object>(null, "Logged out successfully"));
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred during logout." });
    }
  }

  private void ClearMemoryCache()
  {
    var memCache = _memoryCache as MemoryCache;
    if (memCache == null) return;

    var coherentState = typeof(MemoryCache).GetProperty("CoherentState",
        BindingFlags.NonPublic | BindingFlags.Instance);

    var coherentStateValue = coherentState?.GetValue(memCache);
    if (coherentStateValue == null) return;

    var entriesCollection = coherentStateValue.GetType()
        .GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);

    var cacheItems = entriesCollection?.GetValue(coherentStateValue) as IDictionary;
    if (cacheItems == null) return;

    foreach (var key in cacheItems.Keys.Cast<object>().ToList())
    {
      _memoryCache.Remove(key);
    }
  }


  private void ClearnAllOfTheMemoryCache()
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


  [HttpGet("test-token")]
  [AllowAnonymous]
  public IActionResult TestTokenGeneration()
  {
    try
    {
      // Create a test user for token generation
      var testUser = new UserForAuthenticationDto
      {
        LoginId = "admin", // Use a known test user
        Password = "abcd1234" // This should match the actual password in your system
      };

      if (_serviceManager.CustomAuthentication.ValidateUser(testUser))
      {
        var token = _serviceManager.CustomAuthentication.CreateToken(testUser);

        var result = new
        {
          token = token,
          message = "Test token generated successfully",
          issuer = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value
        };
        return Ok(result);
      }
      else
      {
        return BadRequest(new { message = "Test user validation failed" });
      }
    }
    catch (Exception ex)
    {
      return StatusCode(500, new { message = $"Error generating test token: {ex.Message}" });
    }
  }

  [HttpPost("verify-token")]
  [AllowAnonymous]
  public IActionResult VerifyToken([FromBody] TokenVerificationRequest request)
  {
    try
    {
      if (string.IsNullOrEmpty(request.Token))
      {
        return BadRequest(new { message = "Token is required" });
      }

      var handler = new JwtSecurityTokenHandler();

      // First, decode the token without validation to see its structure
      var jsonToken = handler.ReadJwtToken(request.Token);

      // Enhanced debugging information
      var tokenInfo = new
      {
        header = new
        {
          algorithm = jsonToken.Header.Alg,
          type = jsonToken.Header.Typ
        },
        payload = new
        {
          claims = jsonToken.Claims.Select(c => new { c.Type, c.Value }).ToList(),
          issuer = jsonToken.Issuer,
          audience = jsonToken.Audiences.FirstOrDefault(),
          issuedAt = jsonToken.IssuedAt,
          validFrom = jsonToken.ValidFrom,
          validTo = jsonToken.ValidTo,
          notBefore = jsonToken.Payload.Nbf,
          expiration = jsonToken.Payload.Exp,
          // Add more detailed debugging
          hasExpiration = jsonToken.Payload.Exp != null,
          expirationTimestamp = jsonToken.Payload.Exp,
          currentUtcTime = DateTime.Now, // FIXED: Use Now consistently
          isExpiredNow = jsonToken.ValidTo < DateTime.Now // FIXED: Use Now consistently
        }
      };

      // Check if token has proper expiration before validation
      if (jsonToken.ValidTo == DateTime.MinValue || jsonToken.Payload.Exp == null)
      {
        return Ok(new
        {
          isValid = false,
          message = "Token is missing expiration time",
          error = "Token does not contain a valid expiration claim",
          tokenInfo = tokenInfo,
          debugInfo = new
          {
            message = "Token was created without proper expiration time. Check JWT generation logic.",
            suggestedFix = "Ensure 'expires' parameter is properly set in JwtSecurityToken constructor"
          }
        });
      }

      // Now try to validate the token with proper configuration
      var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
      var secretKey = configuration["bdDevsJWT:SecretKey"];
      var issuer = configuration["bdDevsJWT:Issuer"];
      var audience = configuration["bdDevsJWT:Audience"];

      var validationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = "https://localhost:7145", // ⬅️ এইটা null হলে validation fail

        ValidateAudience = true,
        ValidAudience = "https://localhost:7145",

        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(configuration["bdDevsJWT:SecretKey"])
    ),
        ClockSkew = TimeSpan.FromMinutes(5)
      };


      SecurityToken validatedToken;
      var principal = handler.ValidateToken(request.Token, validationParameters, out validatedToken);

      return Ok(new
      {
        isValid = true,
        message = "Token is valid",
        tokenInfo = tokenInfo,
        validationResult = new
        {
          identity = principal.Identity.Name,
          claims = principal.Claims.Select(c => new { c.Type, c.Value }).ToList()
        }
      });
    }
    catch (SecurityTokenExpiredException ex)
    {
      return Ok(new
      {
        isValid = false,
        message = "Token has expired",
        error = ex.Message,
        tokenInfo = GetTokenInfoSafely(request.Token)
      });
    }
    catch (SecurityTokenInvalidSignatureException ex)
    {
      return Ok(new
      {
        isValid = false,
        message = "Token signature is invalid",
        error = ex.Message,
        tokenInfo = GetTokenInfoSafely(request.Token)
      });
    }
    catch (SecurityTokenValidationException ex)
    {
      return Ok(new
      {
        isValid = false,
        message = "Token validation failed",
        error = ex.Message,
        tokenInfo = GetTokenInfoSafely(request.Token),
        debugInfo = new
        {
          possibleCauses = new[]
          {
            "Token missing expiration time (expires parameter not set)",
            "Invalid JWT configuration in appsettings.json",
            "Clock skew issues between token generation and validation",
            "Token was not created with proper JwtSecurityToken constructor parameters"
          }
        }
      });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new
      {
        message = "Error verifying token",
        error = ex.Message,
        tokenInfo = GetTokenInfoSafely(request.Token)
      });
    }
  }

  private object GetTokenInfoSafely(string token)
  {
    try
    {
      var handler = new JwtSecurityTokenHandler();
      var jsonToken = handler.ReadJwtToken(token);

      return new
      {
        header = new
        {
          algorithm = jsonToken.Header.Alg,
          type = jsonToken.Header.Typ
        },
        payload = new
        {
          claims = jsonToken.Claims.Select(c => new { c.Type, c.Value }).ToList(),
          issuer = jsonToken.Issuer,
          audience = jsonToken.Audiences.FirstOrDefault(),
          issuedAt = jsonToken.IssuedAt,
          validFrom = jsonToken.ValidFrom,
          validTo = jsonToken.ValidTo,
          isExpired = jsonToken.ValidTo < DateTime.Now, // FIXED: Use Now consistently
          // Enhanced debugging information
          hasExpirationClaim = jsonToken.Payload.Exp != null,
          expirationTimestamp = jsonToken.Payload.Exp,
          notBeforeTimestamp = jsonToken.Payload.Nbf,
          currentUtcTime = DateTime.Now, // FIXED: Use Now consistently
          tokenAge = DateTime.Now - jsonToken.ValidFrom // FIXED: Use Now consistently
        }
      };
    }
    catch (Exception ex)
    {
      return new
      {
        error = "Could not decode token",
        exception = ex.Message
      };
    }
  }

  [HttpGet("jwt-config")]
  [AllowAnonymous]
  public IActionResult GetJwtConfiguration()
  {
    try
    {
      var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

      var jwtConfig = new
      {
        Issuer = configuration["bdDevsJWT:Issuer"],
        Audience = configuration["bdDevsJWT:Audience"],
        ExpiryInMinutes = configuration["bdDevsJWT:ExpiryInMinutes"],
        SecretKeyExists = !string.IsNullOrEmpty(configuration["bdDevsJWT:SecretKey"]),
        SecretKeyLength = configuration["bdDevsJWT:SecretKey"]?.Length ?? 0,
        CurrentUtcTime = DateTime.Now // FIXED: Use Now consistently
      };

      return Ok(new
      {
        message = "JWT Configuration loaded successfully",
        configuration = jwtConfig,
        isConfigurationValid = !string.IsNullOrEmpty(jwtConfig.Issuer) &&
                               !string.IsNullOrEmpty(jwtConfig.Audience) &&
                               !string.IsNullOrEmpty(jwtConfig.ExpiryInMinutes) &&
                               jwtConfig.SecretKeyExists
      });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new
      {
        message = "Error loading JWT configuration",
        error = ex.Message
      });
    }
  }

  // Helper methods for cookie management and IP address retrieval
  private void SetRefreshTokenCookie(string refreshToken, DateTime expiry)
  {
    var cookieOptions = new CookieOptions
    {
      HttpOnly = true,
      Secure = true, // Set to true in production with HTTPS
      SameSite = SameSiteMode.Strict,
      Expires = expiry,
      Path = "/",
      IsEssential = true
    };
    
    Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
  }

  private void ClearRefreshTokenCookie()
  {
    Response.Cookies.Delete("refreshToken", new CookieOptions
    {
      HttpOnly = true,
      Secure = true,
      SameSite = SameSiteMode.Strict,
      Path = "/"
    });
  }

  private string GetClientIpAddress()
  {
    var forwardedFor = Request.Headers["X-Forwarded-For"].FirstOrDefault();
    if (!string.IsNullOrEmpty(forwardedFor))
      return forwardedFor.Split(',')[0].Trim();
    
    var realIp = Request.Headers["X-Real-IP"].FirstOrDefault();
    if (!string.IsNullOrEmpty(realIp))
      return realIp;
    
    return HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "Unknown";
  }

}
