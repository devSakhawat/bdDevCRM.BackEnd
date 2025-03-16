using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CostCenterAccountHeadMapping
{
    public int CcAccountHeadMapId { get; set; }

    public int CostCenterId { get; set; }

    public int AccountHeadId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
