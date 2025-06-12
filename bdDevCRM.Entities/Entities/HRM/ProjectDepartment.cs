using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ProjectDepartment
{
    public int ProjectDepartmentId { get; set; }

    public int ProjectId { get; set; }

    public int DepartmentId { get; set; }
}
