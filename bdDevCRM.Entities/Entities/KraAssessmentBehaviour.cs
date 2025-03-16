using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraAssessmentBehaviour
{
    public int KraAssessmentId { get; set; }

    public string? Title { get; set; }

    public string? Definition { get; set; }

    /// <summary>
    /// 1=Common role Behaviour,2=Values Assessment
    /// </summary>
    public int? ValueType { get; set; }

    public virtual ICollection<KraAssessmentRating> KraAssessmentRating { get; set; } = new List<KraAssessmentRating>();
}
