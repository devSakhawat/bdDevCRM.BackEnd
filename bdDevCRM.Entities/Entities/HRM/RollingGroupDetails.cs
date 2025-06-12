using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RollingGroupDetails
{
    public int RollGroupDetailsId { get; set; }

    public int RollGroupId { get; set; }

    public int HrRecordId { get; set; }

    public virtual RollingGroup RollGroup { get; set; } = null!;
}
