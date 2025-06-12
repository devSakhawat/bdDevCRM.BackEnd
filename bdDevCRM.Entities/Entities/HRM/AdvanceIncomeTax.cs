using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AdvanceIncomeTax
{
    public int AitId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public decimal? CurrentBasic { get; set; }

    public int? TaxYearId { get; set; }

    public string? AitDescription { get; set; }

    public decimal? AitAmount { get; set; }

    public string? Attachment { get; set; }

    public int? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? IsActive { get; set; }

    public string? InvestDescription { get; set; }

    public decimal? InvestmentAmt { get; set; }

    public int? StateId { get; set; }
}
