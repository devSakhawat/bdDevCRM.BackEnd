using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicantInformation
{
    public int ApplicantId { get; set; }

    public string ApplicantName { get; set; } = null!;

    public string? NationalId { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? MobileNumber { get; set; }

    public string? PersonalEmail { get; set; }

    /// <summary>
    /// Job Vacancy
    /// </summary>
    public int? AppliedPost { get; set; }

    public string? AttachmentCv { get; set; }

    public string? ApplicantAddress { get; set; }

    public string? ApplicantSurname { get; set; }

    public string? ApplicantSource { get; set; }

    public string? Qualification { get; set; }

    public string? CurrentDesignation { get; set; }

    public DateTime? OfferLetterGenerateDate { get; set; }

    public int? IsOfferLetterGenerated { get; set; }

    public int? IsJoiningLetterGenerated { get; set; }

    public DateTime? JoiningLetterGenerateDate { get; set; }

    public int? IsAppointmentLetterGenerated { get; set; }

    public DateTime? AppointmentLetterGenerateDate { get; set; }

    public int? IsJoined { get; set; }

    public DateTime? JoiningDate { get; set; }

    public string? ExperienceYear { get; set; }

    public decimal? PresentSalary { get; set; }

    public string? EducationalQualification { get; set; }

    public string? CurrentCompany { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public string? Institute { get; set; }

    public int? ReferenceType { get; set; }

    public string? Reference1 { get; set; }

    public string? Reference2 { get; set; }

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

    public string? Result { get; set; }

    public int? ApplicantStatus { get; set; }

    public int? IsActive { get; set; }

    public int? ApplicantSourceId { get; set; }

    public int? IsUploadedFromBulk { get; set; }

    public string? Village { get; set; }

    public string? Po { get; set; }

    public string? Thana { get; set; }

    public string? District { get; set; }

    public string? PresentCompanyExperienceYear { get; set; }

    public string? ApplicantParmanentAddress { get; set; }

    public int? IsOfferLetterAccepted { get; set; }

    public int? IsJoiningLetterAccepted { get; set; }

    public int? IsAppointmentLetterAccepted { get; set; }

    public int? IsPcfprovided { get; set; }

    public DateTime? PcfprovidedDate { get; set; }

    public int? PcfprovidedBy { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? JobId { get; set; }

    public int? RequisitionReferenceId { get; set; }

    public int? JobVacancyId { get; set; }

    public string? ApplicantCode { get; set; }

    public int? StateId { get; set; }

    public int? HomeDistrictId { get; set; }

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

    public int? InsertBy { get; set; }

    public DateTime? InsertDate { get; set; }

    public DateTime? AppliedDate { get; set; }

    public int? OfferLetterReferenceNo { get; set; }

    public int? IsTemporary { get; set; }

    public int? AppoinmentLetterRefNumber { get; set; }

    public string? OfferLetterPath { get; set; }

    public string? AppoinmentLetterPath { get; set; }

    public string? BloodGroup { get; set; }

    public int? OfferLetterSentBy { get; set; }

    public DateTime? OfferLetterSentDate { get; set; }

    public DateTime? OfferDate { get; set; }

    public int? Probhition { get; set; }

    public string? CircularEmailPath { get; set; }

    public int? IsTextedForInterviewAfterWritten { get; set; }

    public int? CallForSecondInterviewBy { get; set; }

    public DateTime? CallForSecondInterviewDate { get; set; }

    public int? CallForThirdInterviewBy { get; set; }

    public DateTime? CallForThirdInterviewDate { get; set; }

    public int? HascriminalOffence { get; set; }

    public string? SecondRelativePosition { get; set; }

    public string? SecondRelativeOrganization { get; set; }

    public string? NameOfSecondRelative { get; set; }

    public string? SecondRelationshipName { get; set; }

    public string? CriminalOffenceDescription { get; set; }

    public string? PortalApplicantId { get; set; }

    public string? TrackingNumber { get; set; }

    public int? IsAbsentFormInterview { get; set; }

    public int? IsChecked { get; set; }

    public int? ExamRollNumber { get; set; }

    public decimal? EducationMarks { get; set; }

    public int? ReligionId { get; set; }

    public string? Nationality { get; set; }

    public int? RecommendedByBuHead { get; set; }

    public DateTime? RecommendedDateBuHead { get; set; }

    public int? RecommendedByFunctionalHead { get; set; }

    public DateTime? RecommendedDateFunctionalHead { get; set; }

    public int? RecommendedByHrHead { get; set; }

    public DateTime? RecommendedDateHrHead { get; set; }

    public int? ApprovedByCeo { get; set; }

    public DateTime? ApprovedDateByCeo { get; set; }

    public int? ApprovedByMd { get; set; }

    public DateTime? ApprovedDateByMd { get; set; }

    public string? InterviewRemarks { get; set; }

    public int? IsInitialSelected { get; set; }

    public int? IsInitialRejected { get; set; }

    public int? IsInitialPipelined { get; set; }

    public string? OfferLetterPathBangla { get; set; }

    public string? JoiningLetterPath { get; set; }

    public DateTime? JoiningLetterGeneratedDate { get; set; }

    public int? JoiningLetterGeneratedBy { get; set; }

    public DateTime? OfferExpireDate { get; set; }

    public string? PreviousRemarks { get; set; }

    public int? IsPipelined { get; set; }

    public int? IsSelect { get; set; }

    public int? IsRejected { get; set; }

    public string? ApplicantSignature { get; set; }

    public int? IsVerified { get; set; }

    public string? InterviewVanue { get; set; }

    public string? ApplicantSocialMedia { get; set; }

    public string? SelectionRemarks { get; set; }

    public string? SecondInterivewVanue { get; set; }
}
