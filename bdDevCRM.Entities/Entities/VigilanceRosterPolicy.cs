using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VigilanceRosterPolicy
{
    public int Id { get; set; }

    public byte? CompanyId { get; set; }

    public int? HrrecordId { get; set; }

    public TimeOnly? GraceTime { get; set; }

    public int? Leave { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? CreateDate { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }
}
