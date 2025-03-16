using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SurveyEmployee
{
    public int SurveyEmployeeId { get; set; }

    public int? SurveyId { get; set; }

    public int? HrrecordId { get; set; }
}
