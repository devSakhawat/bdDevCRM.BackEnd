using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencyAndFunctionMap
{
    public int Id { get; set; }

    public int? FunctionId { get; set; }

    public int? CompetencyId { get; set; }

    public virtual Competencies? Competency { get; set; }

    public virtual HrFunction? Function { get; set; }
}
