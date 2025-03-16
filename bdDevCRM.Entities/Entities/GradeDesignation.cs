using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GradeDesignation
{
    public int GradeDesignationId { get; set; }

    public int GradeId { get; set; }

    public int DesignationId { get; set; }

    public int? IsActive { get; set; }

    public int? SalaryYear { get; set; }

    public int? AssessmentYear { get; set; }

    public int? TaxProvidedByCompany { get; set; }

    public int? NetTaxPayable { get; set; }

    public int? IsHospitalizationAvailable { get; set; }

    public decimal? MaxClaimedAmount { get; set; }

    public decimal? CtcAmount { get; set; }

    public DateOnly? EntryDate { get; set; }

    public DateOnly? UpdateDate { get; set; }
}
