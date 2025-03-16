using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SurveyDetails
{
    public int SurveyDetailsId { get; set; }

    public int? SurveyId { get; set; }

    public int? SurveyQuestionId { get; set; }
}
