using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ReportSignatoryMapping
{
    public int SignatoryMappingId { get; set; }

    public int CompanyId { get; set; }

    public int HrRecordId { get; set; }

    public string? Description { get; set; }

    public int SignatorySequenceNo { get; set; }

    public string? SignatoryDesignationName { get; set; }

    public int? ModuleId { get; set; }

    public int? CostCentreId { get; set; }

    public int? ReportMenuId { get; set; }
}
