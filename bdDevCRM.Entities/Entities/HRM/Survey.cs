using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Survey
{
    public int SurveyId { get; set; }

    public string? SurveyTitle { get; set; }

    public int? SurveyCategoryId { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// 0=false,1=True
    /// </summary>
    public int? IsAnnonymous { get; set; }

    /// <summary>
    /// 0=Draft/No, 1=Published/Yes, 2 = Cancel
    /// </summary>
    public int? IsPublished { get; set; }

    public DateTime? PublishedDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? HitCount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? IsManagerialSurvey { get; set; }
}
