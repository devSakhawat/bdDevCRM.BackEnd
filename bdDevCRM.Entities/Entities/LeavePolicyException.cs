using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeavePolicyException
{
    public int ExceptionId { get; set; }

    public int HrRecordId { get; set; }

    public int LeaveTypeId { get; set; }

    public DateOnly LeaveDate { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
