using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Division
{
    public int DivisionId { get; set; }

    public string? DivisionCode { get; set; }

    public string? DivisionName { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
