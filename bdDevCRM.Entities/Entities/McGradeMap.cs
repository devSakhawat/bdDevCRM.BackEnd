using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class McGradeMap
{
    public int McGradeMapId { get; set; }

    public int? McPolicyId { get; set; }

    public int? GradeId { get; set; }
}
