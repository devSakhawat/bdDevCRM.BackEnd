using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencyAreaSettings
{
    public int CompetencyAreaId { get; set; }

    public string? CompetencyAreaName { get; set; }

    public string? CompetencyAreaDescription { get; set; }

    public int? CreateBy { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? IsActive { get; set; }

    public int? IsManagementType { get; set; }
}
