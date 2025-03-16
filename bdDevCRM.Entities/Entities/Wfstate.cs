using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Wfstate
{
    public int WfstateId { get; set; }

    public string StateName { get; set; } = null!;

    public int MenuId { get; set; }

    public bool? IsDefaultStart { get; set; }

    public int? IsClosed { get; set; }

    public int? Sequence { get; set; }

    public virtual ICollection<LoanInformation> LoanInformation { get; set; } = new List<LoanInformation>();

    public virtual ICollection<LoanSchedule> LoanSchedule { get; set; } = new List<LoanSchedule>();
}
