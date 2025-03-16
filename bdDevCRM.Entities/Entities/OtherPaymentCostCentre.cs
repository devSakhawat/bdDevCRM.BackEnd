using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtherPaymentCostCentre
{
    public int OtherPaymentCostcentreHisId { get; set; }

    public DateTime? PaymentMonth { get; set; }

    public int? CtcId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CostCentreId { get; set; }

    public int? CostShareRatio { get; set; }
}
