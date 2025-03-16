using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempEmployeeWiseOvertime
{
    public DateTime? EffectiveDate { get; set; }

    public decimal? Otamount { get; set; }

    public int? IsActive { get; set; }

    public int? UserId { get; set; }

    public int? HrRecordId { get; set; }

    public string EmployeeId { get; set; } = null!;
}
