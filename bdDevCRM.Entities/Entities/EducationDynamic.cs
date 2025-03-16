using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EducationDynamic
{
    public int EducationHistoryDynamicId { get; set; }

    public int HrrecordId { get; set; }

    public string? Certificate { get; set; }

    public int? DisciplineId { get; set; }

    public int? YearofcompletionId { get; set; }

    public string? Result { get; set; }

    public int? BoardInstituteId { get; set; }

    public int? Certificatetypeid { get; set; }

    public int? ResultStatusId { get; set; }

    public int? IsConsiderInc { get; set; }
}
