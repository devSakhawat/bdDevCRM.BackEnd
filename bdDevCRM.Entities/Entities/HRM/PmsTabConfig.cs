using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PmsTabConfig
{
    public int PmsConfigId { get; set; }

    public string? TabName { get; set; }

    public string? Title { get; set; }

    public int? Sequence { get; set; }

    public bool? IsActive { get; set; }

    public virtual PmsInstructionConfig? PmsInstructionConfig { get; set; }
}
