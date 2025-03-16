using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FacilitySectionMap
{
    public int FacilitySectionMapId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }
}
