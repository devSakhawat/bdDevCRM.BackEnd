using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterLock
{
    public int? RosterLockId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public DateTime? LocakFrom { get; set; }

    public DateTime? LocakTo { get; set; }

    public int? LockBy { get; set; }

    public DateTime? LockDate { get; set; }

    public int? UpdateBy { get; set; }

    public int? UpdateDate { get; set; }

    public int? IsActive { get; set; }
}
