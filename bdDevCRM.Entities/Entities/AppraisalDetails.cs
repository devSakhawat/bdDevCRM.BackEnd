using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalDetails
{
    public int AppraisalDetailsId { get; set; }

    public int? AppraisalMasterId { get; set; }

    public int? CompetencyId { get; set; }

    public int? CompitencyAreaId { get; set; }

    public int? ImportanceId { get; set; }

    public int? RatingId { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? JobPerformanceDetailsId { get; set; }
}
