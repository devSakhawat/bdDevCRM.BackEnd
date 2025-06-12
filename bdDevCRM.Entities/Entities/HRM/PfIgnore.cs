using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PfIgnore
{
    public int PfignoreId { get; set; }

    public int HrRecordId { get; set; }

    public int? CreateBy { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? Remarks { get; set; }
}
