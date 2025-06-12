using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeEarlyCpfinterest
{
    public int HrRecordId { get; set; }

    public int? TaxYearId { get; set; }

    public decimal? CpfInterestOwn { get; set; }

    public decimal? CpfInterestComp { get; set; }
}
