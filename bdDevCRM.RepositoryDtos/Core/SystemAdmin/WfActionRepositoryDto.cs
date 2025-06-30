namespace bdDevCRM.RepositoryDtos.Core.SystemAdmin;

public class WfActionRepositoryDto
{
  public int WfactionId { get; set; }

  public int WfstateId { get; set; }

  public string ActionName { get; set; } = null!;

  public int NextStateId { get; set; }

  public int? EmailAlert { get; set; }

  public int? SmsAlert { get; set; }

  public int? AcSortOrder { get; set; }
}
