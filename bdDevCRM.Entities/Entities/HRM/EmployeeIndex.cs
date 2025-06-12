using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeIndex
{
    public int EmployeeIndexId { get; set; }

    public int? HrRecordId { get; set; }

    public int? IndexSectionDetailsId { get; set; }
}
