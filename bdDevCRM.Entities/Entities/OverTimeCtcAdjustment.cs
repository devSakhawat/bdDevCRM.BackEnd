using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OverTimeCtcAdjustment
{
    public int OverTimeAdjustmentId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? OverTimeMonth { get; set; }

    public DateOnly? OverTimeFromDate { get; set; }

    public DateOnly? OverTimeToDate { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public string? Remarks { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? DeleteBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public int? IsDeleted { get; set; }
}
