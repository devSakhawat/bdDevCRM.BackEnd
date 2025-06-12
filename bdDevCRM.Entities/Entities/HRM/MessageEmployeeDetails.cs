using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MessageEmployeeDetails
{
    public int MessageEmpDtlId { get; set; }

    public int? MessageId { get; set; }

    public int? HrRecordId { get; set; }

    public int? Status { get; set; }
}
