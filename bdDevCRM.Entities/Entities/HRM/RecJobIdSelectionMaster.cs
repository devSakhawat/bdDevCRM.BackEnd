using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecJobIdSelectionMaster
{
    public int JobIdSelectionMasterId { get; set; }

    public int? RefNumber { get; set; }

    public string? RefCode { get; set; }

    public int? TotalVacancy { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? StateId { get; set; }

    public bool? IsNeedReview { get; set; }

    public string? RequisitionComment { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? RequisitionStateId { get; set; }

    public string? ManpowerRequistionReportFilePath { get; set; }
}
