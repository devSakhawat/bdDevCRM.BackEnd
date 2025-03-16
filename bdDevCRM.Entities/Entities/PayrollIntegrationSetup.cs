using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollIntegrationSetup
{
    public int PayrollIntegrationSetupId { get; set; }

    public int? MobileDeductionCtc { get; set; }

    public int? CanteenDeductionCtc { get; set; }

    public int? FirstLateDeductionCtc { get; set; }

    public int? SecondLateDeductionCtc { get; set; }

    public int? ThirdLateDeductionCtc { get; set; }

    public int? UnAuthorizeAbsentDeductionCtc { get; set; }

    public int? TadadeductionCtc { get; set; }

    public int? OtherDeductionCtc { get; set; }
}
