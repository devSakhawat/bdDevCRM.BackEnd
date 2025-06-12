using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DottedLineLocationMapping
{
    public int DottedLineLocationMapId { get; set; }

    public int DottedLineEmailConfigId { get; set; }

    public int BranchId { get; set; }

    public int CompanyId { get; set; }
}
