using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RcruitmentJobVacancy
{
    public int JobVacancyId { get; set; }

    public int? RequestedByHrRecordId { get; set; }

    public int? RequestedBySbu { get; set; }

    public string? JobTitle { get; set; }

    public int? VacancyLocation { get; set; }

    public int? VacnacyDepartment { get; set; }

    public DateTime? DesiredEmploymetDate { get; set; }

    public string? JobResponsibility { get; set; }

    public string? EducationalRequirment { get; set; }

    public string? ExperienceRequirment { get; set; }

    public string? AdditionalRequirment { get; set; }

    public DateTime? JobAnnounceDate { get; set; }

    public DateTime? JobAnnounceExpireDate { get; set; }

    public int? NoOfVacancy { get; set; }

    public string? JobProfileAttacement { get; set; }

    /// <summary>
    /// Status 0=Unpulish,1=Publish
    /// </summary>
    public int? Status { get; set; }

    public DateTime? RecomendDate { get; set; }

    public int? RecomendedBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? ApproverId { get; set; }

    public string? Remarks { get; set; }

    public DateTime? AddProvidedDate { get; set; }

    public DateTime? CvcollectedDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public int? IsRecruitment { get; set; }

    public int? IsReplacement { get; set; }

    public int? JobTitleId { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public string? Reason { get; set; }

    public string? ReferenceId { get; set; }

    public int? AddSourceId { get; set; }

    public int? NoOfPresentManpower { get; set; }

    public DateTime? RequestedDate { get; set; }

    public int? SalaryRange { get; set; }

    public int? RequisitionId { get; set; }

    public string? RequisitionReference { get; set; }

    public int? Designation { get; set; }

    public int? SalaryRangeMax { get; set; }

    public int? YearOfExperienceMin { get; set; }

    public int? YearOfExperienceMax { get; set; }

    public int? AgeRangeMin { get; set; }

    public int? AgeRangeMax { get; set; }

    public int? IsPublishedToDashboard { get; set; }

    public int? IsPublishToJobPortal { get; set; }

    public int? JobPortalRefNo { get; set; }

    public string? PostedJobReferenceNo { get; set; }

    public int? IsPublished { get; set; }

    public string? Classification { get; set; }

    public string? LanguagesRequirement { get; set; }

    public int? ProfessionalCategory { get; set; }

    public string? RequirmentForThisPoistion { get; set; }

    public int? VacancyStatusType { get; set; }

    public int? JobGrade { get; set; }

    public int? JobGroupId { get; set; }

    public int? PositionCategory { get; set; }

    public DateTime? AgeOnDate { get; set; }

    public int? TrackingNumberStart { get; set; }

    public string? ServiceArea { get; set; }

    public string? ReadBeforeApply { get; set; }

    public int? RollNumberStart { get; set; }

    public int? FreedomFighterMaxAge { get; set; }

    public int? FreedomFighterMinAge { get; set; }

    public int? IsSingleWorkingArea { get; set; }

    public int? IsMultipleWorkingArea { get; set; }

    public string? OtherBenefits { get; set; }
}
