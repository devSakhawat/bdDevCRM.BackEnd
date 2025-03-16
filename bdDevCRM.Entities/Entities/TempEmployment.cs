using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempEmployment
{
    public int HrrecordId { get; set; }

    public string? EmployeeId { get; set; }

    public string? EmployeeType { get; set; }

    public string? Designationid { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EmploymentDate { get; set; }

    public string? CompanyId { get; set; }

    public string? DepartmentId { get; set; }

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

    public string? Branchid { get; set; }

    public string? Shiftid { get; set; }

    public string? Gpfno { get; set; }

    public DateOnly? JobEndDate { get; set; }

    public string? Joiningpost { get; set; }

    public string? Experience { get; set; }

    public int? Reportdepid { get; set; }

    public string? FuncId { get; set; }

    public DateOnly? ContractEndDate { get; set; }

    public string? JobEndTypeId { get; set; }

    public string? GradeId { get; set; }

    public string? TinNumber { get; set; }

    public string? PostingType { get; set; }

    public int? IsOteligible { get; set; }

    public string? ContactAddress { get; set; }

    public int? IsFieldForce { get; set; }

    public int? ApproverDepartmentId { get; set; }

    public int? Approver { get; set; }

    public string? DivisionId { get; set; }

    public string? FacilityId { get; set; }

    public string? SectionId { get; set; }

    public int? IsReserved { get; set; }

    public DateOnly? ConfirmationDate { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public string? SalaryLocation { get; set; }

    public int? OmitLate { get; set; }

    public DateOnly? PossibleConfirmationDate { get; set; }
}
