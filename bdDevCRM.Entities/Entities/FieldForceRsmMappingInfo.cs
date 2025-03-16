using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FieldForceRsmMappingInfo
{
    public int FieldForceRsmMappingId { get; set; }

    public int? HrRecordId { get; set; }

    public string? PsolocationCode { get; set; }

    public string? RsmregionCode { get; set; }

    public int? UserId { get; set; }

    public string? PsolocationName { get; set; }

    public string? DsmlocationCode { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public int? RsmregionId { get; set; }
}
