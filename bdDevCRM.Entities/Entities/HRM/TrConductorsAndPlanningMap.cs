using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrConductorsAndPlanningMap
{
    public int ConductorsAndPlanningMapId { get; set; }

    public int? TrainingPlanningId { get; set; }

    public int? InstituteOrInstructorId { get; set; }

    public int? FacilationDays { get; set; }
}
