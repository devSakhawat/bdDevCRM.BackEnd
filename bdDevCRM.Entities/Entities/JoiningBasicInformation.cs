using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JoiningBasicInformation
{
    public int JoningBasicId { get; set; }

    public int? HrRecordId { get; set; }

    public decimal? JoiningBasicAmount { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
