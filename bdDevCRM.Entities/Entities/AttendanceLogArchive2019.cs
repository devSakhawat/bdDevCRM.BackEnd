using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendanceLogArchive2019
{
    public int AttendanceId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? AttendanceDate { get; set; }

    public string? LoginTime { get; set; }

    public string? LogoutTime { get; set; }

    public int? Status { get; set; }

    public bool? IsAttendanceClearOut { get; set; }

    public bool? IsLate { get; set; }

    public string? LateReason { get; set; }

    public int? ShiftId { get; set; }

    public int? IsHoliDay { get; set; }

    public int? DefalterType { get; set; }

    public int? IsNightShift { get; set; }

    public decimal? BreakupDuration { get; set; }

    public int? IsNightAllowanceApp { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? DesignationId { get; set; }

    public int? GradeId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? FuncId { get; set; }

    public string? ShiftIntime { get; set; }

    public string? ShiftOutTime { get; set; }

    public decimal? GraceIn { get; set; }

    public decimal? GraceOut { get; set; }

    public string? LastPunch { get; set; }
}
