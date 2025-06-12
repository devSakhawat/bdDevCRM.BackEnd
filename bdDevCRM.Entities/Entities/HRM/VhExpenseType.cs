using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhExpenseType
{
    public int ExpenseTypeId { get; set; }

    public string? ExpenseTypeName { get; set; }

    public int? IsActive { get; set; }

    public string? TypeCode { get; set; }
}
