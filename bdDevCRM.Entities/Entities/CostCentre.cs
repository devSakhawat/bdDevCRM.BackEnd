using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CostCentre
{
    public int CostCentreId { get; set; }

    public string? CostCentreName { get; set; }

    public int? ParentCostCentreId { get; set; }

    public string? CcDescription { get; set; }

    public int? IsActive { get; set; }

    public string? CostCentreCode { get; set; }

    public string? SjvNumber { get; set; }

    public int? SalaryCompanyMappingId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
