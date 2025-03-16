using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrOrganizerAndPlanningMap
{
    public int OrganizerAndPlanningMapId { get; set; }

    public int? TrainingPlanningId { get; set; }

    public int? OrganizerId { get; set; }
}
