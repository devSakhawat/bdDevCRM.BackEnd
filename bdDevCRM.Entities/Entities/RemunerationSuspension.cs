using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RemunerationSuspension
{
    public int RemunerationSusId { get; set; }

    public int? CtcId { get; set; }

    public int? CtcValue { get; set; }
}
