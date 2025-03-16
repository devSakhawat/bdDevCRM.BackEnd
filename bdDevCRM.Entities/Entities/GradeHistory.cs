using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GradeHistory
{
    public int GradeHistoryId { get; set; }

    public int? GradeId { get; set; }

    public string? GradeName { get; set; }

    public int? IsActive { get; set; }

    public decimal? GrossPay { get; set; }

    public decimal? CostToCompany { get; set; }

    public string? GradeCode { get; set; }

    public int? SalaryYearId { get; set; }

    public int? AssessmentYearId { get; set; }

    public int? IsProvidedByCompany { get; set; }

    public int? NetTaxPayable { get; set; }

    public int? GradeTypeId { get; set; }

    public int? IsHospitalizationAvailable { get; set; }

    public decimal? MaxHospitalizationAmount { get; set; }

    public int? PaybandId { get; set; }

    public int? Type { get; set; }

    public int? PayrollType { get; set; }

    public int? SortOrder { get; set; }

    public int? GradeLevelType { get; set; }

    public DateTime? UpdateDate { get; set; }
}
