using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecJobVacancy
{
    public int RecJobVacancyId { get; set; }

    public int? RecuisitionReferenceId { get; set; }

    public int? JobId { get; set; }

    public decimal SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }

    public int? IsNewRecruitment { get; set; }

    public int? IsReplacement { get; set; }

    public string? ReplacementReason { get; set; }

    public int? IsPublishToDashboard { get; set; }

    public int? IsPublishToSocialMedia { get; set; }

    public int? IsPublishToJobPortal { get; set; }

    public int? IsPublishToBdJobs { get; set; }

    public string? OtherAdvertisement { get; set; }

    public DateTime? JobPublishDate { get; set; }

    public DateTime? JobExpireDate { get; set; }

    public int? AddedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? IsPublished { get; set; }

    public int? IsNegotiable { get; set; }

    public int? JobPortalRefNo { get; set; }

    public string? PostedJobReferenceNo { get; set; }

    public int? Status { get; set; }

    public string? TalentAssesmentOfficeCircularPath { get; set; }

    public string? CvsortingCommitteOfficeCircularPath { get; set; }

    public int? IsPublishToNewsPaper { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? ApproveBy { get; set; }

    public string? JobVacancyReportPath { get; set; }

    public int? JobVacancyStateId { get; set; }

    public int? DeadlineCvsorting { get; set; }

    public DateTime? LastDeadlineDate { get; set; }

    public string? TalentAssesmentRefNo { get; set; }

    public string? CvsortingRefNo { get; set; }

    public string? EmploymentStatus { get; set; }

    public int? PublishedBy { get; set; }

    public int? IsJobVacancyPostpone { get; set; }

    public string? InterviewerVanue { get; set; }

    public string? InterviewerSignatoryId { get; set; }

    public string? InterviewerDesgExtra { get; set; }

    public string? CvsortingCopyTo { get; set; }

    public string? CvsortingSignatoryId { get; set; }

    public string? CvsortingDesgExtra { get; set; }

    public int? CvsortingMode { get; set; }

    public string? CvsortingModeInfo { get; set; }

    public int? InterviewMode { get; set; }

    public string? InterviewModeInfo { get; set; }

    public string? CvsortingModeVenue { get; set; }

    public string? InterviewModeVenue { get; set; }

    public string? InterviewPanelCopyTo { get; set; }

    public int? GradeTypeId { get; set; }
}
