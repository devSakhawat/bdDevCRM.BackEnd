using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AcrSupervisorAssessment
{
    public int Id { get; set; }

    public int AcrId { get; set; }

    public decimal? GrandTotal { get; set; }

    public decimal? GrandTotalObtain { get; set; }

    public string? SummerizeRating { get; set; }

    public string? TrainingRequirement { get; set; }

    public int? ValueAddition { get; set; }

    public int? ShortCummingPointOut { get; set; }

    public int? OverShortComming { get; set; }

    public int? WillAddValueNextYear { get; set; }

    public int? RemovalOrPromotion { get; set; }

    public int? FitForIncrement { get; set; }

    public decimal? IncrementAmount { get; set; }

    public int? Acelerated { get; set; }

    public int? Normal { get; set; }

    public int? SuitableForAssignment { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public DateTime WorkedUnderMeFromDate { get; set; }

    public DateTime WorkedUnderMeToDate { get; set; }

    public int? Gradings { get; set; }
}
