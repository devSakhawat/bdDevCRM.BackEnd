using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ExAppClearMaster
{
    public int AppClearenceMaster { get; set; }

    public int? ApplicationId { get; set; }

    public int? IsCleared { get; set; }

    public DateTime? ClearDate { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public bool? IsRecivable { get; set; }

    public int? ClearedBy { get; set; }

    public int? IsVerified { get; set; }

    public int? VerifiedBy { get; set; }

    public DateTime? VerifiedDate { get; set; }

    public int? Cleared2By { get; set; }

    public DateTime? Clear2Date { get; set; }

    public string? DeptRemarks { get; set; }
}
