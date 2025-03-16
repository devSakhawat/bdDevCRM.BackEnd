using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DottedLineGradeMapping
{
    public int DottedLineGradeMapId { get; set; }

    public int? DottedLineEmailConfigId { get; set; }

    public int? GradeTypeId { get; set; }

    public int? CompanyId { get; set; }
}
