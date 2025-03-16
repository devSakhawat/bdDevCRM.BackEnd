using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FieldForceArchive2021
{
    public int FieldForceId { get; set; }

    public DateTime FieldForceDate { get; set; }

    public int HrrecordId { get; set; }

    public string IsPresent { get; set; } = null!;

    public int IsApproved { get; set; }

    public int CompanyId { get; set; }

    public int BranchId { get; set; }

    public int? FuncId { get; set; }

    public string? RsmregionCode { get; set; }

    public string? PsolocationCode { get; set; }

    public int? EmployeeType { get; set; }

    public int? IsUpdateBySms { get; set; }

    public int? ShiftId { get; set; }

    public string? OfficeTimeIn { get; set; }

    public string? OfficeTimeOut { get; set; }

    public int? IsNightShift { get; set; }

    public int? BrakupDuration { get; set; }

    public int? IsNightAllowanceApp { get; set; }

    public decimal? GraceIn { get; set; }

    public decimal? GraceOut { get; set; }
}
