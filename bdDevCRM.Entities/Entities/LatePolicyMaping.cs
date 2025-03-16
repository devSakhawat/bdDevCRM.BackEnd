using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LatePolicyMaping
{
    public int LatePolicyMappingId { get; set; }

    public int LatePolicyId { get; set; }

    public int BranchId { get; set; }

    public int? ShiftId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? IsActive { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdateBy { get; set; }
}
