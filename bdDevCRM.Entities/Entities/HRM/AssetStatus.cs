using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetStatus
{
    public int AssetStatusId { get; set; }

    public string? AssetStatusName { get; set; }

    public int? IsActive { get; set; }
}
