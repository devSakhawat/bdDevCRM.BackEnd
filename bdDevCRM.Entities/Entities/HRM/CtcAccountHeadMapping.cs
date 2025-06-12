using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CtcAccountHeadMapping
{
    public int CtcAccountHeadMapId { get; set; }

    public int CtcId { get; set; }

    public int AccountHeadId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
