using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AdditionalCtcUploadForSalary
{
    public int AdditionalCtcUploadId { get; set; }

    public int CtcId { get; set; }

    public DateOnly? SalaryMonth { get; set; }

    public int? HrRecordId { get; set; }

    public decimal? CtcAmount { get; set; }
}
