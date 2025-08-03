namespace bdDevCRM.RepositoryDtos.CRM;


internal class GetApplicationRepositoryDto
{
  // ================================
  // CrmApplicationDto
  // ================================
  public int ApplicationId { get; set; }
  public DateTime ApplicationDate { get; set; }
  public string ApplicationStatus { get; set; } = null!;
  public DateTime AppCreatedDate { get; set; }
  public int AppCreatedBy { get; set; }
  public DateTime? AppUpdatedDate { get; set; }
  public int? AppUpdatedBy { get; set; }

  // ================================
  // ApplicantCourseDto
  // ================================
  public int ApplicantCourseId { get; set; }
  public int CountryId { get; set; }
  public string? CountryName { get; set; }
  public int InstituteId { get; set; }
  public string? InstituteName { get; set; }
  public string? CourseTitle { get; set; }
  public int IntakeMonthId { get; set; }
  public string? IntakeMonth { get; set; }
  public int IntakeYearId { get; set; }
  public string? IntakeYear { get; set; }
  public string? ApplicationFee { get; set; }
  public int CurrencyId { get; set; }
  public string? CurrencyName { get; set; }
  public int PaymentMethodId { get; set; }
  public string? PaymentMethod { get; set; }
  public string? PaymentReferenceNumber { get; set; }
  public DateTime? PaymentDate { get; set; }
  public string? CourseRemarks { get; set; }
  public DateTime CourseCreatedDate { get; set; }
  public int CourseCreatedBy { get; set; }
  public DateTime? CourseUpdatedDate { get; set; }
  public int? CourseUpdatedBy { get; set; }
  public int? CourseId { get; set; }

  // ================================
  // ApplicantInfoDto
  // ================================
  public int ApplicantId { get; set; }
  public int GenderId { get; set; }
  public string? GenderName { get; set; }
  public string? TitleValue { get; set; }
  public string? TitleText { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string ApplicantName { get; set; } = null!;
  public DateTime? DateOfBirth { get; set; }
  public int MaritalStatusId { get; set; }
  public string? MaritalStatusName { get; set; }
  public string? Nationality { get; set; }
  public string? HasValidPassport { get; set; }
  public string? PassportNumber { get; set; }
  public DateTime? PassportIssueDate { get; set; }
  public DateTime? PassportExpiryDate { get; set; }
  public string? PhoneCountryCode { get; set; }
  public string? PhoneAreaCode { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Mobile { get; set; }
  public string? EmailAddress { get; set; }
  public string? SkypeId { get; set; }
  public string? ApplicantImagePath { get; set; }
  public DateTime ApplicantCreatedDate { get; set; }
  public int ApplicantCreatedBy { get; set; }
  public DateTime? ApplicantUpdatedDate { get; set; }
  public int? ApplicantUpdatedBy { get; set; }

  // ================================
  // PermanentAddressDto
  // ================================
  public int PermanentAddressId { get; set; }
  public string? PermanentAddress { get; set; }
  public string? PermanentCity { get; set; }
  public string? PermanentState { get; set; }
  public int PermanentCountryId { get; set; }
  public string? PermanentCountryName { get; set; }
  public string? PermanentPostalCode { get; set; }
  public DateTime PermanentCreatedDate { get; set; }
  public int PermanentCreatedBy { get; set; }
  public DateTime? PermanentUpdatedDate { get; set; }
  public int? PermanentUpdatedBy { get; set; }

  // ================================
  // PresentAddressDto
  // ================================
  public int PresentAddressId { get; set; }
  public bool SameAsPermanentAddress { get; set; }
  public string? PresentAddress { get; set; }
  public string? PresentCity { get; set; }
  public string? PresentState { get; set; }
  public int PresentCountryId { get; set; }
  public string? PresentCountryName { get; set; }
  public string? PresentPostalCode { get; set; }
  public DateTime PresentCreatedDate { get; set; }
  public int PresentCreatedBy { get; set; }
  public DateTime? PresentUpdatedDate { get; set; }
  public int? PresentUpdatedBy { get; set; }

  // ================================
  // EducationHistoryDto
  // ================================
  public int EducationHistoryId { get; set; }
  public int Education_ApplicantId { get; set; }
  public string? EducationInstitution { get; set; }
  public string? Qualification { get; set; }
  public int? PassingYear { get; set; }
  public string? Grade { get; set; }
  public string? EducationDocumentName { get; set; }
  public string? AttachedDocument { get; set; }
  public string? PdfThumbnail { get; set; }
  public DateTime EducationCreatedDate { get; set; }
  public int EducationCreatedBy { get; set; }
  public DateTime? EducationUpdatedDate { get; set; }
  public int? EducationUpdatedBy { get; set; }

  // ================================
  // IELTSInformationDto
  // ================================
  public int IELTSInformationId { get; set; }
  public int IELTS_ApplicantId { get; set; }
  public string? IELTSListening { get; set; }
  public string? IELTSReading { get; set; }
  public string? IELTSWriting { get; set; }
  public string? IELTSSpeaking { get; set; }
  public string? IELTSOverallScore { get; set; }
  public DateTime? IELTSDate { get; set; }
  public string? IELTSScannedCopyPath { get; set; }
  public string? IELTSAdditionalInformation { get; set; }
  public DateTime IELTS_CreatedDate { get; set; }
  public int IELTS_CreatedBy { get; set; }
  public DateTime? IELTS_UpdatedDate { get; set; }
  public int? IELTS_UpdatedBy { get; set; }

  // ================================
  // TOEFLInformationDto
  // ================================
  public int TOEFLInformationId { get; set; }
  public int TOEFL_ApplicantId { get; set; }
  public string? TOEFLListening { get; set; }
  public string? TOEFLReading { get; set; }
  public string? TOEFLWriting { get; set; }
  public string? TOEFLSpeaking { get; set; }
  public string? TOEFLOverallScore { get; set; }
  public DateTime? TOEFLDate { get; set; }
  public string? TOEFLScannedCopyPath { get; set; }
  public string? TOEFLAdditionalInformation { get; set; }
  public DateTime TOEFL_CreatedDate { get; set; }
  public int TOEFL_CreatedBy { get; set; }
  public DateTime? TOEFL_UpdatedDate { get; set; }
  public int? TOEFL_UpdatedBy { get; set; }

  // ================================
  // PTEInformationDto
  // ================================
  public int PTEInformationId { get; set; }
  public int PTE_ApplicantId { get; set; }
  public string? PTEListening { get; set; }
  public string? PTEReading { get; set; }
  public string? PTEWriting { get; set; }
  public string? PTESpeaking { get; set; }
  public string? PTEOverallScore { get; set; }
  public DateTime? PTEDate { get; set; }
  public string? PTEScannedCopyPath { get; set; }
  public string? PTEAdditionalInformation { get; set; }
  public DateTime PTE_CreatedDate { get; set; }
  public int PTE_CreatedBy { get; set; }
  public DateTime? PTE_UpdatedDate { get; set; }
  public int? PTE_UpdatedBy { get; set; }

  // ================================
  // GMATInformationDto
  // ================================
  public int GMATInformationId { get; set; }
  public int GMAT_ApplicantId { get; set; }
  public string? GMATListening { get; set; }
  public string? GMATReading { get; set; }
  public string? GMATWriting { get; set; }
  public string? GMATSpeaking { get; set; }
  public string? GMATOverallScore { get; set; }
  public DateTime? GMATDate { get; set; }
  public string? GMATScannedCopyPath { get; set; }
  public string? GMATAdditionalInformation { get; set; }
  public DateTime GMAT_CreatedDate { get; set; }
  public int GMAT_CreatedBy { get; set; }
  public DateTime? GMAT_UpdatedDate { get; set; }
  public int? GMAT_UpdatedBy { get; set; }

  // ================================
  // OTHERSInformationDto
  // ================================
  public int OTHERSInformationId { get; set; }
  public int OTHERS_ApplicantId { get; set; }
  public string? OTHERSAdditionalInformation { get; set; }
  public string? OTHERSScannedCopyPath { get; set; }
  public DateTime OTHERS_CreatedDate { get; set; }
  public int OTHERS_CreatedBy { get; set; }
  public DateTime? OTHERS_UpdatedDate { get; set; }
  public int? OTHERS_UpdatedBy { get; set; }

  // ================================
  // WorkExperienceHistoryDto
  // ================================
  public int WorkExperienceId { get; set; }
  public int Work_ApplicantId { get; set; }
  public string? NameOfEmployer { get; set; }
  public string? Position { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }
  public string? Period { get; set; }
  public string? MainResponsibility { get; set; }
  public string? WorkDocumentName { get; set; }
  public string? FileThumbnail { get; set; }
  public DateTime WorkCreatedDate { get; set; }
  public int WorkCreatedBy { get; set; }
  public DateTime? WorkUpdatedDate { get; set; }
  public int? WorkUpdatedBy { get; set; }

  // ================================
  // ApplicantReferenceDto
  // ================================
  public int ApplicantReferenceId { get; set; }
  public int Ref_ApplicantId { get; set; }
  public string ReferenceName { get; set; } = null!;
  public string? Designation { get; set; }
  public string? Institution { get; set; }
  public string? EmailID { get; set; }
  public string? PhoneNo { get; set; }
  public string? FaxNo { get; set; }
  public string? RefAddress { get; set; }
  public string? RefCity { get; set; }
  public string? RefState { get; set; }
  public string? RefCountry { get; set; }
  public string? PostOrZipCode { get; set; }
  public DateTime RefCreatedDate { get; set; }
  public int RefCreatedBy { get; set; }
  public DateTime? RefUpdatedDate { get; set; }
  public int? RefUpdatedBy { get; set; }

  // ================================
  // StatementOfPurposeDto
  // ================================
  public int StatementOfPurposeId { get; set; }
  public int SOP_ApplicantId { get; set; }
  public string? StatementOfPurposeRemarks { get; set; }
  public string? StatementOfPurposeFilePath { get; set; }
  public DateTime SOP_CreatedDate { get; set; }
  public int SOP_CreatedBy { get; set; }
  public DateTime? SOP_UpdatedDate { get; set; }
  public int? SOP_UpdatedBy { get; set; }

  // ================================
  // AdditionalInfoDto
  // ================================
  public int AdditionalInfoId { get; set; }
  public int AddInfo_ApplicantId { get; set; }
  public string? RequireAccommodation { get; set; }
  public string? HealthNMedicalNeeds { get; set; }
  public string? HealthNMedicalNeedsRemarks { get; set; }
  public string? AdditionalInformationRemarks { get; set; }
  public string? AddInfoDocTitle { get; set; }
  public string? AddInfoUploadFile { get; set; }
  public string? AddInfoDocumentName { get; set; }
  public string? AddInfoFileThumbnail { get; set; }
  public string? AddInfoRecordType { get; set; }
  public DateTime AddInfoCreatedDate { get; set; }
  public int AddInfoCreatedBy { get; set; }
  public DateTime? AddInfoUpdatedDate { get; set; }
  public int? AddInfoUpdatedBy { get; set; }
}

