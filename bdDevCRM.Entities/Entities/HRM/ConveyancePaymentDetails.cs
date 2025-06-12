using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ConveyancePaymentDetails
{
    public int CpdetailsId { get; set; }

    public int? PaymentId { get; set; }

    public int? MovementId { get; set; }

    public decimal? Amount { get; set; }

    /// <summary>
    /// 1=Conveyance For Movement, 2= Conveyance for OnSite Client
    /// </summary>
    public int? Mtype { get; set; }

    public virtual ConveyancePayment? Payment { get; set; }
}
