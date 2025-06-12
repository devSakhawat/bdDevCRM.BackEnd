using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OverTimeProcessDetails
{
    public int OverTimeProcessDetailsId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? OverTimeMonth { get; set; }

    public DateOnly? OverTimeFromDate { get; set; }

    public DateOnly? OverTimeToDate { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcHour { get; set; }

    public decimal? CtcValue { get; set; }

    public decimal? CtcActualValue { get; set; }

    public decimal? Otrate { get; set; }

    public decimal? ActualOtHour { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
