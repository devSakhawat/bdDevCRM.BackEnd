using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class UnauthorizedAbsence
{
    public int UnauthorizedAbsenceId { get; set; }

    public int HrRecordId { get; set; }

    public DateOnly AbsentFrom { get; set; }

    public int TotalAbsentDays { get; set; }

    public int StateId { get; set; }
}
