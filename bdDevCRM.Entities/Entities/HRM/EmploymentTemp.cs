using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmploymentTemp
{
    public int HrrecordId { get; set; }

    public string? EmployeeId { get; set; }

    public string? EmployeeType { get; set; }

    public string? Company { get; set; }

    public string? Branch { get; set; }

    public string? Division { get; set; }

    public string? Facility { get; set; }

    public string? Section { get; set; }

    public string? Department { get; set; }

    public string? FunctionJobRole { get; set; }

    public string? Designation { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EmploymentDate { get; set; }

    public string? ReportTo { get; set; }

    public string? TelephoneExtension { get; set; }

    public string? OfficialEmail { get; set; }

    public string? Duties { get; set; }

    public string? AttendanceCardNo { get; set; }

    public int? UserId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public string? Bank { get; set; }

    public string? BankBranch { get; set; }

    public string? BankAccountNo { get; set; }

    public string? Gpfno { get; set; }

    public DateTime? JobEndDate { get; set; }

    public string? Joiningpost { get; set; }

    public string? Reportdepid { get; set; }

    public DateTime? ContractEndDate { get; set; }

    public DateTime? ContractStartDate { get; set; }

    public string? JobEndType { get; set; }

    public string? Grade { get; set; }

    public string? TinNumber { get; set; }

    public string? PostingType { get; set; }

    public string? IsOteligible { get; set; }

    public string? ContactAddress { get; set; }

    public string? IsFieldForce { get; set; }

    public string? ApproverDepartmentId { get; set; }

    public string? Approver { get; set; }

    public string? IsReserved { get; set; }

    public DateOnly? ConfirmationDate { get; set; }

    public DateOnly? AppointmentDate { get; set; }
}
