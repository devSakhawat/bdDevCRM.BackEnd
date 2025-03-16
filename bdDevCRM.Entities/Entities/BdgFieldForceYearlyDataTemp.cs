using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgFieldForceYearlyDataTemp
{
    public int FieldForceYearlyDataId { get; set; }

    public int? YearId { get; set; }

    public int? HrrecordId { get; set; }

    public decimal? SalesAchieved { get; set; }

    public decimal? Ytdrx { get; set; }

    public decimal? GrowthNew { get; set; }

    public decimal? GrowthOld { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UserId { get; set; }

    public string? EmployeeId { get; set; }
}
