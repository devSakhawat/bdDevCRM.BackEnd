using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecruitmentApplicantEmploymentHistory
{
    public int ApplicantEmploymentHistoryId { get; set; }

    public int? ApplicantId { get; set; }

    public string? Designation { get; set; }

    public string? CompanyName { get; set; }

    public string? FromDate { get; set; }

    public string? ToDate { get; set; }

    public string? JobDescription { get; set; }

    public string? ExperienceYear { get; set; }

    public int? IsCurrentlyWorking { get; set; }

    public string? CompanyBusniess { get; set; }

    public string? DepartmentName { get; set; }
}
