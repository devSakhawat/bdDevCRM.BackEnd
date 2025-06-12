using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AcrSalaryFixation
{
    public int AcrId { get; set; }

    public int DesignationId { get; set; }

    public int GradeId { get; set; }

    public decimal BasicSalary { get; set; }

    public DateTime EffectiveDate { get; set; }

    public DateTime NextReviewDate { get; set; }

    public int? Id { get; set; }

    public int? OldGradeId { get; set; }

    public int? OldDesignationId { get; set; }
}
