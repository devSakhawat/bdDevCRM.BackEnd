using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollException
{
    public int PayrollExceptionId { get; set; }

    public int HrRecordId { get; set; }

    public int? IsForeignCurrencyEnable { get; set; }

    public int? IsInvestmentDisable { get; set; }

    public int? IsTaxVariation { get; set; }

    public int? TaxValueType { get; set; }

    public int? TaxSetupValue { get; set; }

    public int? ParentCtc { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
