using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraCompetencies
{
    public int KraCompetenciesId { get; set; }

    public int? KraPerformanceId { get; set; }

    public string? LastYearKey { get; set; }

    public DateTime? ReviewDate { get; set; }

    public string? CommentsByEmployee { get; set; }

    public string? CommentsByLineManager { get; set; }

    public virtual ICollection<KraAssessmentRating> KraAssessmentRating { get; set; } = new List<KraAssessmentRating>();

    public virtual ICollection<KraCompetencyRating> KraCompetencyRating { get; set; } = new List<KraCompetencyRating>();

    public virtual ICollection<KraDevelopmentPlan> KraDevelopmentPlan { get; set; } = new List<KraDevelopmentPlan>();

    public virtual KraPerformance? KraPerformance { get; set; }
}
