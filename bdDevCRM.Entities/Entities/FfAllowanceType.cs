using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfAllowanceType
{
    public int AllowanceTypeId { get; set; }

    public string? AllowanceTypeName { get; set; }

    public int? IsMonthlyDaily { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateUser { get; set; }
}
