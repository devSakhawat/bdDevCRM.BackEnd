using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterDetails18112023
{
    public int RosterDetailsId { get; set; }

    public int RosterId { get; set; }

    public DateOnly DateValue { get; set; }

    public int HrRecordId { get; set; }

    public int? ShiftId { get; set; }

    public int? ReplaceShift { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
