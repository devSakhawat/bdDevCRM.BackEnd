namespace bdDevCRM.Entities.Entities;

public class TokenBlacklist
{
  public Guid TokenId { get; set; }
  public string Token { get; set; } // The JWT token
  public DateTime ExpiryDate { get; set; } // Token's expiration date
}
