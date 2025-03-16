using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgKpiConfig
{
    public int KpiConfigId { get; set; }

    public string? KpiConfigCode { get; set; }

    public string? KpiConfigName { get; set; }

    public int? IsDoublePromotion { get; set; }

    public int? IsSinglePromotion { get; set; }

    public int? IsNormalIncrement { get; set; }

    public int? IsGradebenefitOnly { get; set; }

    public int? IsNoIncrement { get; set; }

    public int? IsBasicIncrement { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? IsApplytoContractualDl { get; set; }

    public int? IsUnion { get; set; }

    public int? IsExcludeProbationary { get; set; }

    public int? IsExcludeUnionProbationary { get; set; }
}
