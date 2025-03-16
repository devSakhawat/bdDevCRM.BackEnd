using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhExpenseMaster
{
    public int ExpenseMasterId { get; set; }

    public int? ExpnsAndReimbrsmntId { get; set; }

    public DateTime? ServicingDate { get; set; }

    public string? FilePath { get; set; }
}
