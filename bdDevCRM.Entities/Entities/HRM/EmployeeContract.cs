using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeContract
{
    public int ContractId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? ContractStartDate { get; set; }

    public DateOnly? ContractEndDate { get; set; }

    public int? Status { get; set; }
}
