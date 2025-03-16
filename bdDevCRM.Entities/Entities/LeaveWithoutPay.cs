using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveWithoutPay
{
    public int LeaveWithoutId { get; set; }

    public int? HrRecordId { get; set; }

    public decimal? LeaveWithoutPay1 { get; set; }

    public DateTime? SalaryMonth { get; set; }

    /// <summary>
    /// 1=SystemGenerated,2=Upload
    /// </summary>
    public int? ChannelId { get; set; }

    public int? IsBasic { get; set; }

    public int? IsLwpPrevSalaryMonth { get; set; }
}
