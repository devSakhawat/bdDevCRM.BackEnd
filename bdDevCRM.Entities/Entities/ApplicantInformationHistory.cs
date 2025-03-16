using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicantInformationHistory
{
    public int ApplicantId { get; set; }

    public int? JobId { get; set; }

    public int? RequisitionReferenceId { get; set; }

    public int? JobVacancyId { get; set; }

    public string ApplicantName { get; set; } = null!;

    public string? ApplicantCode { get; set; }

    public string? NationalId { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? MobileNumber { get; set; }

    public string? PersonalEmail { get; set; }

    public int? AppliedPost { get; set; }

    public string? AttachmentCv { get; set; }

    public string? ApplicantAddress { get; set; }

    public string? ApplicantSurname { get; set; }

    public string? CurrentDesignation { get; set; }

    public DateTime? OfferLetterGenerateDate { get; set; }

    public int? IsOfferLetterGenerated { get; set; }

    public int? IsJoiningLetterGenerated { get; set; }

    public DateTime? JoiningLetterGenerateDate { get; set; }

    public int? IsAppointmentLetterGenerated { get; set; }

    public DateTime? AppointmentLetterGenerateDate { get; set; }

    public int? IsJoined { get; set; }

    public DateTime? JoiningDate { get; set; }

    public string? PresentCompanyExperienceYear { get; set; }

    public string? ExperienceYear { get; set; }

    public decimal? PresentSalary { get; set; }

    public string? CurrentCompany { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public int? IsAttendWrittenTest { get; set; }

    public decimal? WrittenMarks { get; set; }

    public DateTime? WrittenTestAttendDate { get; set; }

    public string? ApplicantPhotoPath { get; set; }

    public int? IsCallForWrittenTest { get; set; }

    public DateTime? WrittenTestCallDate { get; set; }

    public int? IsCallForFirstInterview { get; set; }

    public DateTime? FirstInterviewCallDate { get; set; }

    public int? IsCallForSecondInterview { get; set; }

    public DateTime? SecondInterviewCallDate { get; set; }

    public int? IsAttendFirstInterviewAlter { get; set; }

    public DateTime? FirstInterviewAttendDateAlter { get; set; }

    public decimal? FirstInterviewMarksAlter { get; set; }

    public int? IsAttendSecondInterviewAlter { get; set; }

    public DateTime? SecondInterviewAttendDateAlter { get; set; }

    public decimal? SecondInterviewMarksAlter { get; set; }

    public int? GenderId { get; set; }

    public int? StateId { get; set; }

    public int? IsActive { get; set; }

    public int? ApplicantSourceId { get; set; }

    public int? IsUploadedFromBulk { get; set; }

    public int? IsOfferLetterAccepted { get; set; }

    public int? IsJoiningLetterAccepted { get; set; }

    public int? IsAppointmentLetterAccepted { get; set; }

    public int? IsPcfprovided { get; set; }

    public DateTime? PcfprovidedDate { get; set; }

    public int? PcfprovidedBy { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Village { get; set; }

    public string? Po { get; set; }

    public string? Thana { get; set; }

    public string? District { get; set; }

    public string? ApplicantParmanentAddress { get; set; }

    public int? HomeDistrictId { get; set; }

    public string? ApplicantSource { get; set; }

    public string? Qualification { get; set; }

    public string? EducationalQualification { get; set; }

    public string? Institute { get; set; }

    public string? ReferenceType { get; set; }

    public string? Reference1 { get; set; }

    public string? Reference2 { get; set; }

    public string? Result { get; set; }

    public int? ApplicantStatus { get; set; }

    public int? InvitationTypeId { get; set; }

    public DateTime? InterViewDateFrom { get; set; }

    public DateTime? InterViewDateTo { get; set; }

    public int? IsRejectedFromWrittenTest { get; set; }

    public int? IsRejectedFromPresentation { get; set; }

    public int? IsCallForThirdInterview { get; set; }

    public int? IsSelectedFromInterview { get; set; }

    public int? IsRejectedFormInterview { get; set; }

    public int? RejectedByFromInterview { get; set; }

    public int? SelectedBy { get; set; }

    public DateTime? SelectedDate { get; set; }

    public int? RejectedBy { get; set; }

    public DateTime? RejectedDate { get; set; }

    public int? RecruitmentActivatedBy { get; set; }

    public DateTime? ActivatedDate { get; set; }

    public int? IsNeedPapers { get; set; }

    public int? SendForHrApprovalBy { get; set; }

    public DateTime? SendForHrApprovalDate { get; set; }

    public int? PipelinedBy { get; set; }

    public DateTime? PipelinedDate { get; set; }

    public int? RecommendedBy { get; set; }

    public DateTime? RecommendedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? OffereLetterGeneratedBy { get; set; }

    public DateTime? OfferLitterGenerateDate { get; set; }

    public int? SelectedByFromInterview { get; set; }

    public int? ReportingLine { get; set; }

    public int? IsCallForPresentation { get; set; }

    public DateTime? PresentationDate { get; set; }

    public int? NoticePeriod { get; set; }

    public int? HasRelative { get; set; }

    public string? PreffredDepartment { get; set; }

    public int? HasMedicalCondition { get; set; }

    public string? Relationship { get; set; }

    public string? NameOfRelative { get; set; }

    public string? RelativeOrganization { get; set; }

    public string? RelativePosition { get; set; }

    public string? MedicalDescription { get; set; }

    public string? CtcPayslipFile { get; set; }

    public DateTime? LastCvUpdateDate { get; set; }

    public int? PrimaryShortlistedBy { get; set; }

    public DateTime? PrimaryShoerlistDate { get; set; }

    public int? ActivateFromPipelineBy { get; set; }

    public DateTime? ActivateFromPipeLineDate { get; set; }

    public string? ReferralDetails { get; set; }

    public int? TotalExperienceYear { get; set; }

    public int? ShortlistedBy { get; set; }

    public DateTime? ShortlistedDate { get; set; }
}
