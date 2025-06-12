using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingInfo
{
    public int TrainingInfoId { get; set; }

    public string TrainingCode { get; set; } = null!;

    public string TrainingName { get; set; } = null!;

    public int? TrainingTypeId { get; set; }

    public string? LearnObjective { get; set; }

    public string? Remarks { get; set; }

    public string? CourseOutline { get; set; }

    public decimal? Duration { get; set; }

    public int? OldId { get; set; }

    public decimal? StandardCost { get; set; }

    public int? CompanyId { get; set; }

    public int? PmsYearId { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
