using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ExClearanceSetupMaster
{
    public int ClearanceSetupMasterId { get; set; }

    public int CompanyId { get; set; }

    public int? DivisionId { get; set; }

    public decimal Sequence { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsSendEmail { get; set; }

    public bool? IsApplyToAll { get; set; }

    public int? DepartmentId { get; set; }

    public bool? IsDefault { get; set; }

    public int? Branchid { get; set; }

    public int? TeamId { get; set; }

    public int? ResponsibleHrRecordId { get; set; }

    public int? ResponsibleEndPointId { get; set; }
}
