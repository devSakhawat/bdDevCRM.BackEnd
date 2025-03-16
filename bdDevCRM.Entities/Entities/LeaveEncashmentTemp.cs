using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveEncashmentTemp
{
    public int? HrRecordId { get; set; }

    public int? LeaveTypeId { get; set; }

    public int? FiscalYearId { get; set; }

    public decimal? NormalLeaveDays { get; set; }

    public decimal? LeaveBroughtForward { get; set; }

    public decimal? EncashmentDays { get; set; }

    public int? EncashmentAmount { get; set; }

    public decimal? YearEndBalance { get; set; }

    public decimal? LeaveAvaill { get; set; }

    public decimal? NextYearCarryForward { get; set; }

    public decimal? TotalLeaveBalanceForNextYear { get; set; }

    public int LeaveEncashmentId { get; set; }

    public int? StateId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public int? FacilityId { get; set; }

    public int? GradeId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? PrevLeaveEncashmentDays { get; set; }

    public decimal? PrevLeaveEncashmentAmt { get; set; }

    public decimal? EncashmentAmountPerDay { get; set; }

    public decimal? CurrentBasic { get; set; }
}
