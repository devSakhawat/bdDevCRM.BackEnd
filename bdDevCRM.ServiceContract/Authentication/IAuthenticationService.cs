using bdDevCRM.Entities.Entities.Token;
using bdDevCRM.Shared.DataTransferObjects.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServiceContract.Authentication;

public interface IAuthenticationService
{
	/// <summary>
	/// Validate user credentials
	/// </summary>
	bool ValidateUser(UserForAuthenticationDto userForAuth);

	/// <summary>
	/// Validate user credentials
	/// </summary>
	Task<LoginValidationResult> ValidateUserLogin(UserForAuthenticationDto userForAuth, UsersDto user);

	/// <summary>
	/// Create JWT token with refresh token
	/// </summary>
	TokenResponse CreateToken(UserForAuthenticationDto userForAuth);

	/// <summary>
	/// Refresh access token using refresh token
	/// </summary>
	Task<TokenResponse> RefreshTokenAsync(string refreshToken, string ipAddress);

	/// <summary>
	/// Revoke refresh token
	/// </summary>
	Task<bool> RevokeTokenAsync(string refreshToken, string ipAddress);

	/// <summary>
	/// Remove expired refresh tokens from database
	/// </summary>
	Task RemoveExpiredTokensAsync();

	/// <summary>
	/// Revokes all active refresh tokens for a user (e.g., on logout from all devices or password change)
	/// </summary>
	Task RevokeAllUserTokensAsync(int userId, string ipAddress);

}