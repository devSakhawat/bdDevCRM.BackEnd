using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencyAreaDepartmentDesignationMapping
{
    public int CompetencyAreaDepartmentDesignationMappingId { get; set; }

    public int? CompetencyAreaId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }
}
