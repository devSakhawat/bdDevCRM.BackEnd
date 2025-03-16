using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecruitmentApplicantReferance
{
    public int ApplicantReferanceId { get; set; }

    public int? ApplicantId { get; set; }

    public string? RefName { get; set; }

    public string? RefOccupation { get; set; }

    public string? RefMobile { get; set; }

    public string? RefAddress { get; set; }

    public string? RefType { get; set; }

    public string? RefEmail { get; set; }

    public string? Organization { get; set; }

    public string? Relation { get; set; }
}
