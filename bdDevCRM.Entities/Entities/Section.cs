using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Section
{
    public int SectionId { get; set; }

    public string? SectionCode { get; set; }

    public string? SectionName { get; set; }

    public int? IsActive { get; set; }
}
