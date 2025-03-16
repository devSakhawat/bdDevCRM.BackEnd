using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Lfaapplication
{
    public int LfaapplicationId { get; set; }

    public int? HrRecordId { get; set; }

    public int? LeaveId { get; set; }

    public int? LeaveDays { get; set; }

    public DateOnly? LeaveDateFrom { get; set; }

    public DateOnly? LeaveDateTo { get; set; }

    public string? LeaveReason { get; set; }

    public int? LfastateId { get; set; }

    public int? LfarecommendedBy { get; set; }

    public DateOnly? LfarecommendedDate { get; set; }

    public int? LfaapproveBy { get; set; }

    public DateOnly? LfaapproveDate { get; set; }

    public DateOnly? LfasubmitDate { get; set; }

    public string? Comments { get; set; }

    public string? Address { get; set; }

    public decimal? LfatotalAmount { get; set; }

    public int? FiscalYearId { get; set; }

    public int? TextFileGenerateBy { get; set; }

    public DateTime? TextFileGenerateDate { get; set; }
}
