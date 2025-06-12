using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OutletVisitingSchedule
{
    public int OutletVisitingScheduleId { get; set; }

    public int HrRecordId { get; set; }

    public int? RsmRegionId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
