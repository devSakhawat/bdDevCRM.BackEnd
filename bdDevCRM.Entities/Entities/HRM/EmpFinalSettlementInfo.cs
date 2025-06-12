using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmpFinalSettlementInfo
{
    public int EmpFinalSettleInfoId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? ResignationReceiveDate { get; set; }

    public DateOnly? HrReceiveDate { get; set; }

    public DateOnly? SentToFinanceDate { get; set; }

    public DateOnly? CcissueDate { get; set; }

    public int? StatusId { get; set; }

    public DateOnly? ProcessCompleteDate { get; set; }

    public string? Remarks { get; set; }
}
