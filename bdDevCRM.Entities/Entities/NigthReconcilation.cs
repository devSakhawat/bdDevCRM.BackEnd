using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NigthReconcilation
{
    public int RemoveId { get; set; }

    public int? HrRecordId { get; set; }

    public int? ShiftId { get; set; }

    public DateTime? AttendanceDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
