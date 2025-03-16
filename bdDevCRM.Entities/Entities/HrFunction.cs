using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HrFunction
{
    public int FuncId { get; set; }

    public string? FunctionCode { get; set; }

    public string? FunctionName { get; set; }

    public string? JobDescription { get; set; }
}
