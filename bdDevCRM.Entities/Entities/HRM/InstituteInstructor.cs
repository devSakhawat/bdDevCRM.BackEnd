using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InstituteInstructor
{
    public int InstituteOrInstructorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactMobile { get; set; }

    /// <summary>
    /// 1=Institute,2=Instructor
    /// </summary>
    public string ConductedType { get; set; } = null!;

    public string? Designation { get; set; }
}
