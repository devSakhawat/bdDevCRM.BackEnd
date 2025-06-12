using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollAccessRestriction
{
    public int PayrollAccessRestrictionId { get; set; }

    public int AuthorizationHrRecordId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
