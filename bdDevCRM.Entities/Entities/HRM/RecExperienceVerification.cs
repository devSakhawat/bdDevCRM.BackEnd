using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecExperienceVerification
{
    public int ExperienceVerificationId { get; set; }

    public string? NameofReferee { get; set; }

    public string? Designation { get; set; }

    public string? Organization { get; set; }

    public string? MobileNo { get; set; }

    public int? HonestyTypeId { get; set; }

    public string? HonestyTypeName { get; set; }

    public int? FinancialClearanceTypeId { get; set; }

    public string? FinancialClearanceTypeName { get; set; }

    public int? ProfessionalismTypeId { get; set; }

    public string? ProfessionalismTypeName { get; set; }

    public int? CommentsTypeId { get; set; }

    public string? CommentsTypeName { get; set; }

    public int? ApplicantId { get; set; }
}
