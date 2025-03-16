using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecPresentationMarksHistory
{
    public int PresentationMarksId { get; set; }

    public int? ApplicantId { get; set; }

    public int? JobVacancyId { get; set; }

    public decimal? PresentationMarks { get; set; }

    public int? SavedBy { get; set; }

    public DateTime? SavedDate { get; set; }
}
