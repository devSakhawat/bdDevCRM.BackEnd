using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalTempUploadTable
{
    public string? EmployeeId { get; set; }

    public string? AreaCode { get; set; }

    public decimal? MarkAchived { get; set; }

    public int? UserId { get; set; }
}
