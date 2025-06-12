using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InterestRateConfiguration
{
    public int PfInterestRateId { get; set; }

    public int? CompanyId { get; set; }

    public int? YearId { get; set; }

    public decimal? InterestRate { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
