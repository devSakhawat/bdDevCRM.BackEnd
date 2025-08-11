namespace bdDevCRM.RepositoryDtos.Core.SystemAdmin;

public class WfStateRepositoryDto
{
  public int WfStateId { get; set; }
  public string StateName { get; set; } = null!;
  public int MenuId { get; set; }
  public bool? IsDefaultStart { get; set; }
  public int? IsClosed { get; set; }
  public int? Sequence { get; set; }
  public string MenuName { get; set; }
}
