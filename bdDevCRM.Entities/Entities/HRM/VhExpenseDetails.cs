using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhExpenseDetails
{
    public int ExpenseDetailId { get; set; }

    public int? ExpnsAndReimbrsmntId { get; set; }

    public int? ExpenseTypeId { get; set; }

    public decimal? Amount { get; set; }
}
