namespace bdDevCRM.RepositoryDtos.Core.HR;

public class EmployeesByCompanyBranchDepartmentRepositoroyDto
{
  public int HRRecordId { get; set; }
  public string EmployeeId { get; set; }
  public int CompanyId { get; set; }
  // Report to EmployeeId
  public string ReportTo { get; set; }
  public int DepartmentId { get; set; }
  public int BranchId { get; set; }
  
}
