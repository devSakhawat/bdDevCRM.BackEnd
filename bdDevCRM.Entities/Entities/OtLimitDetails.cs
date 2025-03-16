using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtLimitDetails
{
    public int OtLimitDetailsId { get; set; }

    public int? OtLimitId { get; set; }

    public int? HrRecordId { get; set; }

    public int? MonthlyOtLimit { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
