namespace bdDevCRM.RepositoryDtos.Core.SystemAdmin;

public class GroupPermissionRepositoryDto
{
  public int PermissionId { get; set; }
  public string PermissionTableName { get; set; }
  public int GroupId { get; set; }
  public int ReferenceID { get; set; }
  public int ParentPermission { get; set; }
}
