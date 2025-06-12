using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DailySalesAndCollection
{
    public int SalesCollectionId { get; set; }

    public int? HrRecordId { get; set; }

    public int? SalesTeamId { get; set; }

    public int? IsTeamLeader { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SalesTargetId { get; set; }

    public DateOnly? TxnDate { get; set; }

    public decimal? SalesAmount { get; set; }

    public decimal? CollectionAmount { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? UpdatedBy { get; set; }

    public int? ApproverId { get; set; }

    public DateOnly? ApproveDate { get; set; }

    public int? Status { get; set; }
}
