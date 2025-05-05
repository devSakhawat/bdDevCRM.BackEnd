namespace bdDevCRM.RepositoryDtos.Core.HR;

public class PasswordHistoryRepositoryDto
{

  public int HistoryId { get; set; }
  public int UserId { get; set; }
  public string OldPassword { get; set; }
  public DateTime PasswordChangeDate { get; set; }
}
