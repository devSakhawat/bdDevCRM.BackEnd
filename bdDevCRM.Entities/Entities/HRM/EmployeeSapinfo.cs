using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeSapinfo
{
    public int EmpSapinfoId { get; set; }

    public int? HrRecordId { get; set; }

    public string? SapcompanyCode { get; set; }

    public string? SapcostCentreCode { get; set; }

    public string? SapprofitCentreCode { get; set; }

    public int? UserId { get; set; }
}
