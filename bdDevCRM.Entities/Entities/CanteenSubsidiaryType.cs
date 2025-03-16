using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenSubsidiaryType
{
    public int SubsidiaryTypeId { get; set; }

    public string SubsidiaryTypeName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
