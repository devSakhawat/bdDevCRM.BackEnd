using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRequisitionType
{
    public int RequisitionTypeId { get; set; }

    public string? TypeName { get; set; }

    public bool? IsHoliday { get; set; }

    public bool? IsSecondLift { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsOther { get; set; }
}
