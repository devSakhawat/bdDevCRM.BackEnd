using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GradeTypeInfo
{
    public int GradeTypeInfoId { get; set; }

    public string? GradeTypeName { get; set; }

    public int? IsActive { get; set; }
}
