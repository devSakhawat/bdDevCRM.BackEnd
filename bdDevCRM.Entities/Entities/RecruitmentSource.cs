using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecruitmentSource
{
    public int RecruitmentSourceId { get; set; }

    public string? RecruitmentSourceCode { get; set; }

    public string? RecruitmentSourceName { get; set; }

    public int? Status { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
