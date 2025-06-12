using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompanyDivisionMap
{
    public int SbuDivisionMapId { get; set; }

    public int CompanyId { get; set; }

    public int DivisionId { get; set; }
}
