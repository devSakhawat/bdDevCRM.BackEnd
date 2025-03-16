using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DottedLineCompanyMapping
{
    public int DottedLineCompanyMapId { get; set; }

    public int? DottedLineEmailConfigId { get; set; }

    public int? CompanyId { get; set; }
}
