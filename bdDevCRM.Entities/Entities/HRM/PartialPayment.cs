using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PartialPayment
{
    public int PartialPaymentId { get; set; }

    public int? SalaryId { get; set; }

    public decimal? PaymentPercentage { get; set; }

    public decimal? PaymentAmount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public int? PaymentBy { get; set; }
}
