using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class IndexSection
{
    public int IndexSectionId { get; set; }

    public string? IndexSectionName { get; set; }

    public int? IsActive { get; set; }
}
