using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollAdjustment
{
    public int PayrollAdjustmentId { get; set; }

    public int HrRecordId { get; set; }

    public DateTime? FromMonth { get; set; }

    public DateTime? ToMonth { get; set; }

    public int StatusId { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public decimal? GrossPay { get; set; }

    public decimal? NetTaxPayable { get; set; }

    public decimal? CompanyTaxPayable { get; set; }
}
