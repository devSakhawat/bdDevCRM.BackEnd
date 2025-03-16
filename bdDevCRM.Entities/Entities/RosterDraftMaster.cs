using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterDraftMaster
{
    public int RosterDraftMasterId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FunctionId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? ShiftId { get; set; }

    public int? NewShiftId { get; set; }

    public int? IsDo { get; set; }

    public int? IsNextDayOff { get; set; }

    public string? Remarks { get; set; }

    public int? StateId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? RejectBy { get; set; }

    public DateTime? RejectDate { get; set; }

    public int? DivisionId { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}
