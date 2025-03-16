using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingType
{
    public int TrainingTypeId { get; set; }

    public string TrainingTypeName { get; set; } = null!;

    public string? TrainingTypeCode { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
