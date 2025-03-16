using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendanceMonthEndArchive2020
{
    public int AttendanceMonthEndId { get; set; }

    public int HrRecordId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public decimal? CurrentBasic { get; set; }

    public int? TotalDays { get; set; }

    public decimal? PresentDays { get; set; }

    public decimal? AbsentDays { get; set; }

    public decimal? LeaveDays { get; set; }

    public decimal? OnsiteDays { get; set; }

    public decimal? DayOffHoliday { get; set; }

    public decimal? LeaveWithoutPay { get; set; }

    public decimal? LateDeduction { get; set; }

    public decimal? AcumulatedDays { get; set; }

    public decimal? FinalDeductionDays { get; set; }

    public decimal? OverTimeHours { get; set; }

    public DateTime? AttendanceMonth { get; set; }

    public int? Status { get; set; }

    public int? GenerateBy { get; set; }

    public DateTime? GenerateDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? WorkingDays { get; set; }

    public int? ApprovedLeave { get; set; }

    public int? DayOff { get; set; }

    public int? OnsiteClient { get; set; }

    public int? LateDays { get; set; }

    public int? LeaveDeduction { get; set; }

    public decimal? FirstLateDays { get; set; }

    public decimal? SecLateDays { get; set; }

    public decimal? ThirdLateDays { get; set; }

    public decimal? DutyHourOnHoliday { get; set; }

    public int? HolidayDutyCount { get; set; }

    public decimal? DutyHourOnWeekend { get; set; }

    public int? WeekendDaysCount { get; set; }

    public int? NightCount { get; set; }

    public int? EarlyExit { get; set; }

    public decimal? LeaveWithoutPayBasic { get; set; }

    public DateTime? CutOfDate { get; set; }

    public int? Delay { get; set; }

    public int? IsLwpPrevMonth { get; set; }

    public int? EarlyExitCount { get; set; }
}
