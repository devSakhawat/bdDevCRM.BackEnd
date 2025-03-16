using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempApplicantWrittenMarks
{
    public int ApplicantMarksUploadId { get; set; }

    public int? ApplicantId { get; set; }

    public string? Name { get; set; }

    public decimal? WrittenMarks { get; set; }

    public DateTime? WrittenDate { get; set; }

    public int? AppliedPost { get; set; }

    public int? UserId { get; set; }
}
