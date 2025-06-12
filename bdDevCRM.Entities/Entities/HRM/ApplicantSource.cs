using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicantSource
{
    public int ApplicantSourceId { get; set; }

    public string? ApplicantSourceName { get; set; }

    public int? IsActive { get; set; }
}
