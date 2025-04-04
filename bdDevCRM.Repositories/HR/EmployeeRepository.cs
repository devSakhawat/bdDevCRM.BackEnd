using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.HR;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.HR;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
  public EmployeeRepository(CRMContext context) : base(context) { }


  private const string SELECT_EMPLOYEE_BY_HRRECORDID =
      "Select * from Employee where HRRecordId = {0} order by FullName";

  public async Task<EmploymentRepositoryDto> GetEmploymentByHrRecordId(int hrRecordId)
  {
    string employmentQuery =
    @"SELECT EMPLOYEETYPE.IsContract, EMPLOYEETYPE.IsProbationary, Employment.HRRecordId, EmployeeId, EmployeeType, 
             DESIGNATIONID, StartDate, EmploymentDate, CompanyId, DepartmentId, ReportTo, TelephoneExtension, OfficialEmail, 
             EmergencyContactName, EmergencyContactNo, PossibleConfirmationDate, Duties, AttendanceCardNo, UserId, 
             Employment.LastUpdateDate, BankBranchId, BankAccountNo, BRANCHID, GPFNO, JobEndDate, JOININGPOST, EXPERIENCE, 
             REPORTDEPID, Func_Id, ContractEndDate, JobEndTypeId, GradeId, TinNumber, PostingType, IsOTEligible, DivisionId, 
             FacilityId, SectionId, Approver, ApproverDepartmentId, SalaryLocation 
      FROM Employment 
      INNER JOIN EMPLOYEETYPE ON EMPLOYEETYPE.EMPLOYEETYPEID = Employment.EmployeeType 
      WHERE Employment.HRRecordId = 10468";

    // Call the generic method with the hardcoded query
    var result = await ExecuteSingleData<EmploymentRepositoryDto>(employmentQuery);
    return result;
  }

  public async Task<Wfstate> GetEmployeeCurrentStatusByHrRecordId(int hrRecordId)
  {
    string sql = string.Format(@"Select case when (WFState.IsClosed =3 and Employment.JobEndDate<'{1}') then WFState.StateName else '' end as StateName ,Employment.JobEndDate,GetDate() as CurrentDate  
from Employee 
inner join Employment on Employment.HRRecordId=Employee.HRRecordId 
left outer join WFState on WFState.WFStateId=Employee.StateId where Employment.HRRecordId={0} ", hrRecordId, DateTime.Today.ToString("MM-dd-yyyy"));

    var data = await ExecuteSingleData<Wfstate>(sql);
    return data;
  }

  public async Task<EmployeeRepositoryDto> GetEmployeeByHrRecordId(int hrRecordId)
  {
    string quary = string.Format(SELECT_EMPLOYEE_BY_HRRECORDID, hrRecordId);

    var data = await ExecuteSingleData<EmployeeRepositoryDto>(quary);
    return data;
  }


}
