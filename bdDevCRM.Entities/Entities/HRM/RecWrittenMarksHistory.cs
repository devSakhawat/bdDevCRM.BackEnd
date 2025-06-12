using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecWrittenMarksHistory
{
    public int WrittenMarksId { get; set; }

    public int? ApplicantId { get; set; }

    public int? JobVacancyId { get; set; }

    public decimal? WrittenMarks { get; set; }

    public int? SavedBy { get; set; }

    public DateTime? SavedDate { get; set; }
}
