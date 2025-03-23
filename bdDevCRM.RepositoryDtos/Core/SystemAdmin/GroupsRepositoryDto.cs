namespace bdDevCRM.RepositoryDtos.Core.SystemAdmin;

public class GroupsRepositoryDto
{
  public int GroupId { get; set; }
  public int CompanyId { get; set; }
  public string GroupName { get; set; }
  public int IsDefault { get; set; }

  public List<GroupPermissionRepositoryDto> ModuleList { get; set; }
  public List<GroupPermissionRepositoryDto> MenuList { get; set; }
  public List<GroupPermissionRepositoryDto> AccessList { get; set; }
  public List<GroupPermissionRepositoryDto> StatusList { get; set; }
  public List<GroupPermissionRepositoryDto> ActionList { get; set; }
  public List<GroupPermissionRepositoryDto> ReportList { get; set; }

  public int TotalCount { get; set; }
}
