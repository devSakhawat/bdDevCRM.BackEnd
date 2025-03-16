using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PunishmentDetails
{
    public int PunishmentDetailsId { get; set; }

    public int? SuspensionId { get; set; }

    public int? CtcId { get; set; }

    public decimal? AffectTime { get; set; }
}
