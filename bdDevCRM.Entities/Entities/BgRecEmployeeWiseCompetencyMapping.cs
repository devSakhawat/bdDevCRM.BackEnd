using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BgRecEmployeeWiseCompetencyMapping
{
    public int CompetencyMappingId { get; set; }

    public int? CompetencyId { get; set; }

    public int? HrRecordId { get; set; }

    public int? JobVacancyId { get; set; }
}
