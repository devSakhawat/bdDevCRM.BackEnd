using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class McPolicy
{
    public int McPolicyId { get; set; }

    public string? McPolicyName { get; set; }

    public decimal? TotalCeiling { get; set; }

    public int? IsTotalCeilingUl { get; set; }

    public decimal? IsdCeiling { get; set; }

    public int? IsIsdCeilingUl { get; set; }

    public decimal? VasCeiling { get; set; }

    public int? IsVasCeilingUl { get; set; }

    public decimal? SmsCeiling { get; set; }

    public int? IsSmsCeilingUl { get; set; }

    public decimal? InternetCeiling { get; set; }

    public int? IsInternetCeilingUl { get; set; }

    public int? IsMcPolicyActive { get; set; }

    public int? CreateBy { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateOnly? UpdateDate { get; set; }
}
