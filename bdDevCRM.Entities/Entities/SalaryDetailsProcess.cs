using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryDetailsProcess
{
    public int SalaryDetailsId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public int? CtcAddLess { get; set; }

    public DateTime? SalaryMonth { get; set; }

    public int? StatusId { get; set; }

    public int? IsAdditional { get; set; }

    public int? IsRemove { get; set; }

    public decimal? CtcActualValue { get; set; }

    public int? SalaryType { get; set; }

    public int? LoanScheduleId { get; set; }

    public decimal? Arear { get; set; }

    public decimal? LwpAmt { get; set; }
}
