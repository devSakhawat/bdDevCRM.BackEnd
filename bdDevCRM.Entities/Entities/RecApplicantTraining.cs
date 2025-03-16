using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecApplicantTraining
{
    public int TrainingId { get; set; }

    public int? ApplicantId { get; set; }

    public int? TrainingTypeId { get; set; }

    public string? TrainingTypeName { get; set; }

    public string? TrainingTitle { get; set; }

    public string? Trainer { get; set; }

    public string? TrainingInstitute { get; set; }

    public string? Location { get; set; }

    public string? Duration { get; set; }

    public DateOnly? TrainingFrom { get; set; }

    public DateOnly? TrainingTo { get; set; }

    public string? TopicsCovered { get; set; }

    public DateOnly? CreadedDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? TrainingYear { get; set; }
}
