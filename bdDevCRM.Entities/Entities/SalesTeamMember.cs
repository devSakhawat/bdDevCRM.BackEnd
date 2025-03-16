using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalesTeamMember
{
    public int SalesTeamMemberId { get; set; }

    public int? SalesTeamId { get; set; }

    public int? HrRecordId { get; set; }

    public int? IsTeamLeader { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? UpdatedBy { get; set; }
}
