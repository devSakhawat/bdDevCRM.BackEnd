using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BgPmsCompnayDepartmentMapping
{
    public int CompanyDepartmentMappingId { get; set; }

    public int? CompanyId { get; set; }

    public int? DepartmentId { get; set; }

    public int? CreateUser { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? CompitencyAreaId { get; set; }
}
