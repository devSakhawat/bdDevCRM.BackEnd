using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendenceRemarksSettings
{
    public int AttendenceRemarksId { get; set; }

    public string? RemarksCode { get; set; }

    public string? RemarksName { get; set; }

    public int? IsActive { get; set; }
}
