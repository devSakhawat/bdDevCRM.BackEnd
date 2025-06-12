using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CtcCategory
{
    /// <summary>
    /// 1=Master Category,2=Additional Category
    /// </summary>
    public int CtcCategoryId { get; set; }

    public string? CtcCategoryName { get; set; }
}
