using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingRequisitParticipant
{
    public int TrainingRequisitParticipantId { get; set; }

    public int TrainingRequisitionId { get; set; }

    public int TrainingInfoId { get; set; }

    public int HrRecordId { get; set; }

    public int? IsSelected { get; set; }

    public string? SelectionStatus { get; set; }
}
