using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SelfKpi
{
    public int SelfKpiId { get; set; }

    public string KpiTitle { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public int Marks { get; set; }

    public int HrRecordId { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public int YearConfigId { get; set; }

    public string? AppraisalGoalTitle { get; set; }

    public string? TrainingRequirement { get; set; }

    public int? CreatedBy { get; set; }

    public int? IsActive { get; set; }
}
