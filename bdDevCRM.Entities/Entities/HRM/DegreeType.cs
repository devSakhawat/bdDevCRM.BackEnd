using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DegreeType
{
    public int DegreeTypeId { get; set; }

    public string? DegreeTypeName { get; set; }

    public int? IsActive { get; set; }
}
