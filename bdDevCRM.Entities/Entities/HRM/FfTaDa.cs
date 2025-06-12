using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfTaDa
{
    public int TaDaId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CompanyId { get; set; }

    public decimal? TotalDays { get; set; }

    public int? IsCurrentTerritory { get; set; }

    public int? CtcId { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? SalaryMonth { get; set; }

    public int? StateId { get; set; }

    public int? FieldBenifitCategoryId { get; set; }

    public decimal? BenifitRate { get; set; }

    public int? IsDaily { get; set; }

    public int? DeleteBy { get; set; }

    public DateTime? DeleteDate { get; set; }
}
