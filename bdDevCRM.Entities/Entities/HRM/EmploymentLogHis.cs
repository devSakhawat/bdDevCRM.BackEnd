using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmploymentLogHis
{
    public int EmploymentLogHisId { get; set; }

    public int HrrecordId { get; set; }

    public string? EmployeeId { get; set; }

    public int? EmployeeType { get; set; }

    public int? Designationid { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EmploymentDate { get; set; }

    public int? CompanyId { get; set; }

    public int? DepartmentId { get; set; }

    public int? ReportTo { get; set; }

    public string? TelephoneExtension { get; set; }

    public string? OfficialEmail { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactNo { get; set; }

    public string? Duties { get; set; }

    public string? AttendanceCardNo { get; set; }

    public int? UserId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? BankBranchId { get; set; }

    public string? BankAccountNo { get; set; }

    public int? Branchid { get; set; }

    public int? Shiftid { get; set; }

    public string? Gpfno { get; set; }

    public DateTime? JobEndDate { get; set; }

    public int? Joiningpost { get; set; }

    public string? Experience { get; set; }

    public int? Reportdepid { get; set; }

    public int? FuncId { get; set; }

    public DateTime? ContractEndDate { get; set; }

    public int? JobEndTypeId { get; set; }

    public int? GradeId { get; set; }

    public string? TinNumber { get; set; }

    public int? PostingType { get; set; }

    public int? IsOteligible { get; set; }

    public string? ContactAddress { get; set; }

    public DateTime? EffectEndDate { get; set; }
}
