using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ResignationRemarks
{
    public int ResignationId { get; set; }

    public string ResignationRemarksName { get; set; } = null!;

    public int IsActive { get; set; }
}
