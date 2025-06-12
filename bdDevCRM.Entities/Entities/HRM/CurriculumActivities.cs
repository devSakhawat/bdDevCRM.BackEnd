using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CurriculumActivities
{
    public int CurriculumActivitiesId { get; set; }

    public string Activitiesname { get; set; } = null!;

    public string? Activitycode { get; set; }

    /// <summary>
    /// 0=Inactive,1=Active
    /// </summary>
    public int? IsActive { get; set; }
}
