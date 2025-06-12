using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecruitmentType
{
    public int RecruitmentTypeId { get; set; }

    public string? RecruitmentTypeName { get; set; }

    public string? RecruitmentTypeCode { get; set; }

    public int? Status { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
