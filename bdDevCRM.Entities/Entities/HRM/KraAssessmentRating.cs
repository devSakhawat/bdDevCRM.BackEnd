using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraAssessmentRating
{
    public int KraAssessmentRatingId { get; set; }

    public int? KraAssessmentId { get; set; }

    public int? Rating { get; set; }

    public int? KraCompetenciesId { get; set; }

    public virtual KraAssessmentBehaviour? KraAssessment { get; set; }

    public virtual KraCompetencies? KraCompetencies { get; set; }
}
