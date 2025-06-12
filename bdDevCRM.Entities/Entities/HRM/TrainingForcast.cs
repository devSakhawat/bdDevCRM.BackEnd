using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingForcast
{
    public int TrainingForcastId { get; set; }

    public int TrainingType { get; set; }

    public string? LearnObjective { get; set; }

    public decimal? Duration { get; set; }

    public string? Remarks { get; set; }

    public string? CourseOutline { get; set; }

    public int? OldId { get; set; }

    public decimal? StandardCost { get; set; }

    public int? CompanyId { get; set; }

    public int? PmsYearId { get; set; }

    public int? ForcastedByHrRecordId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? TrainingNameIdForForcast { get; set; }

    public int? NumberofParticipant { get; set; }

    public int? TrainingYear { get; set; }

    public DateTime? CreatedDate { get; set; }
}
