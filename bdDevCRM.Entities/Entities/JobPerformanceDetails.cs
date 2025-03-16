using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobPerformanceDetails
{
    public int JobPerformanceDetailsId { get; set; }

    public int? CompetencyId { get; set; }

    public int? HrRecordId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateUser { get; set; }

    public int? CompanyId { get; set; }
}
