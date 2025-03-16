using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Xpd
{
    public int XpddId { get; set; }

    public int? XpId { get; set; }

    public int? XcId { get; set; }

    public decimal? Xcv { get; set; }
}
