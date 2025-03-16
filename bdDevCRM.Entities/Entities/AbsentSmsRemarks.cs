using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AbsentSmsRemarks
{
    public int AbsentSmsRemarksId { get; set; }

    public int? HrRecordId { get; set; }

    public string? RemarksCode { get; set; }

    public string? Remarks { get; set; }

    public string? RemarksSpecification { get; set; }
}
