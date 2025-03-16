using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LfaapplicationDetails
{
    public int LfaappDetailsId { get; set; }

    public int? LfaapplicationId { get; set; }

    public int? Ctcid { get; set; }

    public decimal? Ctcvalue { get; set; }
}
