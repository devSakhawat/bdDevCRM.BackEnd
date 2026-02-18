using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.AuthorizeAttribiutes;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions.BaseException;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
	private readonly IWebHostEnvironment _environment;

	public AuthenticationController(IServiceManager serviceManager, IMemoryCache memoryCache, IWebHostEnvironment environment) : base(serviceManager)
	{
		//_serviceManager = serviceManager;
		_memoryCache = memoryCache;
		_environment = environment;
	}

	[HttpPost(RouteConstants.Login)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	[AllowAnonymous]
	[IgnoreMediaTypeValidation]
	public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
	{
		// ============================================================================
		// STEP 1: Get User Data
		// ============================================================================

		var userDto = _serviceManager.Users.GetUserByLoginIdAsync(user.LoginId.Trim(), false);
		if (userDto == null)
		{
			return Unauthorized(ResponseHelper.Unauthorized("Invalid username or password"));
		}

		// ============================================================================
		// STEP 2: Validate Login
		// ============================================================================
		LoginValidationResult validationResult = await _serviceManager.CustomAuthentication.ValidateUserLogin(user, userDto);

		if (!validationResult.IsSuccess)
		{
			return validationResult.Status switch
			{
				LoginValidationStatus.Inactive =>
					Unauthorized(ResponseHelper.Unauthorized("Account is inactive")),

				LoginValidationStatus.Expired =>
					Unauthorized(ResponseHelper.Unauthorized("Account has expired")),

				LoginValidationStatus.AccountLocked =>
					Unauthorized(ResponseHelper.Unauthorized("Account is locked due to too many failed attempts")),

				LoginValidationStatus.PasswordChangeRequired =>
					Ok(ResponseHelper.Success(new { requirePasswordChange = true }, validationResult.Message)),

				_ =>
					Unauthorized(ResponseHelper.Unauthorized("Invalid username or password"))
			};
		}

		// ============================================================================
		// STEP 3: Generate Tokens
		// ============================================================================

		var tokenResponse = _serviceManager.CustomAuthentication.CreateToken(user);

		// Set refresh token in HTTP-only cookie
		SetRefreshTokenCookie(tokenResponse.RefreshToken, tokenResponse.RefreshTokenExpiry);
		// ============================================================================
		// STEP 4: Cache User Data
		// ============================================================================
		userDto.Password = "";
		userDto.HrRecordId = userDto.EmployeeId;

		var cacheKey = $"User_{userDto.UserId}";
		var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(5)).SetAbsoluteExpiration(TimeSpan.FromHours(5));

		if (_memoryCache.TryGetValue(cacheKey, out _))
		{
			_memoryCache.Remove(cacheKey);
		}

		_memoryCache.Set(cacheKey, userDto, cacheOptions);

		// ============================================================================
		// STEP 5: Build Response
		// ============================================================================

		var response = new
		{
			AccessToken = tokenResponse.AccessToken,
			AccessTokenExpiry = tokenResponse.AccessTokenExpiry,
			TokenType = tokenResponse.TokenType,
			ExpiresIn = tokenResponse.ExpiresIn,
			UserSession = validationResult.UserSession,
			Status = validationResult.Status.ToString()
		};

		return Ok(ResponseHelper.Success(response, validationResult.Message));
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
		{
			ClearRefreshTokenCookie(); // Ensure cleanup on missing/corrupted cookie
			return Unauthorized(ResponseHelper.Unauthorized("Refresh token not found"));
		}

		var ipAddress = GetClientIpAddress();

		try
		{
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
		catch (UnauthorizedException)
		{
			ClearRefreshTokenCookie(); // Clear cookie on any auth failure
			throw;
		}
	}

	[HttpPost(RouteConstants.RevokeToken)]
	[AllowAnonymous]
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


	//[HttpPost("logout")]
	[HttpPost(RouteConstants.Logout)]
	[AllowAnonymous]
	[IgnoreMediaTypeValidation]
	public async Task<IActionResult> Logout()
	{
		try
		{
			var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

			// Only blacklist if token is provided
			if (!string.IsNullOrEmpty(token))
			{
				await _serviceManager.TokenBlacklist.AddToBlacklistAsync(token);
			}

			var userId = HttpContext.GetUserId();

			if (userId != 0)
			{
				var ipAddress = GetClientIpAddress();

				// Revoke all user tokens
				try
				{
					await _serviceManager.CustomAuthentication.RevokeAllUserTokensAsync(userId, ipAddress);
				}
				catch (Exception ex)
				{
					// Log error but continue with logout process
					// Note: Access token is already blacklisted, so partial failure is acceptable
					Console.WriteLine($"Failed to revoke refresh tokens during logout: {ex.Message}");
				}
			}

			// Clear user cache
			AuthorizeUserAttribute.ClearUserCache(_memoryCache, userId);

			// Clear the entire memory cache
			ClearMemoryCache();

			ClearRefreshTokenCookie();

			return Ok(ResponseHelper.Success<object>(null, "Logged out successfully"));
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred during logout." });
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
          isExpired = jsonToken.ValidTo < DateTime.Now,
          // Enhanced debugging information
          hasExpirationClaim = jsonToken.Payload.Exp != null,
          expirationTimestamp = jsonToken.Payload.Exp,
          notBeforeTimestamp = jsonToken.Payload.Nbf,
          currentUtcTime = DateTime.Now,
          tokenAge = DateTime.Now - jsonToken.ValidFrom
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

  // Helper methods for cookie management and IP address retrieval
  private void SetRefreshTokenCookie(string refreshToken, DateTime expiry)
	{
		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Secure = !_environment.IsDevelopment(), // Allow HTTP in development, require HTTPS in production
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
			Secure = !_environment.IsDevelopment(),
			SameSite = SameSiteMode.Strict,
			Path = "/"
		});
	}

	private string GetClientIpAddress()
	{
		// Note: In production, validate that requests come from trusted proxies before using
		// X-Forwarded-For header to prevent IP spoofing attacks
		var forwardedFor = Request.Headers["X-Forwarded-For"].FirstOrDefault();
		if (!string.IsNullOrEmpty(forwardedFor))
			return forwardedFor.Split(',')[0].Trim();

		var realIp = Request.Headers["X-Real-IP"].FirstOrDefault();
		if (!string.IsNullOrEmpty(realIp))
			return realIp;

		return HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "Unknown";
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


}
