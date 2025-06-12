using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraPerformanceDetails
{
    public int KraPerformanceDetailId { get; set; }

    public int KraPerformanceId { get; set; }

    /// <summary>
    /// 1=Part A,2=Part B
    /// </summary>
    public int? KraType { get; set; }

    public string? KeyArea { get; set; }

    public string? Target { get; set; }

    public decimal? Weight { get; set; }

    public string? EligibleFactor { get; set; }

    public int? Considaration { get; set; }

    public string? MidYearReview { get; set; }

    public string? YearEndAchivement { get; set; }

    public string? AchivementPoint { get; set; }

    public string? SerialNo { get; set; }

    public int? Approver { get; set; }

    public DateOnly? ApproveDate { get; set; }
}
