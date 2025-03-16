using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeLateInfo
{
    public int Id { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? AttendanceDate { get; set; }

    public string? AttendanceLogId { get; set; }

    public int? IsAdjusted { get; set; }

    public string? AdjustmentTypeCode { get; set; }

    public int? AdjustmentReferenceId { get; set; }
}
