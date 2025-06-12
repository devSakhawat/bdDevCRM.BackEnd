using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DayOff
{
    public int DayOffId { get; set; }

    public int HrrecordId { get; set; }

    public DateTime AppliedDate { get; set; }
}
