using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HrSignatory
{
    public int? HrSignatoryId { get; set; }

    public string? SignatoryName { get; set; }

    public string? SignatoryTitle { get; set; }

    public string? SignatoryDetails { get; set; }

    public bool? IsActive { get; set; }
}
