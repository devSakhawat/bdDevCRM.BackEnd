using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CvsortingReport
{
    public int Id { get; set; }

    public int? RequisitionId { get; set; }

    public int? JobId { get; set; }

    public int? JobVacancyId { get; set; }

    public string? Source { get; set; }

    public int? TotalApplied { get; set; }

    public int? InitialShortListed { get; set; }

    public int? FinalShortListed { get; set; }

    public string? Remarks { get; set; }

    public int? CreateBy { get; set; }
}
