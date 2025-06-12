using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Bonus
{
    public int Bonusid { get; set; }

    public int Hrrecordid { get; set; }

    public DateTime Bonusmonth { get; set; }

    public DateTime Generatedate { get; set; }

    /// <summary>
    /// 1=Festival Bonus, 2=Performance Bonus, 3=Project Bonus, 4=Profit Sharing
    /// </summary>
    public int? Bonustype { get; set; }

    public int? Bonusamount { get; set; }

    public int? Messageid { get; set; }

    public int? Stateid { get; set; }

    public string? Paymentmode { get; set; }

    public int? Accountsuserid { get; set; }

    public DateTime? Lastupdatedate { get; set; }

    public int? Approverid { get; set; }

    public DateTime? Approvedate { get; set; }
}
