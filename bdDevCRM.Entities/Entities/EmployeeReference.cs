using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeReference
{
    public int ReferenceId { get; set; }

    public string? HrRecordId { get; set; }

    public string? RefName { get; set; }

    public string? RefOccupation { get; set; }

    public string? RefMobile { get; set; }

    public string? RefAddress { get; set; }

    public string? RefType { get; set; }

    public string? RefEmail { get; set; }
}
