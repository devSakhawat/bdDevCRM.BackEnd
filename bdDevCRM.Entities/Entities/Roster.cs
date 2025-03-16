using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Roster
{
    public int RosterId { get; set; }

    public DateTime RosterMonth { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? Status { get; set; }

    public int? OldRosterId { get; set; }
}
