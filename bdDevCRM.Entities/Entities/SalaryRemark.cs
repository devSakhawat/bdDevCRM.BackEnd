using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryRemark
{
    public int SalaryRemarksId { get; set; }

    public int HrRecordId { get; set; }

    public DateOnly SalaryMonth { get; set; }

    public string? Remarks { get; set; }
}
