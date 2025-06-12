using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HcExpense
{
    public int ExpenseTypeId { get; set; }

    public string? ExpenseType { get; set; }
}
