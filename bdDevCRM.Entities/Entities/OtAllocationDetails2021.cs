using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtAllocationDetails2021
{
    public int OtAllocationDetailsId { get; set; }

    public int OtAllocationId { get; set; }

    public int HrRecordId { get; set; }

    public decimal OtAllocatedHour { get; set; }

    public DateOnly OtFromDate { get; set; }

    public DateOnly OttoDate { get; set; }

    public int? OtAllocationStart { get; set; }

    public int IsActive { get; set; }

    public TimeOnly? OtTimeFrom { get; set; }

    public TimeOnly? OtTimeTo { get; set; }

    public int? HasBreakUp { get; set; }
}
