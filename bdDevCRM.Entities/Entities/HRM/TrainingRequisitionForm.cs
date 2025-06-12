using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingRequisitionForm
{
    public int TrainingRequisitionId { get; set; }

    public int? TrainingInfoId { get; set; }

    public DateOnly? ExpectedFromDate { get; set; }

    public DateOnly? ExpectedToDate { get; set; }

    public int? NumberOfParticipant { get; set; }

    public decimal? Duration { get; set; }

    public string? RequisitionRemarks { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? StateId { get; set; }

    public int? IsSubmitted { get; set; }

    public int? RequestedByHrRecordId { get; set; }

    public DateTime? RequestedDate { get; set; }

    public int? RecommendedBy { get; set; }

    public DateTime? RecommendedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? StateChangedBy { get; set; }

    public DateTime? StateChangedDate { get; set; }

    public int? IsPlanned { get; set; }
}
