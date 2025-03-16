using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfEmployeeWiseFieldForceMapping
{
    public int EmployeeWiseFieldForceId { get; set; }

    public int? CompanyId { get; set; }

    public int? HrRecordId { get; set; }

    public int? ZoneId { get; set; }

    public int? RegionId { get; set; }

    public int? AreaId { get; set; }

    public int? TerritoryId { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? UpdateUser { get; set; }
}
