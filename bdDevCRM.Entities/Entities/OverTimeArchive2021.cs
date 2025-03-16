using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OverTimeArchive2021
{
    public int OverTimeId { get; set; }

    public DateTime OtfromDate { get; set; }

    public DateTime OttoDate { get; set; }

    public DateTime Otmonth { get; set; }

    public int HrRecordId { get; set; }

    public decimal OverTimeHour { get; set; }

    public int ChannelId { get; set; }

    public int? GeneratorId { get; set; }

    public DateTime? GenerateDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? StatusId { get; set; }

    public int? RecommenderId { get; set; }

    public DateTime? RecommandDate { get; set; }

    public int? IsPaid { get; set; }

    public DateTime? PaidDate { get; set; }

    public int? IsCurrentMonth { get; set; }

    public string? Remarks { get; set; }

    public int? OverTimeType { get; set; }
}
