using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NoticeEmployee
{
    public int NoticeEmployeeId { get; set; }

    public int? NoticeId { get; set; }

    public int? HrrecordId { get; set; }

    public int? ViewStatus { get; set; }
}
