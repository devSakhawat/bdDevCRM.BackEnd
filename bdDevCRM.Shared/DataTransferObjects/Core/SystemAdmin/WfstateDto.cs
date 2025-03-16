namespace bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;


public class WfstateDto
{
  public int WFStateId { get; set; }
  public string StateName { get; set; }
  public int MenuID { get; set; }
  public bool IsDefaultStart { get; set; }
  public string MenuName { get; set; }
  public int IsClosed { get; set; }
  public int TotalCount { get; set; }
}