using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PaScoreConfig
{
    public int PaScoreConfigId { get; set; }

    public string PaScoreCode { get; set; } = null!;

    public int PaScoreFromAmount { get; set; }

    public int PaScoreToAmount { get; set; }
}
