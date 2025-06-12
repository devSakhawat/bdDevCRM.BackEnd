using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TransectionType
{
    public int? TransectionTypeId { get; set; }

    public string? TransectionTypeName { get; set; }

    public int? VoucharTypeId { get; set; }

    public int? IsActive { get; set; }
}
