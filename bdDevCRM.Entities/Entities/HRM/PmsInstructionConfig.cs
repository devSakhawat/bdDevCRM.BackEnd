using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PmsInstructionConfig
{
    public int InstructionId { get; set; }

    public int? PmsConfigId { get; set; }

    public string? InstructionTitle { get; set; }

    public string? InstructionText { get; set; }

    public bool? IsShow { get; set; }

    public virtual PmsTabConfig Instruction { get; set; } = null!;
}
