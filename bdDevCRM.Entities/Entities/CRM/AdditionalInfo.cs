using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class AdditionalInfo
{
    public int AdditionalInfoId { get; set; }

    public int ApplicantId { get; set; }

    public string? RequireAccommodation { get; set; }

    public string? HealthNmedicalNeeds { get; set; }

    public string? HealthNmedicalNeedsRemarks { get; set; }

    public string? AdditionalInformationRemarks { get; set; }

    public string? DocumentTitle { get; set; }

    public string? UploadFile { get; set; }

    public string? DocumentName { get; set; }

    public string? FileThumbnail { get; set; }

    public string? RecordType { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
