using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class CrmapplicantCourseDetials
{
    public int ApplicantCourseDetailsId { get; set; }

    public int? CountryId { get; set; }

    public int? Institute { get; set; }

    public int? Course { get; set; }

    public int? IntakeMonth { get; set; }

    public int? IntakeYear { get; set; }

    public decimal? ApplicationFee { get; set; }

    public int? Currency { get; set; }

    public int? PaymentMethod { get; set; }

    public string? PaymentReferenceNumber { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? CourseRemarks { get; set; }
}
