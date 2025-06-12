using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Bonustemp
{
    public int Hrrecordid { get; set; }

    public DateTime? Bonusmonth { get; set; }

    public DateTime? Generatedate { get; set; }

    public int? Bonustype { get; set; }

    public int? Bonusamount { get; set; }

    public int? Messageid { get; set; }

    public int? Stateid { get; set; }

    public int? Paymentmode { get; set; }

    public int? Accountsuserid { get; set; }

    public DateTime? Lastupdatedate { get; set; }

    public int? Approverid { get; set; }

    public DateTime? Approvedate { get; set; }
}
