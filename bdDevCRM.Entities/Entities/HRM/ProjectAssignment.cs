using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ProjectAssignment
{
    public int AssignmentId { get; set; }

    public int? TaskId { get; set; }

    public int? AssignTo { get; set; }

    public DateTime? AssignDate { get; set; }

    public DateTime? TargetDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int? IsActive { get; set; }

    public int? Assigndepartmentid { get; set; }
}
