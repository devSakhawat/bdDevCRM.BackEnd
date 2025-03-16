using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraYearConfigaration
{
    public int YearConfigId { get; set; }

    public string ConfigCode { get; set; } = null!;

    public string ConfigName { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? PartAweight { get; set; }

    public decimal? PartBweight { get; set; }

    public decimal? MaximumScore { get; set; }

    public int? IsActive { get; set; }

    public int? MidYearReviewOpen { get; set; }

    public int? YearendAssesmentOpen { get; set; }

    public decimal? MinimumAchivementPoint { get; set; }

    public decimal? MaximumAchivementPoint { get; set; }

    public int? FirstQuatarReviewOpen { get; set; }

    public int? ThirdQuatarReviewOpen { get; set; }
}
