using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ConfirmationHeldup
{
    public int ConfirmationHeldupId { get; set; }

    public int? HrRecordId { get; set; }

    public bool? IsHeldUp { get; set; }

    public int? UserId { get; set; }

    public DateOnly? HeldupDate { get; set; }
}
