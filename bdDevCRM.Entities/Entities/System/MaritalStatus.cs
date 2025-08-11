using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.System;

public partial class MaritalStatus
{
    public int MaritalStatusId { get; set; }

    public string? MaritalStatusName { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
