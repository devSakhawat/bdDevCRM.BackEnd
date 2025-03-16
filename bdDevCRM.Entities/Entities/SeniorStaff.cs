using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SeniorStaff
{
    public int SeniorStaffId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CreateBy { get; set; }

    public DateOnly? CreateDate { get; set; }
}
