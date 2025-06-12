using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HospitalizationClaimMaster
{
    public int HospitalizationClaimMasterId { get; set; }

    public int? HrRecordId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DivisionId { get; set; }

    public int? CompanyId { get; set; }

    public int? DesignationId { get; set; }

    public int? BranchId { get; set; }

    public decimal? GrandTotal { get; set; }

    public DateTime? ClaimDate { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public int? UpdateDate { get; set; }
}
