using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterDraftDetailsForRl
{
    public int RosterDraftDetailsId { get; set; }

    public int? RosterDraftMasterId { get; set; }

    public int? HrRecordId { get; set; }

    public int? NewShiftId { get; set; }

    public int? IsDo { get; set; }

    public int? IsNextDayOff { get; set; }
}
