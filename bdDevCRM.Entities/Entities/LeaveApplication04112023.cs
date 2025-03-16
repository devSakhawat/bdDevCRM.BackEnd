using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveApplication04112023
{
    public int LeaveId { get; set; }

    public int? LeaveType { get; set; }

    public int? HrrecordId { get; set; }

    public DateOnly? LeaveFrom { get; set; }

    public DateOnly? LeaveTo { get; set; }

    public decimal? LeaveDays { get; set; }

    public string Reason { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime? AppliedDate { get; set; }

    public bool? IsRecommanded { get; set; }

    public int? RecommanderId { get; set; }

    public DateTime? RecommandDate { get; set; }

    public string? Remarks { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? StateId { get; set; }

    public int? ReferenceId { get; set; }

    public int? HalfDaySlot { get; set; }

    public bool? IsDoctorCertificate { get; set; }

    public bool? IsFourceLeave { get; set; }

    public bool? IsRecomandCertificate { get; set; }

    public string? PerformedBy { get; set; }

    public int? InformedOffice { get; set; }

    public string? Comments { get; set; }

    public int? IsLfaapplicable { get; set; }

    public int? DeligateId { get; set; }

    public int? LeaveReasonId { get; set; }

    public int? LfaServiceYear { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? DesignationId { get; set; }

    public int? GradeId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public decimal? Entitlement { get; set; }

    public decimal? Balance { get; set; }

    public int? FuncId { get; set; }

    public string? MedicalCertificatePath { get; set; }

    public DateTime? PerformedDate { get; set; }

    public DateTime? EffectiveMonth { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeleteTime { get; set; }

    public string? DeleteReason { get; set; }
}
