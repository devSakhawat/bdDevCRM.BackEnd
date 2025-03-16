namespace bdDevCRM.RepositoryDtos.Core.SystemAdmin;

public class TokenBlacklistRepositoryDto
{
  public int TokenId { get; set; }
  public string Token { get; set; } // The JWT token
  public DateTime ExpiryDate { get; set; } // Token's expiration date
}
