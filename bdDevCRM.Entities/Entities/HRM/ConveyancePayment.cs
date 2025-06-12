using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ConveyancePayment
{
    public int PaymentId { get; set; }

    public DateTime? PrintDate { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<ConveyancePaymentDetails> ConveyancePaymentDetails { get; set; } = new List<ConveyancePaymentDetails>();
}
