using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfFieldBenifitCategory
{
    public int FieldBenifitCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcRate { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public DateOnly? EffectiveEndDate { get; set; }

    public int? IsDaily { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
