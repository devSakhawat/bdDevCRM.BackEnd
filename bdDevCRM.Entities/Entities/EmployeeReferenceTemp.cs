using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeReferenceTemp
{
    public int ReferenceTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? RefType { get; set; }

    public string? RefName { get; set; }

    public string? RefOccupation { get; set; }

    public string? RefMobile { get; set; }

    public string? RefAddress { get; set; }

    public int? UserId { get; set; }
}
