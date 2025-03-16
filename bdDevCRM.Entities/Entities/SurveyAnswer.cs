using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SurveyAnswer
{
    public int SurveyAnswerId { get; set; }

    public int? HrrecordId { get; set; }

    public int? SurveyId { get; set; }

    public int? SurveyQuestionId { get; set; }

    public int? SqanswerId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    /// <summary>
    /// -1=Defauls/Null,0=Save As Draft, 1=Publish
    /// </summary>
    public int? SurveyAnswerStatus { get; set; }

    public int? SortOrder { get; set; }

    public int? ApproversHrRecordId { get; set; }
}
