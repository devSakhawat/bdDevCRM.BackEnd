using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PmsTabTitles
{
    public int PmsTabTitleId { get; set; }

    public int? PmsConfigId { get; set; }

    public int? TitlePosition { get; set; }

    public string? Title { get; set; }
}
