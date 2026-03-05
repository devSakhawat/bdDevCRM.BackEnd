using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.AuthorizeAttribiutes;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServiceContract.Infrastructure;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions.BaseException;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Authentication;

/// <summary>
/// Authentication endpoints for login, logout, token refresh, and revocation.
/// Mix of [AllowAnonymous] and [AuthorizeUser] endpoints.
/// Clean controller - delegates all operations to services.
/// </summary>
public class AuthenticationController : BaseApiController
{
	private readonly IMemoryCache _memoryCache;
	private readonly ICookieManagementService _cookieService;
	private readonly IHttpContextService _httpContextService;
	private readonly ICacheManagementService _cacheService;

	public AuthenticationController(
		IServiceManager serviceManager,
		IMemoryCache memoryCache,
		ICookieManagementService cookieService,
		IHttpContextService httpContextService,
		ICacheManagementService cacheService) : base(serviceManager)
	{
		_memoryCache = memoryCache;
		_cookieService = cookieService;
		_httpContextService = httpContextService;
		_cacheService = cacheService;
	}

	[HttpPost(RouteConstants.Login)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	[AllowAnonymous]
	[IgnoreMediaTypeValidation]
	[Produces("application/json")]
	public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
	{
		// ============================================================================
		// STEP 1: Get User Data
		// ============================================================================

		var userDto = _serviceManager.Users.GetUserByLoginIdRaw(user.LoginId.Trim(), false);

		if (userDto == null)
			return Unauthorized(ApiResponseHelper.Unauthorized<object>("Invalid username or password"));

		// ============================================================================
		// STEP 2: Validate Login
		// ============================================================================
		LoginValidationResult validationResult = await _serviceManager.CustomAuthentication.ValidateUserLogin(user, userDto);

		if (!validationResult.IsSuccess)
		{
			return validationResult.Status switch
			{
				LoginValidationStatus.Inactive =>
					Unauthorized(ApiResponseHelper.Unauthorized<object>("Account is inactive")),

				LoginValidationStatus.Expired =>
					Unauthorized(ApiResponseHelper.Unauthorized<object>("Account has expired")),

				LoginValidationStatus.AccountLocked =>
					Unauthorized(ApiResponseHelper.Unauthorized<object>("Account is locked due to too many failed attempts")),

				LoginValidationStatus.PasswordChangeRequired =>
					Ok(ApiResponseHelper.Success(new { requirePasswordChange = true }, validationResult.Message)),

				_ => Unauthorized(ApiResponseHelper.Unauthorized<object>("Invalid username or password"))
			};
		}

		// ============================================================================
		// STEP 3: Generate Tokens
		// ============================================================================

		var tokenResponse = await _serviceManager.CustomAuthentication.CreateToken(user);

		// Set refresh token in HTTP-only cookie
		_cookieService.SetRefreshTokenCookie(tokenResponse.RefreshToken, tokenResponse.RefreshTokenExpiry);

		// ============================================================================
		// STEP 4: Cache User Data
		// ============================================================================
		userDto.Password = "";
		userDto.HrRecordId = userDto.EmployeeId;

		var cacheKey = $"User_{userDto.UserId}";
		var cacheOptions = new MemoryCacheEntryOptions()
			.SetSlidingExpiration(TimeSpan.FromHours(5))
			.SetAbsoluteExpiration(TimeSpan.FromHours(5));

		if (_memoryCache.TryGetValue(cacheKey, out _))
			_memoryCache.Remove(cacheKey);

		_memoryCache.Set(cacheKey, userDto, cacheOptions);

		// ============================================================================
		// STEP 5: Build Response
		// ============================================================================

		var response = new TokenResponseDto
		{
			AccessToken = tokenResponse.AccessToken,
			AccessTokenExpiry = tokenResponse.AccessTokenExpiry,
			RefreshTokenExpiry = tokenResponse.RefreshTokenExpiry,
			TokenType = tokenResponse.TokenType,
			ExpiresIn = tokenResponse.ExpiresIn,
			UserSession = validationResult.UserSession,
			Status = validationResult.Status.ToString(),
			IsSuccess = validationResult.IsSuccess,
		};

		return Ok(ApiResponseHelper.Success(response, validationResult.Message));
	}

	[HttpGet(RouteConstants.GetUserInfo)]
	[AuthorizeUser]
	public IActionResult GetUserInfo()
	{
		// CurrentUser is guaranteed by [AuthorizeUser] attribute
		var currentUser = CurrentUser;

		// Ensure HrRecordId is set
		if (currentUser.HrRecordId == null || currentUser.HrRecordId == 0)
			currentUser.HrRecordId = currentUser.EmployeeId;

		// Clear password for security
		currentUser.Password = "";

		return Ok(ApiResponseHelper.Success(currentUser, "User info retrieved"));
	}


	[HttpPost(RouteConstants.RefreshToken)]
	[AllowAnonymous]
	[IgnoreMediaTypeValidation]
	public async Task<IActionResult> RefreshToken()
	{
		// Get refresh token from cookie
		var refreshToken = _cookieService.GetRefreshToken();
		if (string.IsNullOrEmpty(refreshToken))
		{
			_cookieService.ClearRefreshTokenCookie();
			return Unauthorized(ApiResponseHelper.Unauthorized<object>("Refresh token not found"));
		}

		var ipAddress = _httpContextService.GetClientIpAddress();

		try
		{
			var tokenResponse = await _serviceManager.CustomAuthentication.RefreshTokenAsync(refreshToken, ipAddress);

			// Set new refresh token in cookie
			_cookieService.SetRefreshTokenCookie(tokenResponse.RefreshToken, tokenResponse.RefreshTokenExpiry);

			var response = new TokenResponseDto
			{
				AccessToken = tokenResponse.AccessToken,
				AccessTokenExpiry = tokenResponse.AccessTokenExpiry,
				RefreshTokenExpiry = tokenResponse.RefreshTokenExpiry,
				TokenType = tokenResponse.TokenType,
				ExpiresIn = tokenResponse.ExpiresIn,
				IsSuccess = true,
			};

			return Ok(ApiResponseHelper.Success(response, "Token refreshed successfully"));
		}
		catch (UnauthorizedException)
		{
			_cookieService.ClearRefreshTokenCookie();
			throw;
		}
	}

	[HttpPost(RouteConstants.RevokeToken)]
	[AllowAnonymous]
	[IgnoreMediaTypeValidation]
	public async Task<IActionResult> RevokeToken()
	{
		var refreshToken = _cookieService.GetRefreshToken();
		if (string.IsNullOrEmpty(refreshToken))
			return BadRequest(ApiResponseHelper.BadRequest<object>("No refresh token found"));

		var ipAddress = _httpContextService.GetClientIpAddress();

		var result = await _serviceManager.CustomAuthentication.RevokeTokenAsync(refreshToken, ipAddress);

		if (!result)
			return BadRequest(ApiResponseHelper.BadRequest<object>("Invalid or already revoked token"));

		_cookieService.ClearRefreshTokenCookie();

		return Ok(ApiResponseHelper.Success(result, "Token revoked successfully"));
	}

	[AuthorizeUser]
	[HttpPost(RouteConstants.Logout)]
	[AllowAnonymous]
	[IgnoreMediaTypeValidation]
	public async Task<IActionResult> Logout()
	{
		var userId = CurrentUserId;

		if (userId != 0)
		{
			var ipAddress = _httpContextService.GetClientIpAddress();
			await _serviceManager.CustomAuthentication.RevokeAllUserTokensAsync(userId, ipAddress);
		}

		// Clear user cache
		_cacheService.ClearUserCache(userId);

		// Clear the entire memory cache
		_cacheService.ClearAllCache();

		// Clear refresh token cookie
		_cookieService.ClearRefreshTokenCookie();

		return Ok(ApiResponseHelper.Success<object>(null, "Logged out successfully"));
	}
}
