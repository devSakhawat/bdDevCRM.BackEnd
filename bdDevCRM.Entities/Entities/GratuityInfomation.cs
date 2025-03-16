using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GratuityInfomation
{
    public int GratuityId { get; set; }

    public int HrRecordId { get; set; }

    public decimal? GratuityAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? GenerateBy { get; set; }

    public DateTime? GenerateDate { get; set; }

    public int? PostedBy { get; set; }

    public DateTime? PostedDate { get; set; }

    public int? StatusId { get; set; }

    public int? AgeLimit { get; set; }

    public int? MaturedPeriod { get; set; }
}
