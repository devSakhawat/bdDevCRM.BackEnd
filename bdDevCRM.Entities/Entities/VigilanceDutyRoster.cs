using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VigilanceDutyRoster
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? SubSectionId { get; set; }

    public int? EmployeeType { get; set; }

    public int? EmployeeLevel { get; set; }

    public int? EmployeeCostCenter { get; set; }

    public int? ShiftId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ToDate { get; set; }

    public double? AverageTime { get; set; }

    public TimeOnly? FromTime { get; set; }

    public TimeOnly? ToTime { get; set; }

    public string? Remarks { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? Approver { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? Wfstate { get; set; }

    public int? RewardType { get; set; }

    public DateTime? RewardDate { get; set; }

    public int? LeaveCount { get; set; }

    public DateTime? GraceTime { get; set; }

    public int? DutyStarts { get; set; }

    public bool? HasBreakup { get; set; }

    public TimeOnly? BreakupHour { get; set; }
}
