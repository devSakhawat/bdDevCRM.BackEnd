using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class CrmAdditionalInfo
{
    public int AdditionalInfoId { get; set; }

    public int ApplicantId { get; set; }

    public bool? RequireAccommodation { get; set; }

    public bool? HealthNmedicalNeeds { get; set; }

    public string? HealthNmedicalNeedsRemarks { get; set; }

    public string? AdditionalInformationRemarks { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
