using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicantOfferLetterInfo
{
    public int Id { get; set; }

    public int? ApplicantId { get; set; }

    public DateTime? JoiningDate { get; set; }

    public DateTime? OfferDate { get; set; }

    public int? ProbationMonth { get; set; }

    public int? CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int? BranchId { get; set; }

    public string? BranchName { get; set; }

    public int? DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public int? CostCenterId { get; set; }

    public string? CostCenterName { get; set; }

    public int? DesignationId { get; set; }

    public string? DesignationName { get; set; }

    public string? OfferCopyTo { get; set; }

    public string? OfferRefNo { get; set; }

    public string? OfferSignatory { get; set; }

    public decimal? ProbationarySalary { get; set; }

    public string? AppointmentLetterRefNo { get; set; }

    public DateTime? AppointmentEffectiveDate { get; set; }

    public DateTime? AppointmentLetterIssueDate { get; set; }

    public int? PaygradeId { get; set; }

    public string? AppointmentLetterCopyTo { get; set; }

    public int? CompanyNoticePay { get; set; }

    public int? EmployeeNoticePay { get; set; }

    public int? AppointmentLetterSignatory { get; set; }

    public DateTime? OfferExpireDate { get; set; }

    public DateTime? JoiningLetterEffectiveDate { get; set; }

    public string? JoiningLetterRefNo { get; set; }

    public int? JoiningLetterSignatory { get; set; }

    public int? JoiningLetterReportingLine { get; set; }

    public string? ReportingTo { get; set; }

    public int? DivisionId { get; set; }

    public string? JoiningLetterSignatoryName { get; set; }

    public string? JoiningLetterReportingLineName { get; set; }
}
