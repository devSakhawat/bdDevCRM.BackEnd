namespace bdDevCRM.Shared.DataTransferObjects.Conmon;

public class CommonDto
{
  public int? CreatedBy { get; set; }
  public DateTimeOffset? CreatedDate { get; set; } = DateTime.Now;
  public int? UpdateBy { get; set; }
  public DateTimeOffset? UpdateDate { get; set; } = DateTime.Now;
}
