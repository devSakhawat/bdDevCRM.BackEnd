using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ExClearanceParameter
{
    public int ParameterId { get; set; }

    public string ParameterName { get; set; } = null!;

    public int ParameterType { get; set; }

    public int IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
