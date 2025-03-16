using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssignApprover
{
    public int AssignApproverId { get; set; }

    public int ApproverId { get; set; }

    public int HrRecordId { get; set; }

    public int ModuleId { get; set; }

    public int Type { get; set; }

    public int IsNew { get; set; }

    public int? SortOrder { get; set; }

    public bool? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
