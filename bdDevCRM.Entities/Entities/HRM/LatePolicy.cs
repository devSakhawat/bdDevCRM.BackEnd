using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LatePolicy
{
    public int LatePolicyId { get; set; }

    public string? LatePolicyName { get; set; }

    public decimal? FirstLateDays { get; set; }

    public decimal? FirstLateMin { get; set; }

    public decimal? FirstLateDeduction { get; set; }

    public decimal? SecoundLateDays { get; set; }

    public decimal? SecoundLateMin { get; set; }

    public decimal? SecoundLateDeduction { get; set; }

    public decimal? ThirdLateDays { get; set; }

    public decimal? ThirdLateMin { get; set; }

    public decimal? ThirdLateDeduction { get; set; }

    public int? IsActive { get; set; }

    public int? ShiftType { get; set; }

    public int? CompanyId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
