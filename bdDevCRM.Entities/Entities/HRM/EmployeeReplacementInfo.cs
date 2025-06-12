using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeReplacementInfo
{
    public int NewReplacementId { get; set; }

    public int? NewEmpHrRecordId { get; set; }

    public int? IsNewRecruitment { get; set; }

    public int? ReplacementForHrRecordId { get; set; }

    public int? FiscalYearId { get; set; }
}
