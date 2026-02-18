using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Entities.Entities.Token;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions.BaseException;
using bdDevCRM.Utilities.Common;
using bdDevCRM.Utilities.OthersLibrary;
using bdDevs.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace bdDevCRM.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
	private readonly IRepositoryManager _repository;
	private readonly IConfiguration _configuration;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AuthenticationService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
	{
		_repository = repository;
		_configuration = configuration;
		_httpContextAccessor = httpContextAccessor;
	}

	/// <summary>
	/// Validate user credentials
	/// </summary>
	public bool ValidateUser(UserForAuthenticationDto userForAuth)
	{
		var user = _repository.Users.GetUserByLoginIdAsync(userForAuth.LoginId, trackChanges: false);

		if (user == null) return false;

		// Password validation logic here
		// TODO: Implement proper password validation
		// Example: BCrypt. Verify(userForAuth.Password, user.Password)
		return true; // Replace with actual password validation
	}

	public async Task<LoginValidationResult> ValidateUserLogin(UserForAuthenticationDto userForAuth, UsersDto userDB)
	{
		var systemSettings = await _repository.SystemSettings.GetSystemSettingsDataByCompanyId((int)userDB.CompanyId);
		//Users userEntity = new Users();
		Users userEntity = MyMapper.JsonClone<UsersDto, Users>(userDB);
    // ============================================================================
    // STEP 1: Check if User Exists
    // ============================================================================
    if (userDB == null || userDB.UserId == 0)
		{
			return new LoginValidationResult
			{
				IsSuccess = false,
				Status = LoginValidationStatus.Failed,
				Message = "User not found"
			};
		}

		// ============================================================================
		// STEP 2: Check if User is Active
		// ============================================================================

		if (!userDB.IsActive)
		{
			return new LoginValidationResult
			{
				IsSuccess = false,
				Status = LoginValidationStatus.Inactive,
				Message = "User account is inactive"
			};
		}

		// ============================================================================
		// STEP 3: Check if User is Expired
		// ============================================================================

		if (userDB.IsExpired.GetValueOrDefault())
		{
			return new LoginValidationResult
			{
				IsSuccess = false,
				Status = LoginValidationStatus.Expired,
				Message = "User account has expired"
			};
		}

		// ============================================================================
		// STEP 4: Validate Password
		// ============================================================================

		var isValidPassword = ValidationHelper.ValidateLoginPassword(userForAuth.Password, userDB.Password, true);

		if (!isValidPassword)
		{
			userDB.FailedLoginNo = (userDB.FailedLoginNo ?? 0) + 1;

			if (userDB.FailedLoginNo >= systemSettings.WrongAttemptNo)
			{
				userDB.IsActive = false;
			}

			// Always save failed login attempts
			//userEntity = MyMapper.JsonClone<UsersDto, Users>(userDB);
			_repository.Users.UpdateUser(userEntity);
			await _repository.SaveAsync();

			if (!userDB.IsActive)
			{
				return new LoginValidationResult
				{
					IsSuccess = false,
					Status = LoginValidationStatus.AccountLocked,
					Message = "Account locked due to too many failed login attempts"
				};
			}

			return new LoginValidationResult
			{
				IsSuccess = false,
				Status = LoginValidationStatus.Failed,
				Message = "Invalid password"
			};
		}

		// ============================================================================
		// STEP 5: Check Password Expiry (if enabled)
		// ============================================================================

		var passwordExpiryResult = await CheckPasswordExpiry(userEntity, systemSettings);
		if (!passwordExpiryResult.IsValid)
		{
			return new LoginValidationResult
			{
				IsSuccess = false,
				Status = LoginValidationStatus.PasswordChangeRequired,
				Message = passwordExpiryResult.Message
			};
		}

		// ============================================================================
		// STEP 6: Reset Failed Login Counter
		// ============================================================================

		userEntity.FailedLoginNo = 0;
		userEntity.LastLoginDate = DateTime.Now;
		_repository.Users.UpdateUser(userEntity);
		

		// ============================================================================
		// STEP 7: User IP and Device Information.
		// ============================================================================

		userEntity.FailedLoginNo = 0;
		userEntity.LastLoginDate = DateTime.Now;
		_repository.Users.UpdateUser(userEntity);


		// ============================================================================
		// STEP 7: Build User Session Data
		// ============================================================================

		var license = new bdDevsLicense();
    userDB = MyMapper.JsonClone<Users, UsersDto>(userEntity);

    var userSession = MapToUserSession(userDB);
		userSession.ExpiryDate = license.GetExpiryDate();
		userSession.LicenseUserCount = license.GetNumberOfUser();


		// ============================================================================
		// STEP 8: Return Success Result
		// ============================================================================

		return new LoginValidationResult
		{
			IsSuccess = true,
			Status = LoginValidationStatus.Success,
			Message = "Login successful",
			UserSession = userSession,
		};
	}


	/// <summary>
	/// Create JWT token with refresh token
	/// Access token and refresh token expiry times are configurable
	/// </summary>
	public TokenResponse CreateToken(UserForAuthenticationDto userForAuth)
	{
		var user = _repository.Users.GetUserByLoginIdAsync(userForAuth.LoginId, trackChanges: false);
		if (user == null) throw new UnauthorizedException("User not found");

		UsersDto usersDto = MyMapper.JsonClone<UsersRepositoryDto, UsersDto>(user);

		// 1. Generate access token (short-lived: 15 minutes)
		var accessToken = GenerateAccessToken(usersDto);
		var accessTokenExpiry = DateTime.UtcNow.AddMinutes(15);

		// 2. Generate refresh token (long-lived: 7 days)
		var refreshToken = GenerateRefreshToken();
		var refreshTokenExpiry = DateTime.UtcNow.AddHours(8);

		// 3. Save refresh token to database
		var refreshTokenEntity = new RefreshToken
		{
			UserId = user.UserId,
			Token = HashToken(refreshToken),
			ExpiryDate = refreshTokenExpiry,
			CreatedDate = DateTime.UtcNow,
			CreatedByIp = GetCurrentIpAddress(),
			IsRevoked = false
		};

		_repository.RefreshTokens.Create(refreshTokenEntity);
		_repository.SaveAsync();

		// 4. Return token response
		return new TokenResponse
		{
			AccessToken = accessToken,
			RefreshToken = refreshToken,
			AccessTokenExpiry = accessTokenExpiry,
			RefreshTokenExpiry = refreshTokenExpiry,
			TokenType = "Bearer",
			ExpiresIn = 900 // 15 minutes in seconds
		};
	}


	/// <summary>
	/// Refreshes the access token using the provided refresh token.
	/// 
	/// Flow:
	/// 1. Validate the refresh token (check if it exists in the database and is still valid).
	/// 2. Retrieve user information associated with the refresh token.
	/// 3. Generate a new access token and a new refresh token.
	/// 4. Revoke (invalidate) the old refresh token.
	/// 5. Save the new refresh token to the database.
	/// </summary>
	public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string ipAddress)
	{
		// 1. Hash incoming token for database lookup
		var hashedToken = HashToken(refreshToken);

		// 2. Find token in database
		var storedToken = await _repository.RefreshTokens.GetByIdAsync(predicate: t => t.Token == hashedToken, trackChanges: true);


		// 3. Validate token
		if (storedToken == null)
			throw new UnauthorizedException("Refresh token not found");

		// Token reuse detection (security breach)
		if (storedToken.IsRevoked)
		{
			// This token was already used - possible attack!
			// Revoke all tokens for this user as a security measure
			await RevokeAllUserTokensAsync(storedToken.UserId, ipAddress);
			throw new UnauthorizedException("Token reuse detected. All tokens have been revoked for security.");
		}

		if (!storedToken.IsActive)
			throw new UnauthorizedException("Refresh token is expired or revoked");

		// 4. Get user
		var user = await _repository.Users.GetByIdAsync(predicate: u => u.UserId == storedToken.UserId, trackChanges: false);
		if (user == null)
			throw new UnauthorizedException("User not found");
		UsersDto usersDto = MyMapper.JsonClone<Users, UsersDto>(user);

		// 5. Generate new tokens
		var newAccessToken = GenerateAccessToken(usersDto);
		var newRefreshToken = GenerateRefreshToken();
		var accessTokenExpiry = DateTime.UtcNow.AddMinutes(15);
		var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);

		// 6.  Revoke old refresh token
		storedToken.IsRevoked = true;
		storedToken.RevokedDate = DateTime.UtcNow;
		storedToken.ReplacedByToken = HashToken(newRefreshToken);

		// 7. Create new refresh token
		var newRefreshTokenEntity = new RefreshToken
		{
			UserId = user.UserId,
			Token = HashToken(newRefreshToken),
			ExpiryDate = refreshTokenExpiry,
			CreatedDate = DateTime.UtcNow,
			CreatedByIp = ipAddress,
			IsRevoked = false
		};


		_repository.RefreshTokens.Create(newRefreshTokenEntity);
		await _repository.SaveAsync();

		// 8. Return new token response
		return new TokenResponse
		{
			AccessToken = newAccessToken,
			RefreshToken = newRefreshToken,
			AccessTokenExpiry = accessTokenExpiry,
			RefreshTokenExpiry = refreshTokenExpiry,
			TokenType = "Bearer",
			ExpiresIn = 900
		};
	}

	/// <summary>
	/// Revokes a refresh token manually.
	/// 
	/// Use cases:
	/// - When the user logs out.
	/// - When there is a security concern (e.g., suspected token compromise).
	/// </summary>
	public async Task<bool> RevokeTokenAsync(string refreshToken, string ipAddress)
	{
		var hashedToken = HashToken(refreshToken);
		var storedToken = await _repository.RefreshTokens.GetByIdAsync(predicate: t => t.Token == hashedToken, trackChanges: true);

		if (storedToken == null || !storedToken.IsActive)
			return false;

		// Revoke token
		storedToken.IsRevoked = true;
		storedToken.RevokedDate = DateTime.UtcNow;

		await _repository.SaveAsync();
		return true;
	}

	/// <summary>
	/// Removes expired refresh tokens from the database.
	/// Deletes all refresh tokens that have passed their expiration time.
	/// 
	/// Use cases:
	/// - Scheduled cleanup job (e.g., daily or weekly).
	/// - General database maintenance to reduce clutter and improve performance.
	/// 
	/// Logic:
	/// 1. GetExpiredTokensAsync() — Fetch all expired refresh tokens.
	/// 2. BulkDelete() — Delete all fetched tokens in a single efficient operation.
	/// 3. SaveAsync() — Persist the changes to the database.
	/// </summary>
	public async Task RemoveExpiredTokensAsync()
	{
		// 1.  Fetch all expired tokens
		var expiredTokens = await _repository.RefreshTokens.GetExpiredTokensAsync();

		// 2. Check if any tokens found
		if (expiredTokens == null || !expiredTokens.Any())
		{
			Console.WriteLine("No expired tokens found to delete");
			return;
		}

		Console.WriteLine($"Found {expiredTokens.Count()} expired tokens.  Deleting.. .");

		// 3.  Bulk delete (efficient way to delete multiple records)
		_repository.RefreshTokens.BulkDelete(expiredTokens);

		// 4. Save changes to database
		await _repository.SaveAsync();

		Console.WriteLine($"✅ Successfully deleted {expiredTokens.Count()} expired tokens");
	}

	/// <summary>
	/// Revokes all active refresh tokens for a user (e.g., on logout from all devices or password change)
	/// </summary>
	public async Task RevokeAllUserTokensAsync(int userId, string ipAddress)
	{
		// Note: ipAddress can be logged for audit trail purposes if needed
		await _repository.RefreshTokens.RevokeAllTokensByUserIdAsync(userId);
		await _repository.SaveAsync();
	}



	// ============================================
	// PRIVATE HELPER METHODS
	// ============================================
	/// <summary>
	/// Check if password has expired and needs to be changed
	/// </summary>
	private async Task<(bool IsValid, string Message)> CheckPasswordExpiry(Users user, SystemSettings systemSettings)
	{
		if (systemSettings.IsPasswordChange != 1)
		{
			return (true, string.Empty);
		}

		var currentDate = DateTime.Now;
		var passwordHistory = await _repository.Users.GetPasswordHistory((int)user.UserId, (int)systemSettings.OldPassUseRestriction);

		DateTime lastPasswordChange;

		if (!passwordHistory.Any())
		{
			lastPasswordChange = user.CreatedDate;
		}
		else
		{
			lastPasswordChange = passwordHistory.First().PasswordChangeDate;
		}

		var daysSinceChange = (currentDate - lastPasswordChange).Days;

		// Check if password expired
		if (daysSinceChange > systemSettings.PassExpiryDays)
		{
			user.IsExpired = true;
			_repository.Users.UpdateUser(user);

			return (false, "Password has expired. Please change your password.");
		}

		// Check if password change required on first login
		//if (systemSettings.ChangePassFirstLogin && user.IsFirstLoginEnable) --5.6.c
		if ((bool)systemSettings.ChangePassFirstLogin )
		{
			return (false, "Password change required on first login");
		}

		// Check if password change is due
		if (user.LastUpdatedDate != DateTime.MinValue)
		{
			var daysSinceUpdate = (currentDate - (DateTime)user.LastUpdatedDate).Days;

			if (daysSinceUpdate > systemSettings.ChangePassDays)
			{
				return (false, "Password change is required");
			}
		}

		return (true, string.Empty);
	}

  /// <summary>
  /// Map user DTO to session data
  /// Parameter will be UsersDto because Users Has no FullLogoPath and few property.
  /// </summary>
  private UserSessionData MapToUserSession(UsersDto user)
	{
		return new UserSessionData
		{
			UserId = (int)user.UserId,
			LoginId = user.LoginId,
			UserName = user.UserName,
			CompanyId = (int)user.CompanyId,
			EmployeeId = (int)user.EmployeeId,
			CompanyName = user.CompanyName,
			FullLogoPath = user.FullLogoPath,
			LogHourEnable = (bool)user.LogHourEnable,
			FiscalYearStart = (int)user.FiscalYearStart,
			BranchId = (int)user.BranchId,
			AccessParentCompany = (int)user.AccessParentCompany,
			ProfilePicture = user.ProfilePicture,
			Gender = (int)user.Gender,
			DefaultDashboard = (int)user.DefaultDashboard,
			Employee_Id = user.Employee_Id,
			DepartmentId = (int)user.DepartmentId
		};
	}

	/// <summary>
	/// Generate JWT access token
	/// JWT access token
	/// </summary>
	private string GenerateAccessToken(UsersDto user)
	{
		var signingCredentials = GetSigningCredentials();
		var claims = GetClaims(user);
		var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}

	/// <summary>
	/// Get signing credentials for JWT
	/// Create JWT signing for credentials
	/// </summary>
	private SigningCredentials GetSigningCredentials()
	{
		var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
		var secret = new SymmetricSecurityKey(key);

		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}

	/// <summary>
	/// Gets the list of claims to be included in the JWT token.
	/// Defines the user-related identity and custom claims embedded in the token.
	/// </summary>
	/// <param name="user">The user data transfer object containing user details.</param>
	/// <returns>A list of <see cref="Claim"/> objects representing user identity and metadata.</returns>
	private List<Claim> GetClaims(UsersDto user)
	{
		return new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.LoginId),
			new Claim("UserId", user.UserId.ToString()),
			new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
			new Claim(ClaimTypes.Email, user.EmailAddress ?? string.Empty)
		};
	}

	/// <summary>
	/// Generates the JWT security token configuration with specified claims and signing credentials.
	/// Configures issuer, audience, expiration, and cryptographic signing.
	/// </summary>
	/// <param name="signingCredentials">The credentials used to sign the token (e.g., RSA or HMAC key).</param>
	/// <param name="claims">The list of claims to embed in the token.</param>
	/// <returns>A configured <see cref="JwtSecurityToken"/> instance.</returns>
	private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
	{
		return new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.UtcNow.AddMinutes(15), // Short-lived access token (15 minutes)
			signingCredentials: signingCredentials
		);
	}

	/// <summary>
	/// Generates a cryptographically secure random refresh token.
	/// Used to issue new long-lived tokens during authentication or token refresh.
	/// </summary>
	/// <returns>A Base64-encoded 256-bit (32-byte) random string.</returns>
	private string GenerateRefreshToken()
	{
		var randomNumber = new byte[32]; // 256 bits
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber)
			.Replace('+', '-') // URL-safe Base64: replace + with -
			.Replace('/', '_') // URL-safe Base64: replace / with _
			.TrimEnd('=');     // Remove padding (optional, but common for tokens)
	}

	/// <summary>
	/// Hashes a token using SHA-256 for secure storage.
	/// Tokens (e.g., refresh tokens) are stored in hashed form to prevent exposure in case of database breach.
	/// </summary>
	/// <param name="token">The plain-text token to hash.</param>
	/// <returns>A Base64-encoded SHA-256 hash of the input token.</returns>
	/// <exception cref="ArgumentException">Thrown if <paramref name="token"/> is null or whitespace.</exception>
	private string HashToken(string token)
	{
		if (string.IsNullOrWhiteSpace(token))
			throw new ArgumentException("Token cannot be null or empty.", nameof(token));

		using var sha256 = SHA256.Create();
		var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
		return Convert.ToBase64String(hashedBytes);
	}



	/// <summary>
	/// Retrieves the client's IP address from the current HTTP request.
	/// Attempts to extract the real client IP (e.g., from X-Forwarded-For) in reverse-proxy scenarios if configured.
	/// Falls back to the direct remote IP or "0.0.0.0" if unavailable.
	/// </summary>
	/// <returns>The client IP address as a string; defaults to "0.0.0.0" if undetermined.</returns>
	private string GetCurrentIpAddress()
	{
		// Consider extending this to support X-Forwarded-For / X-Real-IP headers
		// if your app runs behind a reverse proxy (e.g., nginx, load balancer).
		var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress;

		// Handle IPv4-mapped IPv6 addresses (e.g., "::ffff:192.168.1.1" → "192.168.1.1")
		if (ipAddress?.IsIPv4MappedToIPv6 == true)
			ipAddress = ipAddress.MapToIPv4();

		return ipAddress?.ToString() ?? "0.0.0.0";
	}

}