using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingPlanningAndRequisitionMap
{
    public int TrainingPlanningAndRequisitionMapId { get; set; }

    public int TrainingPlanningId { get; set; }

    public int TrainingRequisitionId { get; set; }
}
