using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmploymentHistoryTemp
{
    public int EmploymentHistoryTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? Designation { get; set; }

    public string? CompanyName { get; set; }

    public string? FromDate { get; set; }

    public string? ToDate { get; set; }

    public string? JobDescription { get; set; }

    public string? ExperienceYear { get; set; }

    public int? UserId { get; set; }
}
