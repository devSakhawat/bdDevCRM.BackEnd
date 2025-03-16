using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BranchShift
{
    public int Branchshiftid { get; set; }

    public int Shiftid { get; set; }

    public int Branchid { get; set; }
}
