using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NomineeDetails
{
    public int NomineeDetailsId { get; set; }

    public int HrRecordId { get; set; }

    /// <summary>
    /// 1=Hospitalization;2=Group Life Insurance;3=PF;4=Gratuity;
    /// </summary>
    public int? BenefitType { get; set; }

    public string? BenefitName { get; set; }

    public int? NomineeId { get; set; }

    public decimal? NomineeShare { get; set; }

    public int? UpdateBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? Status { get; set; }
}
