using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmploymentHistory
{
    public int EmploymentHistoryId { get; set; }

    public int? HrrecordId { get; set; }

    public string? Designation { get; set; }

    public string? CompanyName { get; set; }

    public string? FromDate { get; set; }

    public string? ToDate { get; set; }

    public string? JobDescription { get; set; }

    public string? ExperienceYear { get; set; }
}
