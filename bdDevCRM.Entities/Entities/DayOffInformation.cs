using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DayOffInformation
{
    public int DayOffInformationId { get; set; }

    public int HrRecordId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int? Days { get; set; }

    public string? Reason { get; set; }

    public DateOnly? ApplyDate { get; set; }

    public int? IsRecomanded { get; set; }

    public int? RecomandBy { get; set; }

    public DateOnly? RecomandDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateOnly? ApprovedDate { get; set; }

    public int? IsApproved { get; set; }

    public int? StateId { get; set; }
}
