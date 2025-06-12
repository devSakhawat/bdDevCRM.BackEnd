using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetLocation
{
    public int AssetLocationId { get; set; }

    public int AssetIdentificationId { get; set; }

    public int? CompanyId { get; set; }

    public int? LocationId { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime? RelocationDate { get; set; }
}
