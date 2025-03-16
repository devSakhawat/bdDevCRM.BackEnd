using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OverTimeTaxInfo
{
    public int OttaxId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? OvertimeMonth { get; set; }

    public decimal? TaxAmount { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }
}
