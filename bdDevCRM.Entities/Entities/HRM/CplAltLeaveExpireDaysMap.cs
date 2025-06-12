using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CplAltLeaveExpireDaysMap
{
    public int LeaveExpireDaysMappingId { get; set; }

    public int HrRecordId { get; set; }

    public int? CplleaveDays { get; set; }

    public int? AltLeaveDays { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? IsActive { get; set; }
}
