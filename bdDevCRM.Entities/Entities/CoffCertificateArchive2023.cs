using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CoffCertificateArchive2023
{
    public int CoffId { get; set; }

    public int? HrrecordId { get; set; }

    public DateOnly? DateOfWork { get; set; }

    public string? ReasonOfWork { get; set; }

    public int? IsRecommanded { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? RecommanderId { get; set; }

    public DateTime? RecommandDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? StateId { get; set; }

    public int? IsApplied { get; set; }

    public int? IsHalfDay { get; set; }
}
