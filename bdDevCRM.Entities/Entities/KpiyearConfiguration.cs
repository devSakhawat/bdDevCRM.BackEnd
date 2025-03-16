using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KpiyearConfiguration
{
    public int YearConfigId { get; set; }

    public string ConfigCode { get; set; } = null!;

    public string? ConfigName { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? TargetOpen { get; set; }

    public int? TargetClose { get; set; }

    public int? ReviewOpen { get; set; }

    public int? ReviewClose { get; set; }

    public int IsActive { get; set; }

    public int? ReviewType { get; set; }

    public int? MaxLimit { get; set; }

    public int? MinLimit { get; set; }
}
