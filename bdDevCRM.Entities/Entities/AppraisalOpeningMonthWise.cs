using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalOpeningMonthWise
{
    public int AppraisalOpeningId { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int? ForMonth { get; set; }

    public int? ForYear { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateUser { get; set; }

    public int? ForApprover { get; set; }

    public int? ForEmployee { get; set; }

    public int? ForFfsEmployee { get; set; }
}
