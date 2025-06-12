using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PfMembership
{
    public int PfmembershipId { get; set; }

    public int HrRecordId { get; set; }

    public DateOnly? MembershipDate { get; set; }

    public DateOnly? SalaryStartMonth { get; set; }

    public int? UpdateBy { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public int? Status { get; set; }

    public string? AttachedDocument { get; set; }

    public bool? IsProfitTaker { get; set; }

    public DateOnly? MembershipClosedDate { get; set; }

    public string? Remarks { get; set; }

    public decimal? PfdeductionPercentage { get; set; }
}
