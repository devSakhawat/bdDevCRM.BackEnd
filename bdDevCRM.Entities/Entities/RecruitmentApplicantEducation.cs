using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecruitmentApplicantEducation
{
    public int ApplicantEducationId { get; set; }

    public int? ApplicantId { get; set; }

    public string? Certificate { get; set; }

    public string? Institute { get; set; }

    public string? Yearofcompletion { get; set; }

    public string? Result { get; set; }

    public string? Board { get; set; }

    public int? Certificatetypeid { get; set; }

    public int? ResultStatusId { get; set; }

    public string? OtherInstitute { get; set; }

    public int? IsLastEducation { get; set; }

    public string? MajorOrGroup { get; set; }

    public string? ExamOrDegreeTitle { get; set; }

    public int? DisciplineId { get; set; }

    public int? OutOfResult { get; set; }
}
