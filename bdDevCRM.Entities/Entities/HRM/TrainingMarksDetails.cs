using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingMarksDetails
{
    public int TrainingMarksDetailsId { get; set; }

    public int? TrainingMarkId { get; set; }

    public int? HrRecordId { get; set; }

    public decimal? Mark { get; set; }
}
