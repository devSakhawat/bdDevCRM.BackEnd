using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ExClearanceSetupDetail
{
    public int ClearanceSetupDetailId { get; set; }

    public int ClearanceSetupMasterId { get; set; }

    public int ParameterId { get; set; }

    public int? IsActive { get; set; }

    public int? Sequence { get; set; }
}
