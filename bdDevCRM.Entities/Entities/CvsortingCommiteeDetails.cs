using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CvsortingCommiteeDetails
{
    public int CvSortingCommiteeId { get; set; }

    public int? CvSortingCommiteeMemberId { get; set; }

    public int? JobVacancyId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? JobId { get; set; }

    public int? CommitteType { get; set; }
}
