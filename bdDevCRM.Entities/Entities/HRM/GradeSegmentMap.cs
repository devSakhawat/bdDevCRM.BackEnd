using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GradeSegmentMap
{
    public int GradeSegmentMapId { get; set; }

    public int? GradeSegmentId { get; set; }

    public int? GradeScaleId { get; set; }
}
