using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Rsmregion
{
    public int RsmregionId { get; set; }

    public string? RsmregionName { get; set; }

    public string RsmregionCode { get; set; } = null!;

    public int? IsActive { get; set; }

    public int? RsmmanagerHrRecordId { get; set; }

    public int? SalaesManagerHrRecordId { get; set; }
}
