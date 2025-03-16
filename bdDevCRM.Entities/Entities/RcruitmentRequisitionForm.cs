using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RcruitmentRequisitionForm
{
    public int JobVacancyId { get; set; }

    public int? RequestedByHrRecordId { get; set; }

    public int? RequestedBySbu { get; set; }

    public string? JobTitle { get; set; }

    public int? VacancyLocation { get; set; }

    public int? VacnacyDepartment { get; set; }

    public DateTime? RequestedDate { get; set; }

    public string? JobResponsibility { get; set; }

    public string? EducationalRequirment { get; set; }

    public string? ExperienceRequirment { get; set; }

    public string? AdditionalRequirment { get; set; }

    public DateTime? JobAnnounceDate { get; set; }

    public DateTime? JobAnnounceExpireDate { get; set; }

    public int? NoOfVacancy { get; set; }

    public string? JobProfileAttacement { get; set; }

    public int? Status { get; set; }

    public DateTime? RecomendDate { get; set; }

    public int? RecomendedBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? ApproverId { get; set; }

    public string? Remarks { get; set; }

    public int? DesignationId { get; set; }

    public DateTime? ReviewDate { get; set; }

    public int? Reviewedby { get; set; }

    public int? IsRecruitment { get; set; }

    public int? IsReplacement { get; set; }

    public int? JobTitleId { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public DateTime? DesiredEmploymetDate { get; set; }

    public string? Reason { get; set; }

    public string? LanguagesRequirement { get; set; }

    public int? IsSpecifyBudget { get; set; }

    public int? IsNotSpecifyBudget { get; set; }

    public int? IsOpenEndedContract { get; set; }

    public int? IsTermContract { get; set; }

    public DateTime? TermContractValidFrom { get; set; }

    public DateTime? TermContractValidTo { get; set; }

    public string? Classification { get; set; }

    public string? ProfessionalCategory { get; set; }

    public decimal? AnnualPackageCharge { get; set; }

    public string? NewRecruitmentRemarks { get; set; }

    public int? IsReplacementDuetoDeparture { get; set; }

    public int? IsDismissal { get; set; }

    public int? IsRetirement { get; set; }

    public int? IsResignation { get; set; }

    public int? IsReplacementDueToTransfer { get; set; }

    public string? ReplacementDueToTransferRemarks { get; set; }

    public int? IsReplacementDueToAbsence { get; set; }

    public string? ReplacementDueToAbsenceRemarks { get; set; }

    public string? ReplacedPersonDetail { get; set; }

    public string? ReplacementReasonOtherDueToDeparture { get; set; }

    public int? IsReplacementReasonOtherDueToDeparture { get; set; }

    public int? NoOfPresentManpower { get; set; }
}
