using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeVariableAllowanceDetails
{
    public int EmployeeVariableAllowanceId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? AllowanceDate { get; set; }

    public decimal? Amount { get; set; }

    public int? SubmitBy { get; set; }

    public DateTime? SubmitDate { get; set; }

    public int? VariableAllowanceMasterId { get; set; }

    public int? AllowanceTypeId { get; set; }
}
