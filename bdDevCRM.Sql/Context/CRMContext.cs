using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.Entities.CRMM;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Sql.Context;

public partial class CRMContext : DbContext
{
  public CRMContext()
  {
  }

  public CRMContext(DbContextOptions<CRMContext> options)
      : base(options)
  {
  }

  // new added
  public virtual DbSet<TokenBlacklist> TokenBlacklist { get; set; }
  public virtual DbSet<CrmapplicantCourseDetials> CrmapplicantCourseDetials { get; set; }

  public virtual DbSet<CRMInstituteType> InstituteType { get; set; }
  public virtual DbSet<Crmcourse> Crmcourse { get; set; }

  public virtual DbSet<CrmcourseIntake> CrmcourseIntake { get; set; }

  public virtual DbSet<Crminstitute> Crminstitute { get; set; }

  public virtual DbSet<Crmmonth> Crmmonth { get; set; }

  public virtual DbSet<Crmyear> Crmyear { get; set; }

  public virtual DbSet<AboutUsLicense> AboutUsLicense { get; set; }

  public virtual DbSet<AbsentSmsRemarks> AbsentSmsRemarks { get; set; }

  public virtual DbSet<AccessRestriction> AccessRestriction { get; set; }

  public virtual DbSet<AccessRestriction05032023> AccessRestriction05032023 { get; set; }

  public virtual DbSet<Accesscontrol> Accesscontrol { get; set; }

  public virtual DbSet<AccountHead> AccountHead { get; set; }

  public virtual DbSet<AccountHeadSubLedger> AccountHeadSubLedger { get; set; }

  public virtual DbSet<Accumulationtype> Accumulationtype { get; set; }

  public virtual DbSet<AcrDetails> AcrDetails { get; set; }

  public virtual DbSet<AcrSalaryFixation> AcrSalaryFixation { get; set; }

  public virtual DbSet<AcrSupAssessMarksDetails> AcrSupAssessMarksDetails { get; set; }

  public virtual DbSet<AcrSupRemarks> AcrSupRemarks { get; set; }

  public virtual DbSet<AcrSupervisorAssessment> AcrSupervisorAssessment { get; set; }

  public virtual DbSet<AdditionalCtc> AdditionalCtc { get; set; }

  public virtual DbSet<AdditionalCtcUploadForSalary> AdditionalCtcUploadForSalary { get; set; }

  public virtual DbSet<AdditionalCtcUploadForSalaryTemp> AdditionalCtcUploadForSalaryTemp { get; set; }

  public virtual DbSet<AdvanceIncomeTax> AdvanceIncomeTax { get; set; }

  public virtual DbSet<AitFileAttachment> AitFileAttachment { get; set; }

  public virtual DbSet<AlternativeLeave> AlternativeLeave { get; set; }

  public virtual DbSet<AlternativeLeaveMap> AlternativeLeaveMap { get; set; }

  public virtual DbSet<AmcExtension> AmcExtension { get; set; }

  public virtual DbSet<Amcdetails> Amcdetails { get; set; }

  public virtual DbSet<ApplicantDocumentPrint> ApplicantDocumentPrint { get; set; }

  public virtual DbSet<ApplicantInformation> ApplicantInformation { get; set; }

  public virtual DbSet<ApplicantInformationHistory> ApplicantInformationHistory { get; set; }

  public virtual DbSet<ApplicantOfferLetterInfo> ApplicantOfferLetterInfo { get; set; }

  public virtual DbSet<ApplicantPayroll> ApplicantPayroll { get; set; }

  public virtual DbSet<ApplicantPayrollDetails> ApplicantPayrollDetails { get; set; }

  public virtual DbSet<ApplicantSource> ApplicantSource { get; set; }

  public virtual DbSet<ApplicationAutoProcessLog> ApplicationAutoProcessLog { get; set; }

  public virtual DbSet<AppraisalBasicIncrement> AppraisalBasicIncrement { get; set; }

  public virtual DbSet<AppraisalDetails> AppraisalDetails { get; set; }

  public virtual DbSet<AppraisalDetailsForFf> AppraisalDetailsForFf { get; set; }

  public virtual DbSet<AppraisalDetailsLog> AppraisalDetailsLog { get; set; }

  public virtual DbSet<AppraisalGradeSettings> AppraisalGradeSettings { get; set; }

  public virtual DbSet<AppraisalGradeWiseAmountMaxRange> AppraisalGradeWiseAmountMaxRange { get; set; }

  public virtual DbSet<AppraisalGradeWiseWeight> AppraisalGradeWiseWeight { get; set; }

  public virtual DbSet<AppraisalImportance> AppraisalImportance { get; set; }

  public virtual DbSet<AppraisalIncrementDetails> AppraisalIncrementDetails { get; set; }

  public virtual DbSet<AppraisalMaster> AppraisalMaster { get; set; }

  public virtual DbSet<AppraisalMasterLog> AppraisalMasterLog { get; set; }

  public virtual DbSet<AppraisalOpeningMonthWise> AppraisalOpeningMonthWise { get; set; }

  public virtual DbSet<AppraisalRating> AppraisalRating { get; set; }

  public virtual DbSet<AppraisalRattingRange> AppraisalRattingRange { get; set; }

  public virtual DbSet<AppraisalRattingSlab> AppraisalRattingSlab { get; set; }

  public virtual DbSet<AppraisalRecommendation> AppraisalRecommendation { get; set; }

  public virtual DbSet<AppraisalTempData> AppraisalTempData { get; set; }

  public virtual DbSet<AppraisalTempUploadTable> AppraisalTempUploadTable { get; set; }

  public virtual DbSet<AppraisalYearEndProcess> AppraisalYearEndProcess { get; set; }

  public virtual DbSet<ApprisalImplementPolicy> ApprisalImplementPolicy { get; set; }

  public virtual DbSet<ApproverDetails> ApproverDetails { get; set; }

  public virtual DbSet<ApproverDetails14012024> ApproverDetails14012024 { get; set; }

  public virtual DbSet<ApproverDetails27122022> ApproverDetails27122022 { get; set; }

  public virtual DbSet<ApproverDetailsRec03012024> ApproverDetailsRec03012024 { get; set; }

  public virtual DbSet<ApproverHistory> ApproverHistory { get; set; }

  public virtual DbSet<ApproverOrder> ApproverOrder { get; set; }

  public virtual DbSet<ApproverType> ApproverType { get; set; }

  public virtual DbSet<ApproverTypeToGroupMapping> ApproverTypeToGroupMapping { get; set; }

  public virtual DbSet<AppsTokenInfo> AppsTokenInfo { get; set; }

  public virtual DbSet<AppsTransactionLog> AppsTransactionLog { get; set; }

  public virtual DbSet<AssemblyInfo> AssemblyInfo { get; set; }

  public virtual DbSet<Assessmenttype> Assessmenttype { get; set; }

  public virtual DbSet<Asset> Asset { get; set; }

  public virtual DbSet<AssetAmortization> AssetAmortization { get; set; }

  public virtual DbSet<AssetCategory> AssetCategory { get; set; }

  public virtual DbSet<AssetIdentification> AssetIdentification { get; set; }

  public virtual DbSet<AssetLocation> AssetLocation { get; set; }

  public virtual DbSet<AssetProcurement> AssetProcurement { get; set; }

  public virtual DbSet<AssetReturnDetails> AssetReturnDetails { get; set; }

  public virtual DbSet<AssetStatus> AssetStatus { get; set; }

  public virtual DbSet<AssetValuation> AssetValuation { get; set; }

  public virtual DbSet<AssignApprover> AssignApprover { get; set; }

  public virtual DbSet<AssignApprover06022024> AssignApprover06022024 { get; set; }

  public virtual DbSet<AssignApprover060724> AssignApprover060724 { get; set; }

  public virtual DbSet<AssignApprover23092023> AssignApprover23092023 { get; set; }

  public virtual DbSet<AssignApproverRec03012024> AssignApproverRec03012024 { get; set; }

  public virtual DbSet<AssignExceptionEmployee> AssignExceptionEmployee { get; set; }

  public virtual DbSet<AttachFileForPo> AttachFileForPo { get; set; }

  public virtual DbSet<AttendanceAdjustment> AttendanceAdjustment { get; set; }

  public virtual DbSet<AttendanceFlag> AttendanceFlag { get; set; }

  public virtual DbSet<AttendanceLog> AttendanceLog { get; set; }

  public virtual DbSet<AttendanceLog14092023> AttendanceLog14092023 { get; set; }

  public virtual DbSet<AttendanceLog21800221> AttendanceLog21800221 { get; set; }

  public virtual DbSet<AttendanceLogArchive2017> AttendanceLogArchive2017 { get; set; }

  public virtual DbSet<AttendanceLogArchive2018> AttendanceLogArchive2018 { get; set; }

  public virtual DbSet<AttendanceLogArchive2019> AttendanceLogArchive2019 { get; set; }

  public virtual DbSet<AttendanceLogArchive2020> AttendanceLogArchive2020 { get; set; }

  public virtual DbSet<AttendanceLogArchive2021> AttendanceLogArchive2021 { get; set; }

  public virtual DbSet<AttendanceLogArchive2022> AttendanceLogArchive2022 { get; set; }

  public virtual DbSet<AttendanceLogArchive2023> AttendanceLogArchive2023 { get; set; }

  public virtual DbSet<AttendanceLogMigration> AttendanceLogMigration { get; set; }

  public virtual DbSet<AttendanceLogReprocess> AttendanceLogReprocess { get; set; }

  public virtual DbSet<AttendanceMonthEnd> AttendanceMonthEnd { get; set; }

  public virtual DbSet<AttendanceMonthEndApprovalNotification> AttendanceMonthEndApprovalNotification { get; set; }

  public virtual DbSet<AttendanceMonthEndApprovalNotification20231105> AttendanceMonthEndApprovalNotification20231105 { get; set; }

  public virtual DbSet<AttendanceMonthEndArchive2020> AttendanceMonthEndArchive2020 { get; set; }

  public virtual DbSet<AttendanceMonthEndArchive2021> AttendanceMonthEndArchive2021 { get; set; }

  public virtual DbSet<AttendanceMonthEndArchive2022> AttendanceMonthEndArchive2022 { get; set; }

  public virtual DbSet<AttendanceMonthEndArchive2023> AttendanceMonthEndArchive2023 { get; set; }

  public virtual DbSet<AttendanceMonthEndHeldUpWithdrawn> AttendanceMonthEndHeldUpWithdrawn { get; set; }

  public virtual DbSet<AttendanceStatus> AttendanceStatus { get; set; }

  public virtual DbSet<Attendancelogtemp> Attendancelogtemp { get; set; }

  public virtual DbSet<AttendenceCode> AttendenceCode { get; set; }

  public virtual DbSet<AttendenceRemarksSettings> AttendenceRemarksSettings { get; set; }

  public virtual DbSet<AttributeDepartmentMapping> AttributeDepartmentMapping { get; set; }

  public virtual DbSet<AuditLog> AuditLog { get; set; }

  public virtual DbSet<AuditTrail> AuditTrail { get; set; }

  public virtual DbSet<AuditType> AuditType { get; set; }

  public virtual DbSet<BalanceSheet> BalanceSheet { get; set; }

  public virtual DbSet<Bank> Bank { get; set; }

  public virtual DbSet<BankAccountType> BankAccountType { get; set; }

  public virtual DbSet<BankBranch> BankBranch { get; set; }

  public virtual DbSet<BdgBasicIncrement> BdgBasicIncrement { get; set; }

  public virtual DbSet<BdgBudgetGradeSettings> BdgBudgetGradeSettings { get; set; }

  public virtual DbSet<BdgBudgetYearCompanyMap> BdgBudgetYearCompanyMap { get; set; }

  public virtual DbSet<BdgBudgetYearConfig> BdgBudgetYearConfig { get; set; }

  public virtual DbSet<BdgEmpEvalution> BdgEmpEvalution { get; set; }

  public virtual DbSet<BdgEmpEvalutionAppprover> BdgEmpEvalutionAppprover { get; set; }

  public virtual DbSet<BdgEmpEvalutionAppproverDraft> BdgEmpEvalutionAppproverDraft { get; set; }

  public virtual DbSet<BdgEmpEvalutionAppproverHistory> BdgEmpEvalutionAppproverHistory { get; set; }

  public virtual DbSet<BdgEmpEvalutionDetails> BdgEmpEvalutionDetails { get; set; }

  public virtual DbSet<BdgEmpEvalutionDetailsLog> BdgEmpEvalutionDetailsLog { get; set; }

  public virtual DbSet<BdgEmpEvalutionFinal> BdgEmpEvalutionFinal { get; set; }

  public virtual DbSet<BdgEmpEvalutionFinalDraft> BdgEmpEvalutionFinalDraft { get; set; }

  public virtual DbSet<BdgEmpEvalutionHalfYearly> BdgEmpEvalutionHalfYearly { get; set; }

  public virtual DbSet<BdgEmpEvalutionHalfYearlyDetails> BdgEmpEvalutionHalfYearlyDetails { get; set; }

  public virtual DbSet<BdgEmpEvalutionHalfYearlyPayroll> BdgEmpEvalutionHalfYearlyPayroll { get; set; }

  public virtual DbSet<BdgEmpEvalutionPayroll> BdgEmpEvalutionPayroll { get; set; }

  public virtual DbSet<BdgEmpEvalutionPayrollLog> BdgEmpEvalutionPayrollLog { get; set; }

  public virtual DbSet<BdgEmpEvalutionRecommender> BdgEmpEvalutionRecommender { get; set; }

  public virtual DbSet<BdgEmpEvalutionRecommenderDraft> BdgEmpEvalutionRecommenderDraft { get; set; }

  public virtual DbSet<BdgEmpEvalutionRecommenderHistory> BdgEmpEvalutionRecommenderHistory { get; set; }

  public virtual DbSet<BdgEmpEvalutionTmp> BdgEmpEvalutionTmp { get; set; }

  public virtual DbSet<BdgEmployeeForPerformenceByYear> BdgEmployeeForPerformenceByYear { get; set; }

  public virtual DbSet<BdgEvaluationHistory> BdgEvaluationHistory { get; set; }

  public virtual DbSet<BdgFieldForceYearlyData> BdgFieldForceYearlyData { get; set; }

  public virtual DbSet<BdgFieldForceYearlyDataTemp> BdgFieldForceYearlyDataTemp { get; set; }

  public virtual DbSet<BdgHalfYearlyKpiBehaviour> BdgHalfYearlyKpiBehaviour { get; set; }

  public virtual DbSet<BdgHalfYearlyKpiBehaviourGradeMap> BdgHalfYearlyKpiBehaviourGradeMap { get; set; }

  public virtual DbSet<BdgKpiConfig> BdgKpiConfig { get; set; }

  public virtual DbSet<BfbilBulkLeavePosting03052023> BfbilBulkLeavePosting03052023 { get; set; }

  public virtual DbSet<BfbilEmpAcc10042023> BfbilEmpAcc10042023 { get; set; }

  public virtual DbSet<BgCost> BgCost { get; set; }

  public virtual DbSet<BgPmsCompnayDepartmentMapping> BgPmsCompnayDepartmentMapping { get; set; }

  public virtual DbSet<BgRecCompnayDepartmentMapping> BgRecCompnayDepartmentMapping { get; set; }

  public virtual DbSet<BgRecEmployeeWiseCompetencyMapping> BgRecEmployeeWiseCompetencyMapping { get; set; }

  public virtual DbSet<BgdManpowerReqGradeMap> BgdManpowerReqGradeMap { get; set; }

  public virtual DbSet<BgdManpowerRequisition> BgdManpowerRequisition { get; set; }

  public virtual DbSet<Bihq2SecALv04092022> Bihq2SecALv04092022 { get; set; }

  public virtual DbSet<Bihq2SecALv07052022> Bihq2SecALv07052022 { get; set; }

  public virtual DbSet<BmfNonMgntHouseRent10072024> BmfNonMgntHouseRent10072024 { get; set; }

  public virtual DbSet<BmfplBulkLeavePosting03052023> BmfplBulkLeavePosting03052023 { get; set; }

  public virtual DbSet<BoardInstitute> BoardInstitute { get; set; }

  public virtual DbSet<Bonus> Bonus { get; set; }

  public virtual DbSet<BonusPolicy> BonusPolicy { get; set; }

  public virtual DbSet<BonusTaxSetup> BonusTaxSetup { get; set; }

  public virtual DbSet<BonusTypeName> BonusTypeName { get; set; }

  public virtual DbSet<Bonustemp> Bonustemp { get; set; }

  public virtual DbSet<Branch> Branch { get; set; }

  public virtual DbSet<BranchShift> BranchShift { get; set; }

  public virtual DbSet<Canteen> Canteen { get; set; }

  public virtual DbSet<CanteenBilling> CanteenBilling { get; set; }

  public virtual DbSet<CanteenBillingList> CanteenBillingList { get; set; }

  public virtual DbSet<CanteenBillingPenalty> CanteenBillingPenalty { get; set; }

  public virtual DbSet<CanteenBooking> CanteenBooking { get; set; }

  public virtual DbSet<CanteenBookingGuest> CanteenBookingGuest { get; set; }

  public virtual DbSet<CanteenBookingMonthlyMeals> CanteenBookingMonthlyMeals { get; set; }

  public virtual DbSet<CanteenDeduction> CanteenDeduction { get; set; }

  public virtual DbSet<CanteenEmployeeSubsidyMapping> CanteenEmployeeSubsidyMapping { get; set; }

  public virtual DbSet<CanteenEnrollment> CanteenEnrollment { get; set; }

  public virtual DbSet<CanteenEnrollmentWeekdayMeals> CanteenEnrollmentWeekdayMeals { get; set; }

  public virtual DbSet<CanteenGuestBillPay> CanteenGuestBillPay { get; set; }

  public virtual DbSet<CanteenLocationMapping> CanteenLocationMapping { get; set; }

  public virtual DbSet<CanteenMealCalender> CanteenMealCalender { get; set; }

  public virtual DbSet<CanteenMealCalenderWeekdayPlanning> CanteenMealCalenderWeekdayPlanning { get; set; }

  public virtual DbSet<CanteenMealType> CanteenMealType { get; set; }

  public virtual DbSet<CanteenMealTypeSubsidiary> CanteenMealTypeSubsidiary { get; set; }

  public virtual DbSet<CanteenSubsidiaryType> CanteenSubsidiaryType { get; set; }

  public virtual DbSet<CanteenSupplier> CanteenSupplier { get; set; }

  public virtual DbSet<CanteenSupplierLedger> CanteenSupplierLedger { get; set; }

  public virtual DbSet<CanteenSupplierMapping> CanteenSupplierMapping { get; set; }

  public virtual DbSet<CareerHistory> CareerHistory { get; set; }

  public virtual DbSet<CareerHistory18122022> CareerHistory18122022 { get; set; }

  public virtual DbSet<CareerHistoryTempCurrent18122022> CareerHistoryTempCurrent18122022 { get; set; }

  public virtual DbSet<CareerHistoryTmpUpload04042024> CareerHistoryTmpUpload04042024 { get; set; }

  public virtual DbSet<CareerHistoryTmpUpload13062024> CareerHistoryTmpUpload13062024 { get; set; }

  public virtual DbSet<CareerHistoryTmpUpload13062024Food> CareerHistoryTmpUpload13062024Food { get; set; }

  public virtual DbSet<Certificatetype> Certificatetype { get; set; }

  public virtual DbSet<ChallanNumberUpload> ChallanNumberUpload { get; set; }

  public virtual DbSet<ChallanNumberUploadTemp> ChallanNumberUploadTemp { get; set; }

  public virtual DbSet<ChatMessage> ChatMessage { get; set; }

  public virtual DbSet<ChatStatus> ChatStatus { get; set; }

  public virtual DbSet<ChatUsers> ChatUsers { get; set; }

  public virtual DbSet<Cheque> Cheque { get; set; }

  public virtual DbSet<ChequeLeafDetails> ChequeLeafDetails { get; set; }

  public virtual DbSet<ChequeLeafMaster> ChequeLeafMaster { get; set; }

  public virtual DbSet<ClearenceAuthority> ClearenceAuthority { get; set; }

  public virtual DbSet<ClearenceDetails> ClearenceDetails { get; set; }

  public virtual DbSet<Client> Client { get; set; }

  public virtual DbSet<ClientContact> ClientContact { get; set; }

  public virtual DbSet<CnfAgency> CnfAgency { get; set; }

  public virtual DbSet<CnfAgencyType> CnfAgencyType { get; set; }

  public virtual DbSet<CnfAgents> CnfAgents { get; set; }

  public virtual DbSet<CnfLicense> CnfLicense { get; set; }

  public virtual DbSet<CnfLicenseHistry> CnfLicenseHistry { get; set; }

  public virtual DbSet<CoffCertificate> CoffCertificate { get; set; }

  public virtual DbSet<CoffCertificate16032024> CoffCertificate16032024 { get; set; }

  public virtual DbSet<CoffCertificateArchive2017> CoffCertificateArchive2017 { get; set; }

  public virtual DbSet<CoffCertificateArchive2018> CoffCertificateArchive2018 { get; set; }

  public virtual DbSet<CoffCertificateArchive2019> CoffCertificateArchive2019 { get; set; }

  public virtual DbSet<CoffCertificateArchive2020> CoffCertificateArchive2020 { get; set; }

  public virtual DbSet<CoffCertificateArchive2021> CoffCertificateArchive2021 { get; set; }

  public virtual DbSet<CoffCertificateArchive2022> CoffCertificateArchive2022 { get; set; }

  public virtual DbSet<CoffCertificateArchive2023> CoffCertificateArchive2023 { get; set; }

  public virtual DbSet<Company> Company { get; set; }

  public virtual DbSet<CompanyBankBranch> CompanyBankBranch { get; set; }

  public virtual DbSet<CompanyDepartmentMap> CompanyDepartmentMap { get; set; }

  public virtual DbSet<CompanyDesignatiomMap> CompanyDesignatiomMap { get; set; }

  public virtual DbSet<CompanyDivisionMap> CompanyDivisionMap { get; set; }

  public virtual DbSet<CompanyFiscalYearMap> CompanyFiscalYearMap { get; set; }

  public virtual DbSet<CompanyLeave> CompanyLeave { get; set; }

  public virtual DbSet<CompanyLocationMap> CompanyLocationMap { get; set; }

  public virtual DbSet<CompanyPayroll> CompanyPayroll { get; set; }

  public virtual DbSet<CompanyPf> CompanyPf { get; set; }

  public virtual DbSet<Competencies> Competencies { get; set; }

  public virtual DbSet<CompetencyAndFunctionMap> CompetencyAndFunctionMap { get; set; }

  public virtual DbSet<CompetencyAreaDepartmentDesignationMapping> CompetencyAreaDepartmentDesignationMapping { get; set; }

  public virtual DbSet<CompetencyAreaGradeMapping> CompetencyAreaGradeMapping { get; set; }

  public virtual DbSet<CompetencyAreaSection> CompetencyAreaSection { get; set; }

  public virtual DbSet<CompetencyAreaSettings> CompetencyAreaSettings { get; set; }

  public virtual DbSet<CompetencyDepartmentDesignationMapping> CompetencyDepartmentDesignationMapping { get; set; }

  public virtual DbSet<CompetencyGradeMapping> CompetencyGradeMapping { get; set; }

  public virtual DbSet<CompetencyLevel> CompetencyLevel { get; set; }

  public virtual DbSet<CompetencyLevelAndGradeMap> CompetencyLevelAndGradeMap { get; set; }

  public virtual DbSet<CompetencySectionLevel> CompetencySectionLevel { get; set; }

  public virtual DbSet<CompitencyArea> CompitencyArea { get; set; }

  public virtual DbSet<ConfirmEmployeePayLab> ConfirmEmployeePayLab { get; set; }

  public virtual DbSet<ConfirmationHeldup> ConfirmationHeldup { get; set; }

  public virtual DbSet<ConfirmationPolicy> ConfirmationPolicy { get; set; }

  public virtual DbSet<ConfirmationPolicyDetails> ConfirmationPolicyDetails { get; set; }

  public virtual DbSet<ContactCategoryType> ContactCategoryType { get; set; }

  public virtual DbSet<ContactCompany> ContactCompany { get; set; }

  public virtual DbSet<ContactDetails> ContactDetails { get; set; }

  public virtual DbSet<ContractRenewTemp> ContractRenewTemp { get; set; }

  public virtual DbSet<ConveyancePayment> ConveyancePayment { get; set; }

  public virtual DbSet<ConveyancePaymentDetails> ConveyancePaymentDetails { get; set; }

  public virtual DbSet<CopyToDetails> CopyToDetails { get; set; }

  public virtual DbSet<CostCenterAccountHeadMapping> CostCenterAccountHeadMapping { get; set; }

  public virtual DbSet<CostCenterToDeptAndParentCostCenterMapping> CostCenterToDeptAndParentCostCenterMapping { get; set; }

  public virtual DbSet<CostCentre> CostCentre { get; set; }

  public virtual DbSet<CostCentreBankBranch> CostCentreBankBranch { get; set; }

  public virtual DbSet<Country> Country { get; set; }

  public virtual DbSet<CplAltLeaveExpireDaysMap> CplAltLeaveExpireDaysMap { get; set; }

  public virtual DbSet<CplAltLeaveExpireDaysMap17122023> CplAltLeaveExpireDaysMap17122023 { get; set; }

  public virtual DbSet<CplAltLeaveExpireDaysMap24082022> CplAltLeaveExpireDaysMap24082022 { get; set; }

  public virtual DbSet<CplAltLeaveExpireDaysMap29012024> CplAltLeaveExpireDaysMap29012024 { get; set; }

  public virtual DbSet<CplopeningBalance> CplopeningBalance { get; set; }

  public virtual DbSet<CtcAccountHeadMapping> CtcAccountHeadMapping { get; set; }

  public virtual DbSet<CtcCategory> CtcCategory { get; set; }

  public virtual DbSet<CtcPolicy> CtcPolicy { get; set; }

  public virtual DbSet<CtcPolicyHistory> CtcPolicyHistory { get; set; }

  public virtual DbSet<CtcSlabData> CtcSlabData { get; set; }

  public virtual DbSet<CtcTypes> CtcTypes { get; set; }

  public virtual DbSet<CurencyRate> CurencyRate { get; set; }

  public virtual DbSet<CurrencyInfo> CurrencyInfo { get; set; }

  public virtual DbSet<CurriculumActivities> CurriculumActivities { get; set; }

  public virtual DbSet<CvsortingCommiteeDetails> CvsortingCommiteeDetails { get; set; }

  public virtual DbSet<CvsortingReport> CvsortingReport { get; set; }

  public virtual DbSet<DailySalesAndCollection> DailySalesAndCollection { get; set; }

  public virtual DbSet<DasboardLayoutColumnSettings> DasboardLayoutColumnSettings { get; set; }

  public virtual DbSet<DashboardLayout> DashboardLayout { get; set; }

  public virtual DbSet<DayOff> DayOff { get; set; }

  public virtual DbSet<DayOffInformation> DayOffInformation { get; set; }

  public virtual DbSet<DegreeType> DegreeType { get; set; }

  public virtual DbSet<DeligationInfo> DeligationInfo { get; set; }

  public virtual DbSet<DeligationInfo18012024> DeligationInfo18012024 { get; set; }

  public virtual DbSet<Department> Department { get; set; }

  public virtual DbSet<DepartmentFacilityMap> DepartmentFacilityMap { get; set; }

  public virtual DbSet<DepartmentSectionMap> DepartmentSectionMap { get; set; }

  public virtual DbSet<Designation> Designation { get; set; }

  public virtual DbSet<DesignationType> DesignationType { get; set; }

  public virtual DbSet<DeviceRawTimeRecord> DeviceRawTimeRecord { get; set; }

  public virtual DbSet<DeviceRawTimeRecord14092023> DeviceRawTimeRecord14092023 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord20092022Bcdl> DeviceRawTimeRecord20092022Bcdl { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2017> DeviceRawTimeRecord2017 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2018> DeviceRawTimeRecord2018 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2019> DeviceRawTimeRecord2019 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2020> DeviceRawTimeRecord2020 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2021> DeviceRawTimeRecord2021 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2022> DeviceRawTimeRecord2022 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2022FromJuneToSep2022> DeviceRawTimeRecord2022FromJuneToSep2022 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2022UptoJune> DeviceRawTimeRecord2022UptoJune { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2023> DeviceRawTimeRecord2023 { get; set; }

  public virtual DbSet<DeviceRawTimeRecord2023UpToJun> DeviceRawTimeRecord2023UpToJun { get; set; }

  public virtual DbSet<DeviceRawTimeRecord21800221> DeviceRawTimeRecord21800221 { get; set; }

  public virtual DbSet<DeviceSetup> DeviceSetup { get; set; }

  public virtual DbSet<DeviceSetup11102023> DeviceSetup11102023 { get; set; }

  public virtual DbSet<DeviceSetup1804ForDelete> DeviceSetup1804ForDelete { get; set; }

  public virtual DbSet<DeviceSetup25052024> DeviceSetup25052024 { get; set; }

  public virtual DbSet<DisciplinaryAction> DisciplinaryAction { get; set; }

  public virtual DbSet<Discipline> Discipline { get; set; }

  public virtual DbSet<Diseasesinformation> Diseasesinformation { get; set; }

  public virtual DbSet<District> District { get; set; }

  public virtual DbSet<Division> Division { get; set; }

  public virtual DbSet<DivisionDepartmentMap> DivisionDepartmentMap { get; set; }

  public virtual DbSet<Docmdetails> Docmdetails { get; set; }

  public virtual DbSet<Docmdetailshistory> Docmdetailshistory { get; set; }

  public virtual DbSet<Documanttype> Documanttype { get; set; }

  public virtual DbSet<Document> Document { get; set; }

  public virtual DbSet<DocumentParameter> DocumentParameter { get; set; }

  public virtual DbSet<DocumentParameterMapping> DocumentParameterMapping { get; set; }

  public virtual DbSet<DocumentQueryMapping> DocumentQueryMapping { get; set; }

  public virtual DbSet<DocumentTemplate> DocumentTemplate { get; set; }

  public virtual DbSet<DottedLineCompanyMapping> DottedLineCompanyMapping { get; set; }

  public virtual DbSet<DottedLineEmailConfig> DottedLineEmailConfig { get; set; }

  public virtual DbSet<DottedLineEmailConfig22012022> DottedLineEmailConfig22012022 { get; set; }

  public virtual DbSet<DottedLineEmailConfigHistory> DottedLineEmailConfigHistory { get; set; }

  public virtual DbSet<DottedLineGradeMapping> DottedLineGradeMapping { get; set; }

  public virtual DbSet<DottedLineLocationMapping> DottedLineLocationMapping { get; set; }

  public virtual DbSet<EarlyExitOutPunchMissApprovedLogForEl> EarlyExitOutPunchMissApprovedLogForEl { get; set; }

  public virtual DbSet<EarlyExitOutPunchMissDailyNotifyLog> EarlyExitOutPunchMissDailyNotifyLog { get; set; }

  public virtual DbSet<EarlyExitOutPunchMissThreeTimesNotifyLog> EarlyExitOutPunchMissThreeTimesNotifyLog { get; set; }

  public virtual DbSet<EarnedLeaveAdjustmentLog> EarnedLeaveAdjustmentLog { get; set; }

  public virtual DbSet<Education> Education { get; set; }

  public virtual DbSet<EducationDynamic> EducationDynamic { get; set; }

  public virtual DbSet<EducationTemp> EducationTemp { get; set; }

  public virtual DbSet<Eligibility> Eligibility { get; set; }

  public virtual DbSet<EmailConfigLocationMapping> EmailConfigLocationMapping { get; set; }

  public virtual DbSet<EmailContent> EmailContent { get; set; }

  public virtual DbSet<EmailContent03062024> EmailContent03062024 { get; set; }

  public virtual DbSet<EmailContentSourovRec> EmailContentSourovRec { get; set; }

  public virtual DbSet<EmailContentTraining> EmailContentTraining { get; set; }

  public virtual DbSet<EmailNotificationConfig> EmailNotificationConfig { get; set; }

  public virtual DbSet<EmailNotificationLog> EmailNotificationLog { get; set; }

  public virtual DbSet<EmbassyInfo> EmbassyInfo { get; set; }

  public virtual DbSet<EmpBankInfo> EmpBankInfo { get; set; }

  public virtual DbSet<EmpFinalSettlementInfo> EmpFinalSettlementInfo { get; set; }

  public virtual DbSet<EmpGuranteersInfo> EmpGuranteersInfo { get; set; }

  public virtual DbSet<EmpManualAttendance> EmpManualAttendance { get; set; }

  public virtual DbSet<EmpManualAttendanceData> EmpManualAttendanceData { get; set; }

  public virtual DbSet<Employee> Employee { get; set; }

  public virtual DbSet<Employee03082022> Employee03082022 { get; set; }

  public virtual DbSet<EmployeeAbsentSmsRemarks> EmployeeAbsentSmsRemarks { get; set; }

  public virtual DbSet<EmployeeAsset> EmployeeAsset { get; set; }

  public virtual DbSet<EmployeeAssetTemp> EmployeeAssetTemp { get; set; }

  public virtual DbSet<EmployeeBanglaInformation> EmployeeBanglaInformation { get; set; }

  public virtual DbSet<EmployeeBank> EmployeeBank { get; set; }

  public virtual DbSet<EmployeeBankAccountUploadTemp> EmployeeBankAccountUploadTemp { get; set; }

  public virtual DbSet<EmployeeBankBranchMap> EmployeeBankBranchMap { get; set; }

  public virtual DbSet<EmployeeBusLateAdjustmentInfo> EmployeeBusLateAdjustmentInfo { get; set; }

  public virtual DbSet<EmployeeContact> EmployeeContact { get; set; }

  public virtual DbSet<EmployeeContactTemp> EmployeeContactTemp { get; set; }

  public virtual DbSet<EmployeeContract> EmployeeContract { get; set; }

  public virtual DbSet<EmployeeCostCenterTemp> EmployeeCostCenterTemp { get; set; }

  public virtual DbSet<EmployeeCostCentre> EmployeeCostCentre { get; set; }

  public virtual DbSet<EmployeeEarlyCpfinterest> EmployeeEarlyCpfinterest { get; set; }

  public virtual DbSet<EmployeeHistory> EmployeeHistory { get; set; }

  public virtual DbSet<EmployeeIndex> EmployeeIndex { get; set; }

  public virtual DbSet<EmployeeLateInfo> EmployeeLateInfo { get; set; }

  public virtual DbSet<EmployeeLeave> EmployeeLeave { get; set; }

  public virtual DbSet<EmployeeLeave1> EmployeeLeave1 { get; set; }

  public virtual DbSet<EmployeeLevel> EmployeeLevel { get; set; }

  public virtual DbSet<EmployeeMembership> EmployeeMembership { get; set; }

  public virtual DbSet<EmployeeMembershipTemp> EmployeeMembershipTemp { get; set; }

  public virtual DbSet<EmployeePayroll> EmployeePayroll { get; set; }

  public virtual DbSet<EmployeePayroll08Jun2024> EmployeePayroll08Jun2024 { get; set; }

  public virtual DbSet<EmployeePayroll09112023> EmployeePayroll09112023 { get; set; }

  public virtual DbSet<EmployeePayroll11072023> EmployeePayroll11072023 { get; set; }

  public virtual DbSet<EmployeePayroll28072022> EmployeePayroll28072022 { get; set; }

  public virtual DbSet<EmployeePayrollDetails> EmployeePayrollDetails { get; set; }

  public virtual DbSet<EmployeePayrollDetails11072023> EmployeePayrollDetails11072023 { get; set; }

  public virtual DbSet<EmployeePayrollDetails28072022> EmployeePayrollDetails28072022 { get; set; }

  public virtual DbSet<EmployeePayrollDetailsHistory> EmployeePayrollDetailsHistory { get; set; }

  public virtual DbSet<EmployeePayrollDetailsHistory31072022> EmployeePayrollDetailsHistory31072022 { get; set; }

  public virtual DbSet<EmployeePayrollHistory> EmployeePayrollHistory { get; set; }

  public virtual DbSet<EmployeePayrollHistory31072022> EmployeePayrollHistory31072022 { get; set; }

  public virtual DbSet<EmployeePayrollVerification> EmployeePayrollVerification { get; set; }

  public virtual DbSet<EmployeePf> EmployeePf { get; set; }

  public virtual DbSet<EmployeeProfileTabs> EmployeeProfileTabs { get; set; }

  public virtual DbSet<EmployeeProfileTabsPermission> EmployeeProfileTabsPermission { get; set; }

  public virtual DbSet<EmployeeReference> EmployeeReference { get; set; }

  public virtual DbSet<EmployeeReferenceTemp> EmployeeReferenceTemp { get; set; }

  public virtual DbSet<EmployeeReplacementInfo> EmployeeReplacementInfo { get; set; }

  public virtual DbSet<EmployeeSapinfo> EmployeeSapinfo { get; set; }

  public virtual DbSet<EmployeeSkillSet> EmployeeSkillSet { get; set; }

  public virtual DbSet<EmployeeSkilsTemp> EmployeeSkilsTemp { get; set; }

  public virtual DbSet<EmployeeTemp> EmployeeTemp { get; set; }

  public virtual DbSet<EmployeeVariableAllowanceDetails> EmployeeVariableAllowanceDetails { get; set; }

  public virtual DbSet<EmployeeVariableAllowanceMaster> EmployeeVariableAllowanceMaster { get; set; }

  public virtual DbSet<EmployeeVerificationForm> EmployeeVerificationForm { get; set; }

  public virtual DbSet<EmployeeWiseOvertimeSettings> EmployeeWiseOvertimeSettings { get; set; }

  public virtual DbSet<EmployeeWiseOvertimeSettingsLog> EmployeeWiseOvertimeSettingsLog { get; set; }

  public virtual DbSet<EmployeepayrollDetailsVerification> EmployeepayrollDetailsVerification { get; set; }

  public virtual DbSet<Employeetype> Employeetype { get; set; }

  public virtual DbSet<Employment> Employment { get; set; }

  public virtual DbSet<Employment02072022> Employment02072022 { get; set; }

  public virtual DbSet<Employment03082022> Employment03082022 { get; set; }

  public virtual DbSet<Employment13012024BankArch> Employment13012024BankArch { get; set; }

  public virtual DbSet<Employment13112023BankArch> Employment13112023BankArch { get; set; }

  public virtual DbSet<Employment14052024Poket> Employment14052024Poket { get; set; }

  public virtual DbSet<Employment21June2022Akhter> Employment21June2022Akhter { get; set; }

  public virtual DbSet<EmploymentHistory> EmploymentHistory { get; set; }

  public virtual DbSet<EmploymentHistoryTemp> EmploymentHistoryTemp { get; set; }

  public virtual DbSet<EmploymentLogHis> EmploymentLogHis { get; set; }

  public virtual DbSet<EmploymentTemp> EmploymentTemp { get; set; }

  public virtual DbSet<EnhancementGuideLine> EnhancementGuideLine { get; set; }

  public virtual DbSet<ExAppClearMaster> ExAppClearMaster { get; set; }

  public virtual DbSet<ExClearanceParameter> ExClearanceParameter { get; set; }

  public virtual DbSet<ExClearanceSetupDetail> ExClearanceSetupDetail { get; set; }

  public virtual DbSet<ExClearanceSetupMaster> ExClearanceSetupMaster { get; set; }

  public virtual DbSet<ExExitInterview> ExExitInterview { get; set; }

  public virtual DbSet<ExLiabilitiesDetails> ExLiabilitiesDetails { get; set; }

  public virtual DbSet<ExResignationApplication> ExResignationApplication { get; set; }

  public virtual DbSet<ExitEmployeeTemp> ExitEmployeeTemp { get; set; }

  public virtual DbSet<ExternalMovementLog> ExternalMovementLog { get; set; }

  public virtual DbSet<Extraciractivities> Extraciractivities { get; set; }

  public virtual DbSet<Facility> Facility { get; set; }

  public virtual DbSet<FacilitySectionMap> FacilitySectionMap { get; set; }

  public virtual DbSet<Feedback> Feedback { get; set; }

  public virtual DbSet<FeedbackAnswered> FeedbackAnswered { get; set; }

  public virtual DbSet<FeedbackDetails> FeedbackDetails { get; set; }

  public virtual DbSet<FeedbackForEmployee> FeedbackForEmployee { get; set; }

  public virtual DbSet<FeedbackQuestion> FeedbackQuestion { get; set; }

  public virtual DbSet<FeedbackQuestionAnswers> FeedbackQuestionAnswers { get; set; }

  public virtual DbSet<FfAllowanceType> FfAllowanceType { get; set; }

  public virtual DbSet<FfArea> FfArea { get; set; }

  public virtual DbSet<FfAreaLog> FfAreaLog { get; set; }

  public virtual DbSet<FfDailyFieldBenifitMapping> FfDailyFieldBenifitMapping { get; set; }

  public virtual DbSet<FfEmployeeWiseFieldForceMapping> FfEmployeeWiseFieldForceMapping { get; set; }

  public virtual DbSet<FfEmployeeWiseFieldForceMappingHistory> FfEmployeeWiseFieldForceMappingHistory { get; set; }

  public virtual DbSet<FfFieldBenifitCategory> FfFieldBenifitCategory { get; set; }

  public virtual DbSet<FfFieldBenifitCategoryHistory> FfFieldBenifitCategoryHistory { get; set; }

  public virtual DbSet<FfMonthEndProcess> FfMonthEndProcess { get; set; }

  public virtual DbSet<FfRegion> FfRegion { get; set; }

  public virtual DbSet<FfRegionLog> FfRegionLog { get; set; }

  public virtual DbSet<FfTaDa> FfTaDa { get; set; }

  public virtual DbSet<FfTaDaHistory> FfTaDaHistory { get; set; }

  public virtual DbSet<FfTempTada> FfTempTada { get; set; }

  public virtual DbSet<FfTerritory> FfTerritory { get; set; }

  public virtual DbSet<FfTerritoryLog> FfTerritoryLog { get; set; }

  public virtual DbSet<FfTerritoryWiseCtcAllowanceMapping> FfTerritoryWiseCtcAllowanceMapping { get; set; }

  public virtual DbSet<FfTerritoryWiseCtcAllowanceMappingHistory> FfTerritoryWiseCtcAllowanceMappingHistory { get; set; }

  public virtual DbSet<FfZone> FfZone { get; set; }

  public virtual DbSet<FfZoneLog> FfZoneLog { get; set; }

  public virtual DbSet<FieldForce> FieldForce { get; set; }

  public virtual DbSet<FieldForce1> FieldForce1 { get; set; }

  public virtual DbSet<FieldForceAbsent> FieldForceAbsent { get; set; }

  public virtual DbSet<FieldForceAbsentArchive2020> FieldForceAbsentArchive2020 { get; set; }

  public virtual DbSet<FieldForceAbsentArchive2021> FieldForceAbsentArchive2021 { get; set; }

  public virtual DbSet<FieldForceAbsentArchive2022> FieldForceAbsentArchive2022 { get; set; }

  public virtual DbSet<FieldForceAbsentArchive2023> FieldForceAbsentArchive2023 { get; set; }

  public virtual DbSet<FieldForceArchive2020> FieldForceArchive2020 { get; set; }

  public virtual DbSet<FieldForceArchive2021> FieldForceArchive2021 { get; set; }

  public virtual DbSet<FieldForceArchive2022> FieldForceArchive2022 { get; set; }

  public virtual DbSet<FieldForceArchive2023> FieldForceArchive2023 { get; set; }

  public virtual DbSet<FieldForceEmployeeTemp> FieldForceEmployeeTemp { get; set; }

  public virtual DbSet<FieldForceRsmMappingInfo> FieldForceRsmMappingInfo { get; set; }

  public virtual DbSet<FieldForceRsmMappingInfoHistory> FieldForceRsmMappingInfoHistory { get; set; }

  public virtual DbSet<FinalSettlement> FinalSettlement { get; set; }

  public virtual DbSet<FinalSettlementArearSalary> FinalSettlementArearSalary { get; set; }

  public virtual DbSet<FinalSettlementDetails> FinalSettlementDetails { get; set; }

  public virtual DbSet<FinancerInfo> FinancerInfo { get; set; }

  public virtual DbSet<FinancialYear> FinancialYear { get; set; }

  public virtual DbSet<FiscalYear> FiscalYear { get; set; }

  public virtual DbSet<FndProfitLoss> FndProfitLoss { get; set; }

  public virtual DbSet<FndReceivePayment> FndReceivePayment { get; set; }

  public virtual DbSet<FndTrialBalance> FndTrialBalance { get; set; }

  public virtual DbSet<FunctionalJob13102022SecA> FunctionalJob13102022SecA { get; set; }

  public virtual DbSet<FundYear> FundYear { get; set; }

  public virtual DbSet<GeneralLedger> GeneralLedger { get; set; }

  public virtual DbSet<GeoFencingUserInfo> GeoFencingUserInfo { get; set; }

  public virtual DbSet<Glsalary> Glsalary { get; set; }

  public virtual DbSet<GlsalaryItem> GlsalaryItem { get; set; }

  public virtual DbSet<GoodsConditionStstus> GoodsConditionStstus { get; set; }

  public virtual DbSet<Grade> Grade { get; set; }

  public virtual DbSet<GradeCtc> GradeCtc { get; set; }

  public virtual DbSet<GradeCtcHistory> GradeCtcHistory { get; set; }

  public virtual DbSet<GradeDesignation> GradeDesignation { get; set; }

  public virtual DbSet<GradeHistory> GradeHistory { get; set; }

  public virtual DbSet<GradeLevel> GradeLevel { get; set; }

  public virtual DbSet<GradeSbu> GradeSbu { get; set; }

  public virtual DbSet<GradeSegment> GradeSegment { get; set; }

  public virtual DbSet<GradeSegmentMap> GradeSegmentMap { get; set; }

  public virtual DbSet<GradeType> GradeType { get; set; }

  public virtual DbSet<GradeTypeInfo> GradeTypeInfo { get; set; }

  public virtual DbSet<GratuityInfomation> GratuityInfomation { get; set; }

  public virtual DbSet<GreetingsOrWishNotificationLog> GreetingsOrWishNotificationLog { get; set; }

  public virtual DbSet<GroupMember> GroupMember { get; set; }

  public virtual DbSet<GroupMemberTemp> GroupMemberTemp { get; set; }

  public virtual DbSet<GroupPermission> GroupPermission { get; set; }

  public virtual DbSet<Groups> Groups { get; set; }

  public virtual DbSet<HcExpense> HcExpense { get; set; }

  public virtual DbSet<Holiday> Holiday { get; set; }

  public virtual DbSet<HolidayType> HolidayType { get; set; }

  public virtual DbSet<HospitailizationNominee> HospitailizationNominee { get; set; }

  public virtual DbSet<HospitalInformation> HospitalInformation { get; set; }

  public virtual DbSet<Hospitalization> Hospitalization { get; set; }

  public virtual DbSet<HospitalizationAttachment> HospitalizationAttachment { get; set; }

  public virtual DbSet<HospitalizationClaimDetails> HospitalizationClaimDetails { get; set; }

  public virtual DbSet<HospitalizationClaimMaster> HospitalizationClaimMaster { get; set; }

  public virtual DbSet<HrDocument> HrDocument { get; set; }

  public virtual DbSet<HrDocumentSubReport> HrDocumentSubReport { get; set; }

  public virtual DbSet<HrFunction> HrFunction { get; set; }

  public virtual DbSet<HrSignatory> HrSignatory { get; set; }

  public virtual DbSet<HrTest> HrTest { get; set; }

  public virtual DbSet<Hris> Hris { get; set; }

  public virtual DbSet<Idpolicy> Idpolicy { get; set; }

  public virtual DbSet<ImageTemp> ImageTemp { get; set; }

  public virtual DbSet<IncrementEligibleNotifyLog> IncrementEligibleNotifyLog { get; set; }

  public virtual DbSet<IncrementGuideLine> IncrementGuideLine { get; set; }

  public virtual DbSet<IncrementRateSettings> IncrementRateSettings { get; set; }

  public virtual DbSet<IndexSection> IndexSection { get; set; }

  public virtual DbSet<IndexSectionDetails> IndexSectionDetails { get; set; }

  public virtual DbSet<InstituteInstructor> InstituteInstructor { get; set; }

  public virtual DbSet<InterestRateConfiguration> InterestRateConfiguration { get; set; }

  public virtual DbSet<Interview> Interview { get; set; }

  public virtual DbSet<InterviewDetails> InterviewDetails { get; set; }

  public virtual DbSet<InterviewHistory> InterviewHistory { get; set; }

  public virtual DbSet<InterviewMarking> InterviewMarking { get; set; }

  public virtual DbSet<InterviewRatingDetails> InterviewRatingDetails { get; set; }

  public virtual DbSet<InterviewSelectionApprover> InterviewSelectionApprover { get; set; }

  public virtual DbSet<InterviewSelectionApproverDetails> InterviewSelectionApproverDetails { get; set; }

  public virtual DbSet<Investment> Investment { get; set; }

  public virtual DbSet<InvestmentAdjustment> InvestmentAdjustment { get; set; }

  public virtual DbSet<InvestmentDetails> InvestmentDetails { get; set; }

  public virtual DbSet<InvestmentEncash> InvestmentEncash { get; set; }

  public virtual DbSet<InvestmentInformation> InvestmentInformation { get; set; }

  public virtual DbSet<InvestmentRenewal> InvestmentRenewal { get; set; }

  public virtual DbSet<InvestmentSlabSettings> InvestmentSlabSettings { get; set; }

  public virtual DbSet<InvestmentType> InvestmentType { get; set; }

  public virtual DbSet<InvoiceInfo> InvoiceInfo { get; set; }

  public virtual DbSet<InvoiceOrderDetails> InvoiceOrderDetails { get; set; }

  public virtual DbSet<IvmsVisitDetails> IvmsVisitDetails { get; set; }

  public virtual DbSet<IvmsVisitor> IvmsVisitor { get; set; }

  public virtual DbSet<JobCode> JobCode { get; set; }

  public virtual DbSet<JobCode15012024> JobCode15012024 { get; set; }

  public virtual DbSet<JobCode20012024> JobCode20012024 { get; set; }

  public virtual DbSet<JobConfirmationEvaluation> JobConfirmationEvaluation { get; set; }

  public virtual DbSet<JobConfirmationEvaluationHistory> JobConfirmationEvaluationHistory { get; set; }

  public virtual DbSet<JobConfirmationMaster> JobConfirmationMaster { get; set; }

  public virtual DbSet<JobConfirmationMaster25032023> JobConfirmationMaster25032023 { get; set; }

  public virtual DbSet<JobConfirmationMaster31012024> JobConfirmationMaster31012024 { get; set; }

  public virtual DbSet<JobConfirmationMasterHistory> JobConfirmationMasterHistory { get; set; }

  public virtual DbSet<JobConfirmationMasterRejectedInfo> JobConfirmationMasterRejectedInfo { get; set; }

  public virtual DbSet<JobConfirmationPayrollDetails> JobConfirmationPayrollDetails { get; set; }

  public virtual DbSet<JobConfirmationReview> JobConfirmationReview { get; set; }

  public virtual DbSet<JobEndType> JobEndType { get; set; }

  public virtual DbSet<JobPerformanceDetails> JobPerformanceDetails { get; set; }

  public virtual DbSet<JobPerformanceDetailsLog> JobPerformanceDetailsLog { get; set; }

  public virtual DbSet<JobPosition> JobPosition { get; set; }

  public virtual DbSet<JobRecommendationType> JobRecommendationType { get; set; }

  public virtual DbSet<JobResponsibility> JobResponsibility { get; set; }

  public virtual DbSet<JoiningBasicInformation> JoiningBasicInformation { get; set; }

  public virtual DbSet<Kpi> Kpi { get; set; }

  public virtual DbSet<KpiGrade> KpiGrade { get; set; }

  public virtual DbSet<KpiyearConfigWithGrade> KpiyearConfigWithGrade { get; set; }

  public virtual DbSet<KpiyearConfiguration> KpiyearConfiguration { get; set; }

  public virtual DbSet<KraAssessmentBehaviour> KraAssessmentBehaviour { get; set; }

  public virtual DbSet<KraAssessmentRating> KraAssessmentRating { get; set; }

  public virtual DbSet<KraAssignApprover> KraAssignApprover { get; set; }

  public virtual DbSet<KraCompetencies> KraCompetencies { get; set; }

  public virtual DbSet<KraCompetencyRating> KraCompetencyRating { get; set; }

  public virtual DbSet<KraDevelopmentPlan> KraDevelopmentPlan { get; set; }

  public virtual DbSet<KraOffDevelopmentActivities> KraOffDevelopmentActivities { get; set; }

  public virtual DbSet<KraOnDevelopmentActivities> KraOnDevelopmentActivities { get; set; }

  public virtual DbSet<KraPerformance> KraPerformance { get; set; }

  public virtual DbSet<KraPerformanceDetails> KraPerformanceDetails { get; set; }

  public virtual DbSet<KraRemarksHistory> KraRemarksHistory { get; set; }

  public virtual DbSet<KraYearAndGradeMap> KraYearAndGradeMap { get; set; }

  public virtual DbSet<KraYearConfigaration> KraYearConfigaration { get; set; }

  public virtual DbSet<LateAttendanceThreeTimesNotifyLog> LateAttendanceThreeTimesNotifyLog { get; set; }

  public virtual DbSet<LatePolicy> LatePolicy { get; set; }

  public virtual DbSet<LatePolicyMaping> LatePolicyMaping { get; set; }

  public virtual DbSet<LeaveAdjustment> LeaveAdjustment { get; set; }

  public virtual DbSet<LeaveAdjustmentInfo> LeaveAdjustmentInfo { get; set; }

  public virtual DbSet<LeaveAdjustmentInfoArchieve> LeaveAdjustmentInfoArchieve { get; set; }

  public virtual DbSet<LeaveApplication> LeaveApplication { get; set; }

  public virtual DbSet<LeaveApplication01102023> LeaveApplication01102023 { get; set; }

  public virtual DbSet<LeaveApplication04112023> LeaveApplication04112023 { get; set; }

  public virtual DbSet<LeaveApplicationArchive2017> LeaveApplicationArchive2017 { get; set; }

  public virtual DbSet<LeaveApplicationArchive2018> LeaveApplicationArchive2018 { get; set; }

  public virtual DbSet<LeaveApplicationArchive2019> LeaveApplicationArchive2019 { get; set; }

  public virtual DbSet<LeaveApplicationArchive2020> LeaveApplicationArchive2020 { get; set; }

  public virtual DbSet<LeaveApplicationArchive2021> LeaveApplicationArchive2021 { get; set; }

  public virtual DbSet<LeaveApplicationArchive2022> LeaveApplicationArchive2022 { get; set; }

  public virtual DbSet<LeaveApplicationArchive2023> LeaveApplicationArchive2023 { get; set; }

  public virtual DbSet<LeaveBalance> LeaveBalance { get; set; }

  public virtual DbSet<LeaveBalance01102023> LeaveBalance01102023 { get; set; }

  public virtual DbSet<LeaveBalance03082022> LeaveBalance03082022 { get; set; }

  public virtual DbSet<LeaveBalance04092022> LeaveBalance04092022 { get; set; }

  public virtual DbSet<LeaveBalance10072023> LeaveBalance10072023 { get; set; }

  public virtual DbSet<LeaveBalance2019> LeaveBalance2019 { get; set; }

  public virtual DbSet<LeaveCoffMap> LeaveCoffMap { get; set; }

  public virtual DbSet<LeaveCostCentreMap> LeaveCostCentreMap { get; set; }

  public virtual DbSet<LeaveDedDetails> LeaveDedDetails { get; set; }

  public virtual DbSet<LeaveDedPolicySbu> LeaveDedPolicySbu { get; set; }

  public virtual DbSet<LeaveDeductionInformation> LeaveDeductionInformation { get; set; }

  public virtual DbSet<LeaveDeductionPolicy> LeaveDeductionPolicy { get; set; }

  public virtual DbSet<LeaveEncashment> LeaveEncashment { get; set; }

  public virtual DbSet<LeaveEncashmentCostCenter> LeaveEncashmentCostCenter { get; set; }

  public virtual DbSet<LeaveEncashmentPolicy> LeaveEncashmentPolicy { get; set; }

  public virtual DbSet<LeaveEncashmentReferenceNo> LeaveEncashmentReferenceNo { get; set; }

  public virtual DbSet<LeaveEncashmentTemp> LeaveEncashmentTemp { get; set; }

  public virtual DbSet<LeaveFormAndCompanyMapping> LeaveFormAndCompanyMapping { get; set; }

  public virtual DbSet<LeaveGradeMap> LeaveGradeMap { get; set; }

  public virtual DbSet<LeavePlaned> LeavePlaned { get; set; }

  public virtual DbSet<LeavePlaned01102023> LeavePlaned01102023 { get; set; }

  public virtual DbSet<LeavePlaned02092023> LeavePlaned02092023 { get; set; }

  public virtual DbSet<LeavePlanedSubmitToDeptHead> LeavePlanedSubmitToDeptHead { get; set; }

  public virtual DbSet<LeavePolicy> LeavePolicy { get; set; }

  public virtual DbSet<LeavePolicyDetails> LeavePolicyDetails { get; set; }

  public virtual DbSet<LeavePolicyException> LeavePolicyException { get; set; }

  public virtual DbSet<LeaveReason> LeaveReason { get; set; }

  public virtual DbSet<LeaveTemp> LeaveTemp { get; set; }

  public virtual DbSet<LeaveTemp1> LeaveTemp1 { get; set; }

  public virtual DbSet<LeaveType> LeaveType { get; set; }

  public virtual DbSet<LeaveWithoutPay> LeaveWithoutPay { get; set; }

  public virtual DbSet<LeaveYearEndProcess> LeaveYearEndProcess { get; set; }

  public virtual DbSet<LeaveYearEndProcess11012022> LeaveYearEndProcess11012022 { get; set; }

  public virtual DbSet<LeaveYearEndProcess14012024> LeaveYearEndProcess14012024 { get; set; }

  public virtual DbSet<LeaveYearEndProcess2019> LeaveYearEndProcess2019 { get; set; }

  public virtual DbSet<LeaveYearEndProcess2020> LeaveYearEndProcess2020 { get; set; }

  public virtual DbSet<LeaveYearEndProcess2021> LeaveYearEndProcess2021 { get; set; }

  public virtual DbSet<LeaveYearEndProcess2022> LeaveYearEndProcess2022 { get; set; }

  public virtual DbSet<LeaveYearEndProcess2023> LeaveYearEndProcess2023 { get; set; }

  public virtual DbSet<LeaveYearEndProcessTemp> LeaveYearEndProcessTemp { get; set; }

  public virtual DbSet<LedgerHead> LedgerHead { get; set; }

  public virtual DbSet<LetterGeneration> LetterGeneration { get; set; }

  public virtual DbSet<LetterInfoJobConfirmation> LetterInfoJobConfirmation { get; set; }

  public virtual DbSet<LetterInfoResignation> LetterInfoResignation { get; set; }

  public virtual DbSet<Lfaapplication> Lfaapplication { get; set; }

  public virtual DbSet<LfaapplicationDetails> LfaapplicationDetails { get; set; }

  public virtual DbSet<LoanEarlySettlement> LoanEarlySettlement { get; set; }

  public virtual DbSet<LoanInformation> LoanInformation { get; set; }

  public virtual DbSet<LoanInformationAudit> LoanInformationAudit { get; set; }

  public virtual DbSet<LoanPurpose> LoanPurpose { get; set; }

  public virtual DbSet<LoanPurposeDetails> LoanPurposeDetails { get; set; }

  public virtual DbSet<LoanSchedule> LoanSchedule { get; set; }

  public virtual DbSet<LoanScheduleAudit> LoanScheduleAudit { get; set; }

  public virtual DbSet<LoanType> LoanType { get; set; }

  public virtual DbSet<LwpDeductionSettings> LwpDeductionSettings { get; set; }

  public virtual DbSet<McGradeMap> McGradeMap { get; set; }

  public virtual DbSet<McPolicy> McPolicy { get; set; }

  public virtual DbSet<Menu> Menu { get; set; }

  public virtual DbSet<Message> Message { get; set; }

  public virtual DbSet<Message2023> Message2023 { get; set; }

  public virtual DbSet<MessageDetails> MessageDetails { get; set; }

  public virtual DbSet<MessageEmployeeDetails> MessageEmployeeDetails { get; set; }

  public virtual DbSet<MobUpload> MobUpload { get; set; }

  public virtual DbSet<MobileBillingTmp> MobileBillingTmp { get; set; }

  public virtual DbSet<MobileBillingUploadtmp> MobileBillingUploadtmp { get; set; }

  public virtual DbSet<MobileCeiling> MobileCeiling { get; set; }

  public virtual DbSet<MobileCeilingTemp> MobileCeilingTemp { get; set; }

  public virtual DbSet<MobileSimVendor> MobileSimVendor { get; set; }

  public virtual DbSet<MobileVendorLedger> MobileVendorLedger { get; set; }

  public virtual DbSet<Module> Module { get; set; }

  public virtual DbSet<Mongla24082022DivDep> Mongla24082022DivDep { get; set; }

  public virtual DbSet<MovementLog> MovementLog { get; set; }

  public virtual DbSet<MovementLogArchive2017> MovementLogArchive2017 { get; set; }

  public virtual DbSet<MovementLogArchive2018> MovementLogArchive2018 { get; set; }

  public virtual DbSet<MovementLogArchive2019> MovementLogArchive2019 { get; set; }

  public virtual DbSet<MovementLogArchive2020> MovementLogArchive2020 { get; set; }

  public virtual DbSet<MovementLogArchive2021> MovementLogArchive2021 { get; set; }

  public virtual DbSet<MovementLogArchive2022> MovementLogArchive2022 { get; set; }

  public virtual DbSet<MovementLogArchive2023> MovementLogArchive2023 { get; set; }

  public virtual DbSet<MovementLogTemp> MovementLogTemp { get; set; }

  public virtual DbSet<MovementPolicy> MovementPolicy { get; set; }

  public virtual DbSet<MovementPolicyDetails> MovementPolicyDetails { get; set; }

  public virtual DbSet<MovementPolicyWithSbulist> MovementPolicyWithSbulist { get; set; }

  public virtual DbSet<MovementType> MovementType { get; set; }

  public virtual DbSet<MyTemp> MyTemp { get; set; }

  public virtual DbSet<NatureOfActions> NatureOfActions { get; set; }

  public virtual DbSet<News> News { get; set; }

  public virtual DbSet<NewsCategory> NewsCategory { get; set; }

  public virtual DbSet<NightPunchConfigaration> NightPunchConfigaration { get; set; }

  public virtual DbSet<NigthReconcilation> NigthReconcilation { get; set; }

  public virtual DbSet<Nominee> Nominee { get; set; }

  public virtual DbSet<NomineeDetails> NomineeDetails { get; set; }

  public virtual DbSet<NomineeTemp> NomineeTemp { get; set; }

  public virtual DbSet<Notice> Notice { get; set; }

  public virtual DbSet<NoticeCategory> NoticeCategory { get; set; }

  public virtual DbSet<NoticeEmployee> NoticeEmployee { get; set; }

  public virtual DbSet<NoticePay> NoticePay { get; set; }

  public virtual DbSet<NotificationEmailType> NotificationEmailType { get; set; }

  public virtual DbSet<NotificationException> NotificationException { get; set; }

  public virtual DbSet<NotificationSource> NotificationSource { get; set; }

  public virtual DbSet<NotificationTypeAndPeerGroup> NotificationTypeAndPeerGroup { get; set; }

  public virtual DbSet<Notifications> Notifications { get; set; }

  public virtual DbSet<Occupation> Occupation { get; set; }

  public virtual DbSet<OfficeTime> OfficeTime { get; set; }

  public virtual DbSet<OfficeTimeHistory> OfficeTimeHistory { get; set; }

  public virtual DbSet<OldSalary> OldSalary { get; set; }

  public virtual DbSet<Oldsalarytemp> Oldsalarytemp { get; set; }

  public virtual DbSet<OmitLateLog> OmitLateLog { get; set; }

  public virtual DbSet<OnSiteClientConveyance> OnSiteClientConveyance { get; set; }

  public virtual DbSet<OnsiteClient> OnsiteClient { get; set; }

  public virtual DbSet<OnsiteClientArchive2017> OnsiteClientArchive2017 { get; set; }

  public virtual DbSet<OnsiteClientArchive2018> OnsiteClientArchive2018 { get; set; }

  public virtual DbSet<OnsiteClientArchive2019> OnsiteClientArchive2019 { get; set; }

  public virtual DbSet<OnsiteClientArchive2020> OnsiteClientArchive2020 { get; set; }

  public virtual DbSet<OnsiteClientArchive2021> OnsiteClientArchive2021 { get; set; }

  public virtual DbSet<OnsiteClientArchive2022> OnsiteClientArchive2022 { get; set; }

  public virtual DbSet<OnsiteClientArchive2023> OnsiteClientArchive2023 { get; set; }

  public virtual DbSet<OrganizerInfo> OrganizerInfo { get; set; }

  public virtual DbSet<OtAllocation> OtAllocation { get; set; }

  public virtual DbSet<OtAllocation2018> OtAllocation2018 { get; set; }

  public virtual DbSet<OtAllocation2019> OtAllocation2019 { get; set; }

  public virtual DbSet<OtAllocation2020> OtAllocation2020 { get; set; }

  public virtual DbSet<OtAllocation2021> OtAllocation2021 { get; set; }

  public virtual DbSet<OtAllocation2022> OtAllocation2022 { get; set; }

  public virtual DbSet<OtAllocation2023> OtAllocation2023 { get; set; }

  public virtual DbSet<OtAllocationDetails> OtAllocationDetails { get; set; }

  public virtual DbSet<OtAllocationDetails2018> OtAllocationDetails2018 { get; set; }

  public virtual DbSet<OtAllocationDetails2019> OtAllocationDetails2019 { get; set; }

  public virtual DbSet<OtAllocationDetails2020> OtAllocationDetails2020 { get; set; }

  public virtual DbSet<OtAllocationDetails2021> OtAllocationDetails2021 { get; set; }

  public virtual DbSet<OtAllocationDetails2022> OtAllocationDetails2022 { get; set; }

  public virtual DbSet<OtAllocationDetails2023> OtAllocationDetails2023 { get; set; }

  public virtual DbSet<OtAllocationDetails27April2022> OtAllocationDetails27April2022 { get; set; }

  public virtual DbSet<OtAmountSetUpInfo> OtAmountSetUpInfo { get; set; }

  public virtual DbSet<OtCompleteListMay2022> OtCompleteListMay2022 { get; set; }

  public virtual DbSet<OtCutofMonth> OtCutofMonth { get; set; }

  public virtual DbSet<OtLimit> OtLimit { get; set; }

  public virtual DbSet<OtLimitDetails> OtLimitDetails { get; set; }

  public virtual DbSet<Otconfiguration> Otconfiguration { get; set; }

  public virtual DbSet<OtgradeSbu> OtgradeSbu { get; set; }

  public virtual DbSet<OtherPayment> OtherPayment { get; set; }

  public virtual DbSet<OtherPaymentCostCentre> OtherPaymentCostCentre { get; set; }

  public virtual DbSet<OtherPaymentReferenceNo> OtherPaymentReferenceNo { get; set; }

  public virtual DbSet<OtherPaymentTemp> OtherPaymentTemp { get; set; }

  public virtual DbSet<OtherPaymentUploadTemp> OtherPaymentUploadTemp { get; set; }

  public virtual DbSet<OtherSchedule> OtherSchedule { get; set; }

  public virtual DbSet<Otsettings> Otsettings { get; set; }

  public virtual DbSet<OtsettingsBg> OtsettingsBg { get; set; }

  public virtual DbSet<OtsettingsMapDesignation> OtsettingsMapDesignation { get; set; }

  public virtual DbSet<Otslab> Otslab { get; set; }

  public virtual DbSet<Outlet> Outlet { get; set; }

  public virtual DbSet<OutletVisitingSchedule> OutletVisitingSchedule { get; set; }

  public virtual DbSet<OutletVisitingScheduleDetails> OutletVisitingScheduleDetails { get; set; }

  public virtual DbSet<OverTime> OverTime { get; set; }

  public virtual DbSet<OverTime10082022> OverTime10082022 { get; set; }

  public virtual DbSet<OverTimeApprovalHistory> OverTimeApprovalHistory { get; set; }

  public virtual DbSet<OverTimeArchive2018> OverTimeArchive2018 { get; set; }

  public virtual DbSet<OverTimeArchive2019> OverTimeArchive2019 { get; set; }

  public virtual DbSet<OverTimeArchive2020> OverTimeArchive2020 { get; set; }

  public virtual DbSet<OverTimeArchive2021> OverTimeArchive2021 { get; set; }

  public virtual DbSet<OverTimeArchive2022> OverTimeArchive2022 { get; set; }

  public virtual DbSet<OverTimeArchive2023> OverTimeArchive2023 { get; set; }

  public virtual DbSet<OverTimeCtcAdjustment> OverTimeCtcAdjustment { get; set; }

  public virtual DbSet<OverTimeMonthEndRecommendedNotification> OverTimeMonthEndRecommendedNotification { get; set; }

  public virtual DbSet<OverTimePaidDetails> OverTimePaidDetails { get; set; }

  public virtual DbSet<OverTimeProcessDetails> OverTimeProcessDetails { get; set; }

  public virtual DbSet<OverTimeTaxInfo> OverTimeTaxInfo { get; set; }

  public virtual DbSet<OverTimeType> OverTimeType { get; set; }

  public virtual DbSet<OvertimeDeduction> OvertimeDeduction { get; set; }

  public virtual DbSet<OvertimePaid> OvertimePaid { get; set; }

  public virtual DbSet<OvertimePaymentCostCentre> OvertimePaymentCostCentre { get; set; }

  public virtual DbSet<OvertimeProcess> OvertimeProcess { get; set; }

  public virtual DbSet<OvertimeTemp> OvertimeTemp { get; set; }

  public virtual DbSet<PaScoreConfig> PaScoreConfig { get; set; }

  public virtual DbSet<ParallelApprover> ParallelApprover { get; set; }

  public virtual DbSet<PartialPayment> PartialPayment { get; set; }

  public virtual DbSet<PasswordHistory> PasswordHistory { get; set; }

  public virtual DbSet<Payband> Payband { get; set; }

  public virtual DbSet<Payroll> Payroll { get; set; }

  public virtual DbSet<PayrollAccessRestriction> PayrollAccessRestriction { get; set; }

  public virtual DbSet<PayrollAdjustment> PayrollAdjustment { get; set; }

  public virtual DbSet<PayrollAdjustmentDetails> PayrollAdjustmentDetails { get; set; }

  public virtual DbSet<PayrollBlockInfo> PayrollBlockInfo { get; set; }

  public virtual DbSet<PayrollCycleMapping> PayrollCycleMapping { get; set; }

  public virtual DbSet<PayrollCycleSetup> PayrollCycleSetup { get; set; }

  public virtual DbSet<PayrollException> PayrollException { get; set; }

  public virtual DbSet<PayrollHistory> PayrollHistory { get; set; }

  public virtual DbSet<PayrollIntegrationSetup> PayrollIntegrationSetup { get; set; }

  public virtual DbSet<PerformanceFactors> PerformanceFactors { get; set; }

  public virtual DbSet<PerformanceFactorsRating> PerformanceFactorsRating { get; set; }

  public virtual DbSet<PerformanceFactory> PerformanceFactory { get; set; }

  public virtual DbSet<PerformanceFactoryRating> PerformanceFactoryRating { get; set; }

  public virtual DbSet<PerformanceFactoryRatingLog> PerformanceFactoryRatingLog { get; set; }

  public virtual DbSet<PerformancePayroll> PerformancePayroll { get; set; }

  public virtual DbSet<PerformancePayrollDetails> PerformancePayrollDetails { get; set; }

  public virtual DbSet<PerformanceReview> PerformanceReview { get; set; }

  public virtual DbSet<PerformanceReviewAttribute> PerformanceReviewAttribute { get; set; }

  public virtual DbSet<PerformanceReviewDetail> PerformanceReviewDetail { get; set; }

  public virtual DbSet<PerformanceReviewDetailDraft> PerformanceReviewDetailDraft { get; set; }

  public virtual DbSet<PerformanceReviewLog> PerformanceReviewLog { get; set; }

  public virtual DbSet<PerformanceReviewMaster> PerformanceReviewMaster { get; set; }

  public virtual DbSet<PerformanceReviewMasterDraft> PerformanceReviewMasterDraft { get; set; }

  public virtual DbSet<PfFundEligibleAmount> PfFundEligibleAmount { get; set; }

  public virtual DbSet<PfFundEligibleAmountMapping> PfFundEligibleAmountMapping { get; set; }

  public virtual DbSet<PfIgnore> PfIgnore { get; set; }

  public virtual DbSet<PfMembership> PfMembership { get; set; }

  public virtual DbSet<Pfrecovery> Pfrecovery { get; set; }

  public virtual DbSet<PhoneBillDeductionTemp> PhoneBillDeductionTemp { get; set; }

  public virtual DbSet<PmsInstructionConfig> PmsInstructionConfig { get; set; }

  public virtual DbSet<PmsTabConfig> PmsTabConfig { get; set; }

  public virtual DbSet<PmsTabTitles> PmsTabTitles { get; set; }

  public virtual DbSet<PostatusType> PostatusType { get; set; }

  public virtual DbSet<PostingType> PostingType { get; set; }

  public virtual DbSet<PreferredName22012023> PreferredName22012023 { get; set; }

  public virtual DbSet<ProfitLossDetails> ProfitLossDetails { get; set; }

  public virtual DbSet<ProfitLossMaster> ProfitLossMaster { get; set; }

  public virtual DbSet<Project> Project { get; set; }

  public virtual DbSet<ProjectAssignment> ProjectAssignment { get; set; }

  public virtual DbSet<ProjectDepartment> ProjectDepartment { get; set; }

  public virtual DbSet<ProjectTask> ProjectTask { get; set; }

  public virtual DbSet<PromotionGuideLine> PromotionGuideLine { get; set; }

  public virtual DbSet<ProrataBonusForEmployeeType> ProrataBonusForEmployeeType { get; set; }

  public virtual DbSet<Psolocation> Psolocation { get; set; }

  public virtual DbSet<PunishmentDetails> PunishmentDetails { get; set; }

  public virtual DbSet<PunishmentInfo> PunishmentInfo { get; set; }

  public virtual DbSet<PunishmentSettings> PunishmentSettings { get; set; }

  public virtual DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }

  public virtual DbSet<QuestionCategory> QuestionCategory { get; set; }

  public virtual DbSet<RcruitmentJobVacancy> RcruitmentJobVacancy { get; set; }

  public virtual DbSet<RcruitmentRequisitionForm> RcruitmentRequisitionForm { get; set; }

  public virtual DbSet<ReasonOfReview> ReasonOfReview { get; set; }

  public virtual DbSet<RecApplicantTraining> RecApplicantTraining { get; set; }

  public virtual DbSet<RecCandidateJoining> RecCandidateJoining { get; set; }

  public virtual DbSet<RecCandidateSalary> RecCandidateSalary { get; set; }

  public virtual DbSet<RecCompetencies> RecCompetencies { get; set; }

  public virtual DbSet<RecCompitencyArea> RecCompitencyArea { get; set; }

  public virtual DbSet<RecEducationalVerification> RecEducationalVerification { get; set; }

  public virtual DbSet<RecEmailBody> RecEmailBody { get; set; }

  public virtual DbSet<RecEmployeeCategory> RecEmployeeCategory { get; set; }

  public virtual DbSet<RecEmployeeReplacementInfo> RecEmployeeReplacementInfo { get; set; }

  public virtual DbSet<RecEmploymentHistoryCheck> RecEmploymentHistoryCheck { get; set; }

  public virtual DbSet<RecExperienceVerification> RecExperienceVerification { get; set; }

  public virtual DbSet<RecInterviewRatingDetails> RecInterviewRatingDetails { get; set; }

  public virtual DbSet<RecInterviewRatingMaster> RecInterviewRatingMaster { get; set; }

  public virtual DbSet<RecInterviewRatingMasterHistory> RecInterviewRatingMasterHistory { get; set; }

  public virtual DbSet<RecInvitationType> RecInvitationType { get; set; }

  public virtual DbSet<RecJobDescription> RecJobDescription { get; set; }

  public virtual DbSet<RecJobDescriptionSetup> RecJobDescriptionSetup { get; set; }

  public virtual DbSet<RecJobIdSelectionDetails> RecJobIdSelectionDetails { get; set; }

  public virtual DbSet<RecJobIdSelectionMaster> RecJobIdSelectionMaster { get; set; }

  public virtual DbSet<RecJobVacancy> RecJobVacancy { get; set; }

  public virtual DbSet<RecPresentationMarks> RecPresentationMarks { get; set; }

  public virtual DbSet<RecPresentationMarksHistory> RecPresentationMarksHistory { get; set; }

  public virtual DbSet<RecReferenceCheck> RecReferenceCheck { get; set; }

  public virtual DbSet<RecRequisitionHeldInfo> RecRequisitionHeldInfo { get; set; }

  public virtual DbSet<RecSalaryMatchingInformation> RecSalaryMatchingInformation { get; set; }

  public virtual DbSet<RecSecInterviewRating> RecSecInterviewRating { get; set; }

  public virtual DbSet<RecSecondInterview> RecSecondInterview { get; set; }

  public virtual DbSet<RecThirdInterviewRating> RecThirdInterviewRating { get; set; }

  public virtual DbSet<RecWrittenMarks> RecWrittenMarks { get; set; }

  public virtual DbSet<RecWrittenMarksHistory> RecWrittenMarksHistory { get; set; }

  public virtual DbSet<RecruitmentApplicantEducation> RecruitmentApplicantEducation { get; set; }

  public virtual DbSet<RecruitmentApplicantEmploymentHistory> RecruitmentApplicantEmploymentHistory { get; set; }

  public virtual DbSet<RecruitmentApplicantReferance> RecruitmentApplicantReferance { get; set; }

  public virtual DbSet<RecruitmentApplicantSkill> RecruitmentApplicantSkill { get; set; }

  public virtual DbSet<RecruitmentEligibleType> RecruitmentEligibleType { get; set; }

  public virtual DbSet<RecruitmentSource> RecruitmentSource { get; set; }

  public virtual DbSet<RecruitmentStandardForm> RecruitmentStandardForm { get; set; }

  public virtual DbSet<RecruitmentType> RecruitmentType { get; set; }

  public virtual DbSet<RefRequisitionSn> RefRequisitionSn { get; set; }

  public virtual DbSet<ReferenceType> ReferenceType { get; set; }

  public virtual DbSet<RegionDivisionMap> RegionDivisionMap { get; set; }

  public virtual DbSet<RelationShip> RelationShip { get; set; }

  public virtual DbSet<Religion> Religion { get; set; }

  public virtual DbSet<ReligionCtcMapping> ReligionCtcMapping { get; set; }

  public virtual DbSet<RemunerationSuspension> RemunerationSuspension { get; set; }

  public virtual DbSet<ReportBuilder> ReportBuilder { get; set; }

  public virtual DbSet<ReportSignatoryMapping> ReportSignatoryMapping { get; set; }

  public virtual DbSet<ReportToApproverTemp> ReportToApproverTemp { get; set; }

  public virtual DbSet<ReqruitmentEducationalInstitute> ReqruitmentEducationalInstitute { get; set; }

  public virtual DbSet<RequisitEmployee> RequisitEmployee { get; set; }

  public virtual DbSet<ResignationAcceptanceLetterType> ResignationAcceptanceLetterType { get; set; }

  public virtual DbSet<ResignationApplication> ResignationApplication { get; set; }

  public virtual DbSet<ResignationMapping> ResignationMapping { get; set; }

  public virtual DbSet<ResignationRemarks> ResignationRemarks { get; set; }

  public virtual DbSet<RevDistributionDetails> RevDistributionDetails { get; set; }

  public virtual DbSet<RevDistributionMaster> RevDistributionMaster { get; set; }

  public virtual DbSet<Reward> Reward { get; set; }

  public virtual DbSet<RewardDistribution> RewardDistribution { get; set; }

  public virtual DbSet<RewardEligibility> RewardEligibility { get; set; }

  public virtual DbSet<RewardGenerate> RewardGenerate { get; set; }

  public virtual DbSet<RewardGenerateDetails> RewardGenerateDetails { get; set; }

  public virtual DbSet<RewardPenaltyGuideLine> RewardPenaltyGuideLine { get; set; }

  public virtual DbSet<RollingGroup> RollingGroup { get; set; }

  public virtual DbSet<RollingGroupAndPolicyMap> RollingGroupAndPolicyMap { get; set; }

  public virtual DbSet<RollingGroupDetails> RollingGroupDetails { get; set; }

  public virtual DbSet<Roster> Roster { get; set; }

  public virtual DbSet<RosterDetails> RosterDetails { get; set; }

  public virtual DbSet<RosterDetails18112023> RosterDetails18112023 { get; set; }

  public virtual DbSet<RosterDetails27062024> RosterDetails27062024 { get; set; }

  public virtual DbSet<RosterDetails29082022> RosterDetails29082022 { get; set; }

  public virtual DbSet<RosterDetailsApproval> RosterDetailsApproval { get; set; }

  public virtual DbSet<RosterDetailsArchive2017> RosterDetailsArchive2017 { get; set; }

  public virtual DbSet<RosterDetailsArchive2018> RosterDetailsArchive2018 { get; set; }

  public virtual DbSet<RosterDetailsArchive2019> RosterDetailsArchive2019 { get; set; }

  public virtual DbSet<RosterDetailsArchive2020> RosterDetailsArchive2020 { get; set; }

  public virtual DbSet<RosterDetailsArchive2021> RosterDetailsArchive2021 { get; set; }

  public virtual DbSet<RosterDetailsArchive2022> RosterDetailsArchive2022 { get; set; }

  public virtual DbSet<RosterDetailsArchive2023> RosterDetailsArchive2023 { get; set; }

  public virtual DbSet<RosterDraftDetails> RosterDraftDetails { get; set; }

  public virtual DbSet<RosterDraftDetailsForRl> RosterDraftDetailsForRl { get; set; }

  public virtual DbSet<RosterDraftMaster> RosterDraftMaster { get; set; }

  public virtual DbSet<RosterForwardableEmployee> RosterForwardableEmployee { get; set; }

  public virtual DbSet<RosterFwdEmployeeDetails> RosterFwdEmployeeDetails { get; set; }

  public virtual DbSet<RosterLock> RosterLock { get; set; }

  public virtual DbSet<RosterRollingGroupTemp> RosterRollingGroupTemp { get; set; }

  public virtual DbSet<RosterRunMonth> RosterRunMonth { get; set; }

  public virtual DbSet<Rsmregion> Rsmregion { get; set; }

  public virtual DbSet<RsmregionManager> RsmregionManager { get; set; }

  public virtual DbSet<Salary> Salary { get; set; }

  public virtual DbSet<Salary20230719> Salary20230719 { get; set; }

  public virtual DbSet<SalaryBk22072023> SalaryBk22072023 { get; set; }

  public virtual DbSet<SalaryByAdmin> SalaryByAdmin { get; set; }

  public virtual DbSet<SalaryClose> SalaryClose { get; set; }

  public virtual DbSet<SalaryCloseCompanyMap> SalaryCloseCompanyMap { get; set; }

  public virtual DbSet<SalaryCostCentre> SalaryCostCentre { get; set; }

  public virtual DbSet<SalaryDetails> SalaryDetails { get; set; }

  public virtual DbSet<SalaryDetailsByAdmin> SalaryDetailsByAdmin { get; set; }

  public virtual DbSet<SalaryDetailsProcess> SalaryDetailsProcess { get; set; }

  public virtual DbSet<SalaryDetailsProcessAudit> SalaryDetailsProcessAudit { get; set; }

  public virtual DbSet<SalaryDetailsTemp> SalaryDetailsTemp { get; set; }

  public virtual DbSet<SalaryIncrement> SalaryIncrement { get; set; }

  public virtual DbSet<SalaryIncrementDetails> SalaryIncrementDetails { get; set; }

  public virtual DbSet<SalaryProcess> SalaryProcess { get; set; }

  public virtual DbSet<SalaryProcess09062024> SalaryProcess09062024 { get; set; }

  public virtual DbSet<SalaryProcessAudit> SalaryProcessAudit { get; set; }

  public virtual DbSet<SalaryRefereneNo> SalaryRefereneNo { get; set; }

  public virtual DbSet<SalaryRemark> SalaryRemark { get; set; }

  public virtual DbSet<SalaryTemp> SalaryTemp { get; set; }

  public virtual DbSet<SalaryUploadTemplate> SalaryUploadTemplate { get; set; }

  public virtual DbSet<SalesTarget> SalesTarget { get; set; }

  public virtual DbSet<SalesTeam> SalesTeam { get; set; }

  public virtual DbSet<SalesTeamMember> SalesTeamMember { get; set; }

  public virtual DbSet<Section> Section { get; set; }

  public virtual DbSet<SectorAAppraisalProScale03072022> SectorAAppraisalProScale03072022 { get; set; }

  public virtual DbSet<SectorAMonglaScale07112022> SectorAMonglaScale07112022 { get; set; }

  public virtual DbSet<SelfKpi> SelfKpi { get; set; }

  public virtual DbSet<SeniorStaff> SeniorStaff { get; set; }

  public virtual DbSet<SentEmail> SentEmail { get; set; }

  public virtual DbSet<Sheet1> Sheet1 { get; set; }

  public virtual DbSet<ShifRollingPolicyMap> ShifRollingPolicyMap { get; set; }

  public virtual DbSet<Shift> Shift { get; set; }

  public virtual DbSet<Shift03042022> Shift03042022 { get; set; }

  public virtual DbSet<Shift25032023> Shift25032023 { get; set; }

  public virtual DbSet<ShiftBk12032024Rmdn> ShiftBk12032024Rmdn { get; set; }

  public virtual DbSet<ShiftHistory> ShiftHistory { get; set; }

  public virtual DbSet<ShiftMapping> ShiftMapping { get; set; }

  public virtual DbSet<ShiftRollingPolicy> ShiftRollingPolicy { get; set; }

  public virtual DbSet<ShiftRollingPolicyDetails> ShiftRollingPolicyDetails { get; set; }

  public virtual DbSet<SiclMngEmp29012023> SiclMngEmp29012023 { get; set; }

  public virtual DbSet<SiclSecAEnhanced08082022> SiclSecAEnhanced08082022 { get; set; }

  public virtual DbSet<SimNumberSettings> SimNumberSettings { get; set; }

  public virtual DbSet<Skill> Skill { get; set; }

  public virtual DbSet<SliderPicture> SliderPicture { get; set; }

  public virtual DbSet<Smsrecieved> Smsrecieved { get; set; }

  public virtual DbSet<Smssent> Smssent { get; set; }

  public virtual DbSet<Sqanswer> Sqanswer { get; set; }

  public virtual DbSet<SubKpi> SubKpi { get; set; }

  public virtual DbSet<SubjectOfAccountDetails> SubjectOfAccountDetails { get; set; }

  public virtual DbSet<SubjectOfAccounts> SubjectOfAccounts { get; set; }

  public virtual DbSet<SubjectOfAccountsHead> SubjectOfAccountsHead { get; set; }

  public virtual DbSet<SubsidaryAccountMapping> SubsidaryAccountMapping { get; set; }

  public virtual DbSet<SugarClient> SugarClient { get; set; }

  public virtual DbSet<SupportTransactionLog> SupportTransactionLog { get; set; }

  public virtual DbSet<SupportTransactionLog17102023> SupportTransactionLog17102023 { get; set; }

  public virtual DbSet<SupportTransactionLog31102023> SupportTransactionLog31102023 { get; set; }

  public virtual DbSet<Survey> Survey { get; set; }

  public virtual DbSet<SurveyAnswer> SurveyAnswer { get; set; }

  public virtual DbSet<SurveyCategory> SurveyCategory { get; set; }

  public virtual DbSet<SurveyDetails> SurveyDetails { get; set; }

  public virtual DbSet<SurveyEmployee> SurveyEmployee { get; set; }

  public virtual DbSet<SurveyQuestion> SurveyQuestion { get; set; }

  public virtual DbSet<SuspensionDetails> SuspensionDetails { get; set; }

  public virtual DbSet<SystemSettings> SystemSettings { get; set; }

  public virtual DbSet<TaskType> TaskType { get; set; }

  public virtual DbSet<TaxBfbilMaySal22072023> TaxBfbilMaySal22072023 { get; set; }

  public virtual DbSet<TaxInvestmentLetterReferenceNo> TaxInvestmentLetterReferenceNo { get; set; }

  public virtual DbSet<TaxSlabSettings> TaxSlabSettings { get; set; }

  public virtual DbSet<TaxYear> TaxYear { get; set; }

  public virtual DbSet<TempActualBasic> TempActualBasic { get; set; }

  public virtual DbSet<TempApplicantInformation> TempApplicantInformation { get; set; }

  public virtual DbSet<TempApplicantInterviewMarks> TempApplicantInterviewMarks { get; set; }

  public virtual DbSet<TempApplicantWrittenMarks> TempApplicantWrittenMarks { get; set; }

  public virtual DbSet<TempAppraisalData> TempAppraisalData { get; set; }

  public virtual DbSet<TempApproverRecommender> TempApproverRecommender { get; set; }

  public virtual DbSet<TempAttendanceMonthEnd> TempAttendanceMonthEnd { get; set; }

  public virtual DbSet<TempEmployee> TempEmployee { get; set; }

  public virtual DbSet<TempEmployeeLeaveUpload> TempEmployeeLeaveUpload { get; set; }

  public virtual DbSet<TempEmployeeWiseOvertime> TempEmployeeWiseOvertime { get; set; }

  public virtual DbSet<TempEmployment> TempEmployment { get; set; }

  public virtual DbSet<TempExcelData> TempExcelData { get; set; }

  public virtual DbSet<TempFieldForce> TempFieldForce { get; set; }

  public virtual DbSet<TempFuncDesign29012023> TempFuncDesign29012023 { get; set; }

  public virtual DbSet<TempGroupPermissionHrms> TempGroupPermissionHrms { get; set; }

  public virtual DbSet<TempLeaveUpload30052022> TempLeaveUpload30052022 { get; set; }

  public virtual DbSet<TempLeaveWithoutPay> TempLeaveWithoutPay { get; set; }

  public virtual DbSet<TempMenuHrms> TempMenuHrms { get; set; }

  public virtual DbSet<TempMissingConfimDate29012023> TempMissingConfimDate29012023 { get; set; }

  public virtual DbSet<TempMultiApprover> TempMultiApprover { get; set; }

  public virtual DbSet<TempOverTime> TempOverTime { get; set; }

  public virtual DbSet<TempOverTimeCtcAdjustment> TempOverTimeCtcAdjustment { get; set; }

  public virtual DbSet<TempPfincrementSicl29072024> TempPfincrementSicl29072024 { get; set; }

  public virtual DbSet<TempRecommendation> TempRecommendation { get; set; }

  public virtual DbSet<TempRosterUploadFinal> TempRosterUploadFinal { get; set; }

  public virtual DbSet<Tempmessage> Tempmessage { get; set; }

  public virtual DbSet<TempsalaryNov> TempsalaryNov { get; set; }

  public virtual DbSet<Thana> Thana { get; set; }

  public virtual DbSet<Timesheet> Timesheet { get; set; }

  public virtual DbSet<TmpBusUpload> TmpBusUpload { get; set; }

  public virtual DbSet<TmpTaxUpload> TmpTaxUpload { get; set; }

  public virtual DbSet<ToDelete> ToDelete { get; set; }

  public virtual DbSet<TrConductorsAndPlanningMap> TrConductorsAndPlanningMap { get; set; }

  public virtual DbSet<TrFinancerAndPlanningMap> TrFinancerAndPlanningMap { get; set; }

  public virtual DbSet<TrOrganizerAndPlanningMap> TrOrganizerAndPlanningMap { get; set; }

  public virtual DbSet<TrainingBooking> TrainingBooking { get; set; }

  public virtual DbSet<TrainingCertificateUpload> TrainingCertificateUpload { get; set; }

  public virtual DbSet<TrainingCost> TrainingCost { get; set; }

  public virtual DbSet<TrainingForcast> TrainingForcast { get; set; }

  public virtual DbSet<TrainingHistory> TrainingHistory { get; set; }

  public virtual DbSet<TrainingHistoryTemp> TrainingHistoryTemp { get; set; }

  public virtual DbSet<TrainingInfo> TrainingInfo { get; set; }

  public virtual DbSet<TrainingMarks> TrainingMarks { get; set; }

  public virtual DbSet<TrainingMarksDetails> TrainingMarksDetails { get; set; }

  public virtual DbSet<TrainingMarksTemp> TrainingMarksTemp { get; set; }

  public virtual DbSet<TrainingNameForForcastInfo> TrainingNameForForcastInfo { get; set; }

  public virtual DbSet<TrainingParticipantAttendance> TrainingParticipantAttendance { get; set; }

  public virtual DbSet<TrainingPlanning> TrainingPlanning { get; set; }

  public virtual DbSet<TrainingPlanningAndRequisitionMap> TrainingPlanningAndRequisitionMap { get; set; }

  public virtual DbSet<TrainingRequisitParticipant> TrainingRequisitParticipant { get; set; }

  public virtual DbSet<TrainingRequisitionForm> TrainingRequisitionForm { get; set; }

  public virtual DbSet<TrainingScheduleEvents> TrainingScheduleEvents { get; set; }

  public virtual DbSet<TrainingType> TrainingType { get; set; }

  public virtual DbSet<TraningSchedule> TraningSchedule { get; set; }

  public virtual DbSet<TransectionType> TransectionType { get; set; }

  public virtual DbSet<TransferPromotion> TransferPromotion { get; set; }

  public virtual DbSet<TransferPromotionDelete> TransferPromotionDelete { get; set; }

  public virtual DbSet<TransferPromotionTemp> TransferPromotionTemp { get; set; }

  public virtual DbSet<TrusteeBoardSignatory> TrusteeBoardSignatory { get; set; }

  public virtual DbSet<UnauthorizedAbsence> UnauthorizedAbsence { get; set; }

  public virtual DbSet<UnauthorizedAbsenceDetails> UnauthorizedAbsenceDetails { get; set; }

  public virtual DbSet<Users> Users { get; set; }

  public virtual DbSet<Userstemp> Userstemp { get; set; }

  public virtual DbSet<Venue> Venue { get; set; }

  public virtual DbSet<VerificationType> VerificationType { get; set; }

  public virtual DbSet<VhAssignCarToManager> VhAssignCarToManager { get; set; }

  public virtual DbSet<VhAssignDriver> VhAssignDriver { get; set; }

  public virtual DbSet<VhAssignVhToDivision> VhAssignVhToDivision { get; set; }

  public virtual DbSet<VhAssignVhToLocation> VhAssignVhToLocation { get; set; }

  public virtual DbSet<VhColor> VhColor { get; set; }

  public virtual DbSet<VhDivision> VhDivision { get; set; }

  public virtual DbSet<VhDriver> VhDriver { get; set; }

  public virtual DbSet<VhEmpPickUpSpot> VhEmpPickUpSpot { get; set; }

  public virtual DbSet<VhEmployeePickUpSpot> VhEmployeePickUpSpot { get; set; }

  public virtual DbSet<VhEngineCapacity> VhEngineCapacity { get; set; }

  public virtual DbSet<VhExpenseAndReimbursement> VhExpenseAndReimbursement { get; set; }

  public virtual DbSet<VhExpenseDetails> VhExpenseDetails { get; set; }

  public virtual DbSet<VhExpenseMaster> VhExpenseMaster { get; set; }

  public virtual DbSet<VhExpenseType> VhExpenseType { get; set; }

  public virtual DbSet<VhFuelType> VhFuelType { get; set; }

  public virtual DbSet<VhLocationMaster> VhLocationMaster { get; set; }

  public virtual DbSet<VhManufacture> VhManufacture { get; set; }

  public virtual DbSet<VhMasterLocation> VhMasterLocation { get; set; }

  public virtual DbSet<VhMilesLogBook> VhMilesLogBook { get; set; }

  public virtual DbSet<VhModel> VhModel { get; set; }

  public virtual DbSet<VhPassengerRoute> VhPassengerRoute { get; set; }

  public virtual DbSet<VhProvider> VhProvider { get; set; }

  public virtual DbSet<VhRegNoFirst> VhRegNoFirst { get; set; }

  public virtual DbSet<VhRegNoMiddle> VhRegNoMiddle { get; set; }

  public virtual DbSet<VhRegion> VhRegion { get; set; }

  public virtual DbSet<VhRenewal> VhRenewal { get; set; }

  public virtual DbSet<VhRenewalTemp> VhRenewalTemp { get; set; }

  public virtual DbSet<VhRenewalType> VhRenewalType { get; set; }

  public virtual DbSet<VhRequisitEmployee> VhRequisitEmployee { get; set; }

  public virtual DbSet<VhRequisitionAllocation> VhRequisitionAllocation { get; set; }

  public virtual DbSet<VhRequisitionType> VhRequisitionType { get; set; }

  public virtual DbSet<VhRoute> VhRoute { get; set; }

  public virtual DbSet<VhRouteAssignPassengerVehicleMap> VhRouteAssignPassengerVehicleMap { get; set; }

  public virtual DbSet<VhRouteAssignPassengerVehicleMapDetails> VhRouteAssignPassengerVehicleMapDetails { get; set; }

  public virtual DbSet<VhRouteVehicleMap> VhRouteVehicleMap { get; set; }

  public virtual DbSet<VhServiceCenter> VhServiceCenter { get; set; }

  public virtual DbSet<VhUsageType> VhUsageType { get; set; }

  public virtual DbSet<VhVehicleOwnership> VhVehicleOwnership { get; set; }

  public virtual DbSet<VhVehicleRegistration> VhVehicleRegistration { get; set; }

  public virtual DbSet<VhVehicleRequisition> VhVehicleRequisition { get; set; }

  public virtual DbSet<VhVehicleType> VhVehicleType { get; set; }

  public virtual DbSet<VhVehileRouteAssignToPassengerMapDetails> VhVehileRouteAssignToPassengerMapDetails { get; set; }

  public virtual DbSet<VigilanceDutyRoster> VigilanceDutyRoster { get; set; }

  public virtual DbSet<VigilanceDutyTime> VigilanceDutyTime { get; set; }

  public virtual DbSet<VigilanceRewardType> VigilanceRewardType { get; set; }

  public virtual DbSet<VigilanceRosterDetails> VigilanceRosterDetails { get; set; }

  public virtual DbSet<VigilanceRosterDetailsTemp> VigilanceRosterDetailsTemp { get; set; }

  public virtual DbSet<VigilanceRosterPolicy> VigilanceRosterPolicy { get; set; }

  public virtual DbSet<VoucharDetails> VoucharDetails { get; set; }

  public virtual DbSet<VoucharMaster> VoucharMaster { get; set; }

  public virtual DbSet<VoucharType> VoucharType { get; set; }

  public virtual DbSet<WelfareFund> WelfareFund { get; set; }

  public virtual DbSet<WelfareFundApplication> WelfareFundApplication { get; set; }

  public virtual DbSet<Wfaction> Wfaction { get; set; }

  public virtual DbSet<Wfstate> Wfstate { get; set; }

  public virtual DbSet<WorkLog> WorkLog { get; set; }

  public virtual DbSet<WrittenMarksTemp> WrittenMarksTemp { get; set; }

  public virtual DbSet<XP> XP { get; set; }

  public virtual DbSet<Xbi> Xbi { get; set; }

  public virtual DbSet<Xdelete> Xdelete { get; set; }

  public virtual DbSet<Xena> Xena { get; set; }

  public virtual DbSet<Xpd> Xpd { get; set; }

  public virtual DbSet<Xpdh> Xpdh { get; set; }

  public virtual DbSet<Xph> Xph { get; set; }

  public virtual DbSet<XxtoDeleteEmp> XxtoDeleteEmp { get; set; }

  public virtual DbSet<ZBcdlpf> ZBcdlpf { get; set; }

  public virtual DbSet<ZEmployeeLeaveTemp> ZEmployeeLeaveTemp { get; set; }

  public virtual DbSet<ZfieldForce> ZfieldForce { get; set; }

  public virtual DbSet<ZrosterDetailsBackup> ZrosterDetailsBackup { get; set; }

//  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//      => optionsBuilder.UseSqlServer("Server=DESKTOP-657HR8U;User ID=sa;password=@BIr3011;Database=BG_A;TrustServerCertificate=true;");

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    modelBuilder.Entity<Crmcourse>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("CRMCourse");

      entity.Property(e => e.AdditionalInformationOfCourse)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.After2YearsPswcompletingCourse)
          .HasMaxLength(300)
          .IsUnicode(false)
          .HasColumnName("After2YearsPSWCompletingCourse");
      entity.Property(e => e.ApplicationFee).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AwardingBody)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.CountryBenefits)
          .HasMaxLength(500)
          .IsUnicode(false);
      entity.Property(e => e.CourseBenefits)
          .HasMaxLength(200)
          .IsUnicode(false);
      entity.Property(e => e.CourseCategory)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.CourseDuration)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.CourseFee).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CourseId).ValueGeneratedOnAdd();
      entity.Property(e => e.CourseLevel)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.CourseTitle)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.DocumentId)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.FundsRequirementforVisa)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.GeneralEligibility)
          .HasMaxLength(200)
          .IsUnicode(false);
      entity.Property(e => e.InstitutionalBenefits)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.KeyModules)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.LanguagesRequirement)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.MonthlyLivingCost).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PartTimeWorkDetails)
          .HasMaxLength(500)
          .IsUnicode(false);
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.VisaRequirement)
          .HasMaxLength(500)
          .IsUnicode(false);
    });

    modelBuilder.Entity<CrmcourseIntake>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("CRMCourseIntake");

      entity.Property(e => e.CourseIntakeId).ValueGeneratedOnAdd();
      entity.Property(e => e.IntakeTitile)
          .HasMaxLength(100)
          .IsUnicode(false);
    });

    modelBuilder.Entity<Crminstitute>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("CRMInstitute");

      entity.Property(e => e.ApplicationFee).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Campus)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.FundsRequirementforVisa)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.InstituteAddress)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.InstituteCode)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.InstituteEmail)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.InstituteId).ValueGeneratedOnAdd();
      entity.Property(e => e.InstituteMobileNo)
          .HasMaxLength(20)
          .IsUnicode(false);
      entity.Property(e => e.InstitutePhoneNo)
          .HasMaxLength(20)
          .IsUnicode(false)
          .HasColumnName("InstitutePhoneNO");
      entity.Property(e => e.InstituteType)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.InstitutionLogo)
          .HasMaxLength(200)
          .IsUnicode(false);
      entity.Property(e => e.InstitutionProspectus)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.InstitutionStatusNotes)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.InstitutionalBenefits)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.LanguagesRequirement)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.MonthlyLivingCost).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PartTimeWorkDetails)
          .HasMaxLength(500)
          .IsUnicode(false);
      entity.Property(e => e.ScholarshipsPolicy)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.Website)
          .HasMaxLength(100)
          .IsUnicode(false);
    });

    modelBuilder.Entity<Crmmonth>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("CRMMonth");

      entity.Property(e => e.CrmmonthName)
          .HasMaxLength(10)
          .IsUnicode(false)
          .HasColumnName("CRMMonthName");
      entity.Property(e => e.MonthCode)
          .HasMaxLength(10)
          .IsUnicode(false);
      entity.Property(e => e.MonthId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Crmyear>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("CRMYear");

      entity.Property(e => e.YearCode)
          .HasMaxLength(10)
          .IsUnicode(false);
      entity.Property(e => e.YearId).ValueGeneratedOnAdd();
      entity.Property(e => e.YearName)
          .HasMaxLength(10)
          .IsUnicode(false);
    });


    modelBuilder.Entity<AboutUsLicense>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AboutUsLicenseId)
              .ValueGeneratedOnAdd()
              .HasColumnName("AboutUsLicenseID");
      entity.Property(e => e.CodeBaseVersion).HasMaxLength(50);
      entity.Property(e => e.LicenseFor).HasMaxLength(50);
      entity.Property(e => e.LicenseNumber).HasMaxLength(50);
      entity.Property(e => e.LicenseType).HasMaxLength(50);
      entity.Property(e => e.LocationLicense).HasMaxLength(50);
      entity.Property(e => e.ProductCode).HasMaxLength(50);
      entity.Property(e => e.Sbulicense)
              .HasMaxLength(50)
              .HasColumnName("SBULicense");
      entity.Property(e => e.ServerId)
              .HasMaxLength(50)
              .HasColumnName("ServerID");
      entity.Property(e => e.UserLicense).HasMaxLength(50);
    });

    modelBuilder.Entity<AbsentSmsRemarks>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AbsentSmsRemarksId).ValueGeneratedOnAdd();
      entity.Property(e => e.Remarks).HasMaxLength(200);
      entity.Property(e => e.RemarksCode).HasMaxLength(50);
      entity.Property(e => e.RemarksSpecification).HasMaxLength(250);
    });

    modelBuilder.Entity<AccessRestriction>(entity =>
    {
      entity.Property(e => e.AccessDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AccessRestriction05032023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AccessRestriction_05_03_2023");

      entity.Property(e => e.AccessDate).HasColumnType("datetime");
      entity.Property(e => e.AccessRestrictionId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Accesscontrol>(entity =>
    {
      entity.HasKey(e => e.AccessId);

      entity.ToTable("ACCESSCONTROL");

      entity.Property(e => e.AccessName).HasMaxLength(50);
    });

    modelBuilder.Entity<AccountHead>(entity =>
    {
      entity.Property(e => e.AccountHeadCode).HasMaxLength(50);
      entity.Property(e => e.AccountHeadName).HasMaxLength(250);
      entity.Property(e => e.AccountHeadType).HasComment("1= Balance Sheet , 2= Profit & Loss");
      entity.Property(e => e.IsPl).HasColumnName("IsPL");
      entity.Property(e => e.Level).HasColumnName("level");
      entity.Property(e => e.SubLgCode).HasMaxLength(50);
      entity.Property(e => e.TransectionType).HasComment("1=Debit Type,2=Credit Type");
    });

    modelBuilder.Entity<AccountHeadSubLedger>(entity =>
    {
      entity.HasKey(e => e.SubLedgerAccountId);

      entity.Property(e => e.IsActive).HasDefaultValue(true);
      entity.Property(e => e.LastCode)
              .HasMaxLength(10)
              .IsUnicode(false);
      entity.Property(e => e.SubLedgerCode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SubLedgerName)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Accumulationtype>(entity =>
    {
      entity.ToTable("ACCUMULATIONTYPE");

      entity.Property(e => e.Accumulationtypeid).HasColumnName("ACCUMULATIONTYPEID");
      entity.Property(e => e.Accumulationtypename)
              .HasMaxLength(100)
              .HasColumnName("ACCUMULATIONTYPENAME");
    });

    modelBuilder.Entity<AcrDetails>(entity =>
    {
      entity.HasKey(e => e.Acrid);

      entity.Property(e => e.Acrid).HasColumnName("ACRID");
      entity.Property(e => e.Actiondate)
              .HasColumnType("datetime")
              .HasColumnName("ACTIONDATE");
      entity.Property(e => e.AdminComments)
              .HasMaxLength(500)
              .HasColumnName("ADMIN_COMMENTS");
      entity.Property(e => e.AdminRecommenderId).HasColumnName("ADMIN_RECOMMENDER_ID");
      entity.Property(e => e.ApproverComments)
              .HasMaxLength(500)
              .HasColumnName("APPROVER_COMMENTS");
      entity.Property(e => e.ApproverId).HasColumnName("APPROVER_ID");
      entity.Property(e => e.ApproverOrSupervisorid).HasColumnName("APPROVER_OR_SUPERVISORID");
      entity.Property(e => e.Assessmentperiodform)
              .HasColumnType("datetime")
              .HasColumnName("ASSESSMENTPERIODFORM");
      entity.Property(e => e.Assessmentperiodto)
              .HasColumnType("datetime")
              .HasColumnName("ASSESSMENTPERIODTO");
      entity.Property(e => e.Attendance).HasColumnName("ATTENDANCE");
      entity.Property(e => e.Cooparationtoothers).HasColumnName("COOPARATIONTOOTHERS");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.IsAdminRecommended).HasColumnName("IS_ADMIN_RECOMMENDED");
      entity.Property(e => e.IsApprove).HasColumnName("IS_APPROVE");
      entity.Property(e => e.IsSupervise).HasColumnName("IS_SUPERVISE");
      entity.Property(e => e.Knowledgeachived)
              .HasMaxLength(2000)
              .IsUnicode(false)
              .HasColumnName("KNOWLEDGEACHIVED");
      entity.Property(e => e.Newknowledgegained).HasColumnName("NEWKNOWLEDGEGAINED");
      entity.Property(e => e.Nextyearvalueaddedforcompany)
              .HasMaxLength(2000)
              .IsUnicode(false)
              .HasColumnName("NEXTYEARVALUEADDEDFORCOMPANY");
      entity.Property(e => e.Physicalfitness).HasColumnName("PHYSICALFITNESS");
      entity.Property(e => e.Presentmajorworkres)
              .HasMaxLength(2000)
              .IsUnicode(false)
              .HasColumnName("PRESENTMAJORWORKRES");
      entity.Property(e => e.Projectdealing).HasColumnName("PROJECTDEALING");
      entity.Property(e => e.Properofficediscipline).HasColumnName("PROPEROFFICEDISCIPLINE");
      entity.Property(e => e.Qualityofwork).HasColumnName("QUALITYOFWORK");
      entity.Property(e => e.ReasonOfSendBack)
              .HasMaxLength(500)
              .HasColumnName("REASON_OF_SEND_BACK");
      entity.Property(e => e.Senderid).HasColumnName("SENDERID");
      entity.Property(e => e.Shortcomming)
              .HasMaxLength(2000)
              .IsUnicode(false)
              .HasColumnName("SHORTCOMMING");
      entity.Property(e => e.Statusid).HasColumnName("STATUSID");
      entity.Property(e => e.SuperviserId).HasColumnName("SUPERVISER_ID");
      entity.Property(e => e.SupervisorComments)
              .HasMaxLength(500)
              .HasColumnName("SUPERVISOR_COMMENTS");
      entity.Property(e => e.Supportresponse).HasColumnName("SUPPORTRESPONSE");
      entity.Property(e => e.Targetmetting).HasColumnName("TARGETMETTING");
      entity.Property(e => e.Typeofassessment).HasColumnName("TYPEOFASSESSMENT");
      entity.Property(e => e.Valueaddedbytheexecutive)
              .HasMaxLength(2000)
              .IsUnicode(false)
              .HasColumnName("VALUEADDEDBYTHEEXECUTIVE");
    });

    modelBuilder.Entity<AcrSalaryFixation>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ACR_SALARY_FIXATION");

      entity.Property(e => e.AcrId).HasColumnName("ACR_ID");
      entity.Property(e => e.BasicSalary)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("BASIC_SALARY");
      entity.Property(e => e.DesignationId).HasColumnName("DESIGNATION_ID");
      entity.Property(e => e.EffectiveDate)
              .HasColumnType("datetime")
              .HasColumnName("EFFECTIVE_DATE");
      entity.Property(e => e.GradeId).HasColumnName("GRADE_ID");
      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.NextReviewDate)
              .HasColumnType("datetime")
              .HasColumnName("NEXT_REVIEW_DATE");
      entity.Property(e => e.OldDesignationId)
              .HasDefaultValue(0)
              .HasColumnName("OLD_DESIGNATION_ID");
      entity.Property(e => e.OldGradeId)
              .HasDefaultValue(0)
              .HasColumnName("OLD_GRADE_ID");
    });

    modelBuilder.Entity<AcrSupAssessMarksDetails>(entity =>
    {
      entity.ToTable("ACR_SUP_ASSESS_MARKS_DETAILS");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.AcrId).HasColumnName("ACR_ID");
      entity.Property(e => e.KpiGradeId).HasColumnName("KPI_GRADE_ID");
      entity.Property(e => e.KpiId).HasColumnName("KPI_ID");
      entity.Property(e => e.LastUpdateDate)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime")
              .HasColumnName("LAST_UPDATE_DATE");
      entity.Property(e => e.SubKpiId).HasColumnName("SUB_KPI_ID");
    });

    modelBuilder.Entity<AcrSupRemarks>(entity =>
    {
      entity.ToTable("ACR_SUP_REMARKS");

      entity.Property(e => e.AcrSupRemarksId).HasColumnName("ACR_SUP_REMARKS_ID");
      entity.Property(e => e.Remarks)
              .HasMaxLength(100)
              .HasColumnName("REMARKS");
    });

    modelBuilder.Entity<AcrSupervisorAssessment>(entity =>
    {
      entity.ToTable("ACR_SUPERVISOR_ASSESSMENT");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.Acelerated).HasColumnName("ACELERATED");
      entity.Property(e => e.AcrId).HasColumnName("ACR_ID");
      entity.Property(e => e.FitForIncrement).HasColumnName("FIT_FOR_INCREMENT");
      entity.Property(e => e.GrandTotal)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("GRAND_TOTAL");
      entity.Property(e => e.GrandTotalObtain)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("GRAND_TOTAL_OBTAIN");
      entity.Property(e => e.IncrementAmount)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("INCREMENT_AMOUNT");
      entity.Property(e => e.LastUpdateDate)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime")
              .HasColumnName("LAST_UPDATE_DATE");
      entity.Property(e => e.Normal).HasColumnName("NORMAL");
      entity.Property(e => e.OverShortComming).HasColumnName("OVER_SHORT_COMMING");
      entity.Property(e => e.RemovalOrPromotion).HasColumnName("REMOVAL_OR_PROMOTION");
      entity.Property(e => e.ShortCummingPointOut).HasColumnName("SHORT_CUMMING_POINT_OUT");
      entity.Property(e => e.SuitableForAssignment).HasColumnName("SUITABLE_FOR_ASSIGNMENT");
      entity.Property(e => e.SummerizeRating)
              .HasMaxLength(500)
              .HasColumnName("SUMMERIZE_RATING");
      entity.Property(e => e.TrainingRequirement)
              .HasMaxLength(500)
              .HasColumnName("TRAINING_REQUIREMENT");
      entity.Property(e => e.ValueAddition).HasColumnName("VALUE_ADDITION");
      entity.Property(e => e.WillAddValueNextYear).HasColumnName("WILL_ADD_VALUE_NEXT_YEAR");
      entity.Property(e => e.WorkedUnderMeFromDate)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime")
              .HasColumnName("WORKED_UNDER_ME_FROM_DATE");
      entity.Property(e => e.WorkedUnderMeToDate)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime")
              .HasColumnName("WORKED_UNDER_ME_TO_DATE");
    });

    modelBuilder.Entity<AdditionalCtc>(entity =>
    {
      entity.HasKey(e => e.CtcId);

      entity.Property(e => e.CtcName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.CtcOperator).HasComment("1=Allowance,2=Deduction");
    });

    modelBuilder.Entity<AdditionalCtcUploadForSalary>(entity =>
    {
      entity.HasKey(e => e.AdditionalCtcUploadId);

      entity.Property(e => e.CtcAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AdditionalCtcUploadForSalaryTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CtcAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<AdvanceIncomeTax>(entity =>
    {
      entity.HasKey(e => e.AitId);

      entity.Property(e => e.AitAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AitDescription).HasMaxLength(500);
      entity.Property(e => e.Attachment).HasMaxLength(500);
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EntryDate).HasColumnType("datetime");
      entity.Property(e => e.InvestmentAmt).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AitFileAttachment>(entity =>
    {
      entity.HasKey(e => e.AttachFileId);

      entity.Property(e => e.AttachedDocument).HasMaxLength(100);
      entity.Property(e => e.Describtion).HasMaxLength(100);
      entity.Property(e => e.TitleOfDocument).HasMaxLength(100);
    });

    modelBuilder.Entity<AlternativeLeave>(entity =>
    {
      entity.HasKey(e => e.AlternativeDayId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AmcExtension>(entity =>
    {
      entity.HasKey(e => e.AmcExtId);

      entity.Property(e => e.AmcAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.AmcPerentage).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.ExtDate).HasColumnType("datetime");
      entity.Property(e => e.NewProjectValue).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Amcdetails>(entity =>
    {
      entity.HasKey(e => e.AmcInfoId);

      entity.ToTable("AMCDetails");

      entity.Property(e => e.AmcParcentage).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.AmcRef).HasMaxLength(100);
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.MonthlyAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.ProjectValue).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.QuaterlyAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.SignDate).HasColumnType("datetime");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.YearlyAmount).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<ApplicantDocumentPrint>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ApplicantDocumentId).ValueGeneratedOnAdd();
      entity.Property(e => e.ParamIdentity).HasMaxLength(150);
      entity.Property(e => e.ParamValue).HasMaxLength(100);
    });

    modelBuilder.Entity<ApplicantInformation>(entity =>
    {
      entity.HasKey(e => e.ApplicantId);

      entity.Property(e => e.ActivateFromPipeLineDate).HasColumnType("datetime");
      entity.Property(e => e.ActivatedDate).HasColumnType("datetime");
      entity.Property(e => e.ApplicantAddress).HasMaxLength(1000);
      entity.Property(e => e.ApplicantCode).HasMaxLength(50);
      entity.Property(e => e.ApplicantName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.ApplicantPhotoPath).HasMaxLength(100);
      entity.Property(e => e.ApplicantSignature).HasMaxLength(250);
      entity.Property(e => e.ApplicantSocialMedia).HasMaxLength(500);
      entity.Property(e => e.ApplicantSource).HasMaxLength(500);
      entity.Property(e => e.ApplicantSurname).HasMaxLength(100);
      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.AppliedPost).HasComment("Job Vacancy");
      entity.Property(e => e.AppointmentLetterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedByCeo).HasColumnName("ApprovedByCEO");
      entity.Property(e => e.ApprovedByMd).HasColumnName("ApprovedByMD");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDateByCeo)
              .HasColumnType("datetime")
              .HasColumnName("ApprovedDateByCEO");
      entity.Property(e => e.ApprovedDateByMd)
              .HasColumnType("datetime")
              .HasColumnName("ApprovedDateByMD");
      entity.Property(e => e.AttachmentCv)
              .HasMaxLength(500)
              .HasColumnName("AttachmentCV");
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.CallForSecondInterviewDate).HasColumnType("datetime");
      entity.Property(e => e.CallForThirdInterviewDate).HasColumnType("datetime");
      entity.Property(e => e.CriminalOffenceDescription).HasMaxLength(1000);
      entity.Property(e => e.CurrentCompany).HasMaxLength(400);
      entity.Property(e => e.CurrentDesignation).HasMaxLength(500);
      entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
      entity.Property(e => e.District).HasMaxLength(50);
      entity.Property(e => e.EducationMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EducationalQualification).HasMaxLength(400);
      entity.Property(e => e.ExpectedSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExperienceYear).HasMaxLength(50);
      entity.Property(e => e.FatherName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.FirstInterviewAttendDateAlter).HasColumnType("datetime");
      entity.Property(e => e.FirstInterviewCallDate).HasColumnType("datetime");
      entity.Property(e => e.FirstInterviewMarksAlter).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InsertDate).HasColumnType("datetime");
      entity.Property(e => e.Institute).HasMaxLength(300);
      entity.Property(e => e.InterViewDateFrom).HasColumnType("datetime");
      entity.Property(e => e.InterViewDateTo).HasColumnType("datetime");
      entity.Property(e => e.InterviewRemarks).HasMaxLength(250);
      entity.Property(e => e.InterviewVanue).HasMaxLength(250);
      entity.Property(e => e.IsInitialPipelined).HasColumnName("isInitialPipelined");
      entity.Property(e => e.IsInitialRejected).HasColumnName("isInitialRejected");
      entity.Property(e => e.IsInitialSelected).HasColumnName("isInitialSelected");
      entity.Property(e => e.IsPcfprovided).HasColumnName("IsPCFProvided");
      entity.Property(e => e.JoiningDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningLetterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningLetterGeneratedDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningLetterPath).HasMaxLength(250);
      entity.Property(e => e.LastCvUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.MobileNumber)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.MotherName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.NationalId)
              .HasMaxLength(100)
              .IsUnicode(false)
              .HasColumnName("NationalID");
      entity.Property(e => e.Nationality).HasMaxLength(500);
      entity.Property(e => e.OfferDate).HasColumnType("datetime");
      entity.Property(e => e.OfferExpireDate).HasColumnType("datetime");
      entity.Property(e => e.OfferLetterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OfferLetterSentDate).HasColumnType("datetime");
      entity.Property(e => e.OfferLitterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.PcfprovidedBy).HasColumnName("PCFProvidedBy");
      entity.Property(e => e.PcfprovidedDate)
              .HasColumnType("datetime")
              .HasColumnName("PCFProvidedDate");
      entity.Property(e => e.PersonalEmail)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.PipelinedDate).HasColumnType("datetime");
      entity.Property(e => e.Po)
              .HasMaxLength(50)
              .HasColumnName("PO");
      entity.Property(e => e.PortalApplicantId).HasMaxLength(250);
      entity.Property(e => e.PreffredDepartment).HasMaxLength(500);
      entity.Property(e => e.PresentCompanyExperienceYear).HasMaxLength(50);
      entity.Property(e => e.PresentSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentationDate).HasColumnType("datetime");
      entity.Property(e => e.PreviousRemarks).HasMaxLength(250);
      entity.Property(e => e.PrimaryShoerlistDate).HasColumnType("datetime");
      entity.Property(e => e.Qualification).HasMaxLength(500);
      entity.Property(e => e.RecommendedDate).HasColumnType("datetime");
      entity.Property(e => e.RecommendedDateBuHead).HasColumnType("datetime");
      entity.Property(e => e.RecommendedDateFunctionalHead).HasColumnType("datetime");
      entity.Property(e => e.RecommendedDateHrHead).HasColumnType("datetime");
      entity.Property(e => e.Reference1).HasMaxLength(200);
      entity.Property(e => e.Reference2).HasMaxLength(200);
      entity.Property(e => e.RejectedDate).HasColumnType("datetime");
      entity.Property(e => e.Relationship).HasMaxLength(250);
      entity.Property(e => e.Result).HasMaxLength(100);
      entity.Property(e => e.SecondInterivewVanue)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.SecondInterviewAttendDateAlter).HasColumnType("datetime");
      entity.Property(e => e.SecondInterviewCallDate).HasColumnType("datetime");
      entity.Property(e => e.SecondInterviewMarksAlter).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecondRelationshipName).HasMaxLength(1000);
      entity.Property(e => e.SelectedDate).HasColumnType("datetime");
      entity.Property(e => e.SelectionRemarks).HasMaxLength(250);
      entity.Property(e => e.SendForHrApprovalDate).HasColumnType("datetime");
      entity.Property(e => e.ShortlistedDate).HasColumnType("datetime");
      entity.Property(e => e.Thana).HasMaxLength(50);
      entity.Property(e => e.TrackingNumber).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.Village).HasMaxLength(50);
      entity.Property(e => e.WrittenMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.WrittenTestAttendDate).HasColumnType("datetime");
      entity.Property(e => e.WrittenTestCallDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ApplicantInformationHistory>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ApplicantInformation_History");

      entity.Property(e => e.ActivateFromPipeLineDate).HasColumnType("datetime");
      entity.Property(e => e.ActivatedDate).HasColumnType("datetime");
      entity.Property(e => e.ApplicantAddress).HasMaxLength(1000);
      entity.Property(e => e.ApplicantCode).HasMaxLength(50);
      entity.Property(e => e.ApplicantName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.ApplicantParmanentAddress).HasMaxLength(3000);
      entity.Property(e => e.ApplicantPhotoPath).HasMaxLength(100);
      entity.Property(e => e.ApplicantSource).HasMaxLength(500);
      entity.Property(e => e.ApplicantSurname).HasMaxLength(100);
      entity.Property(e => e.AppointmentLetterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AttachmentCv)
              .HasMaxLength(500)
              .HasColumnName("AttachmentCV");
      entity.Property(e => e.CtcPayslipFile).HasMaxLength(500);
      entity.Property(e => e.CurrentCompany).HasMaxLength(400);
      entity.Property(e => e.CurrentDesignation).HasMaxLength(500);
      entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
      entity.Property(e => e.District).HasMaxLength(50);
      entity.Property(e => e.EducationalQualification).HasMaxLength(500);
      entity.Property(e => e.ExpectedSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExperienceYear).HasMaxLength(50);
      entity.Property(e => e.FatherName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.FirstInterviewAttendDateAlter).HasColumnType("datetime");
      entity.Property(e => e.FirstInterviewCallDate).HasColumnType("datetime");
      entity.Property(e => e.FirstInterviewMarksAlter).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Institute).HasMaxLength(500);
      entity.Property(e => e.InterViewDateFrom).HasColumnType("datetime");
      entity.Property(e => e.InterViewDateTo).HasColumnType("datetime");
      entity.Property(e => e.IsPcfprovided).HasColumnName("IsPCFProvided");
      entity.Property(e => e.JoiningDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningLetterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.LastCvUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.MedicalDescription).HasMaxLength(250);
      entity.Property(e => e.MobileNumber)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.MotherName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.NameOfRelative).HasMaxLength(250);
      entity.Property(e => e.NationalId)
              .HasMaxLength(100)
              .IsUnicode(false)
              .HasColumnName("NationalID");
      entity.Property(e => e.OfferLetterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OfferLitterGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.PcfprovidedBy).HasColumnName("PCFProvidedBy");
      entity.Property(e => e.PcfprovidedDate)
              .HasColumnType("datetime")
              .HasColumnName("PCFProvidedDate");
      entity.Property(e => e.PersonalEmail)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.PipelinedDate).HasColumnType("datetime");
      entity.Property(e => e.Po)
              .HasMaxLength(50)
              .HasColumnName("PO");
      entity.Property(e => e.PreffredDepartment).HasMaxLength(500);
      entity.Property(e => e.PresentCompanyExperienceYear).HasMaxLength(50);
      entity.Property(e => e.PresentSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentationDate).HasColumnType("datetime");
      entity.Property(e => e.PrimaryShoerlistDate).HasColumnType("datetime");
      entity.Property(e => e.Qualification).HasMaxLength(500);
      entity.Property(e => e.RecommendedDate).HasColumnType("datetime");
      entity.Property(e => e.Reference1).HasMaxLength(500);
      entity.Property(e => e.Reference2).HasMaxLength(500);
      entity.Property(e => e.ReferenceType).HasMaxLength(500);
      entity.Property(e => e.RejectedDate).HasColumnType("datetime");
      entity.Property(e => e.Relationship).HasMaxLength(250);
      entity.Property(e => e.RelativeOrganization).HasMaxLength(250);
      entity.Property(e => e.RelativePosition).HasMaxLength(250);
      entity.Property(e => e.Result).HasMaxLength(100);
      entity.Property(e => e.SecondInterviewAttendDateAlter).HasColumnType("datetime");
      entity.Property(e => e.SecondInterviewCallDate).HasColumnType("datetime");
      entity.Property(e => e.SecondInterviewMarksAlter).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SelectedDate).HasColumnType("datetime");
      entity.Property(e => e.SendForHrApprovalDate).HasColumnType("datetime");
      entity.Property(e => e.ShortlistedDate).HasColumnType("datetime");
      entity.Property(e => e.Thana).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.Village).HasMaxLength(50);
      entity.Property(e => e.WrittenMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.WrittenTestAttendDate).HasColumnType("datetime");
      entity.Property(e => e.WrittenTestCallDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ApplicantOfferLetterInfo>(entity =>
    {
      entity.Property(e => e.AppointmentEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.AppointmentLetterCopyTo).HasMaxLength(250);
      entity.Property(e => e.AppointmentLetterIssueDate).HasColumnType("datetime");
      entity.Property(e => e.AppointmentLetterRefNo).HasMaxLength(250);
      entity.Property(e => e.BranchName).HasMaxLength(250);
      entity.Property(e => e.CompanyName).HasMaxLength(250);
      entity.Property(e => e.CostCenterName).HasMaxLength(250);
      entity.Property(e => e.DepartmentName).HasMaxLength(250);
      entity.Property(e => e.DesignationName).HasMaxLength(250);
      entity.Property(e => e.JoiningDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningLetterEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningLetterRefNo)
              .HasMaxLength(250)
              .IsFixedLength();
      entity.Property(e => e.JoiningLetterReportingLineName).HasMaxLength(150);
      entity.Property(e => e.JoiningLetterSignatoryName).HasMaxLength(150);
      entity.Property(e => e.OfferCopyTo).HasMaxLength(250);
      entity.Property(e => e.OfferDate).HasColumnType("datetime");
      entity.Property(e => e.OfferExpireDate).HasColumnType("datetime");
      entity.Property(e => e.OfferRefNo).HasMaxLength(250);
      entity.Property(e => e.OfferSignatory).HasMaxLength(250);
      entity.Property(e => e.ProbationarySalary).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.ReportingTo).HasMaxLength(250);
    });

    modelBuilder.Entity<ApplicantPayroll>(entity =>
    {
      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.Status).HasDefaultValue(1);
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ApplicantPayrollDetails>(entity =>
    {
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ApplicantSource>(entity =>
    {
      entity.Property(e => e.ApplicantSourceName).HasMaxLength(500);
    });

    modelBuilder.Entity<ApplicationAutoProcessLog>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AppAutoProcessLogId).ValueGeneratedOnAdd();
      entity.Property(e => e.ProcessDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalBasicIncrement>(entity =>
    {
      entity.HasKey(e => e.BasicIncrementId);

      entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalDetails>(entity =>
    {
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalDetailsForFf>(entity =>
    {
      entity.HasKey(e => e.DetailsId);

      entity.ToTable("AppraisalDetailsForFF");

      entity.Property(e => e.AchievingMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MarkWisePoint).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AppraisalDetailsLog>(entity =>
    {
      entity.HasKey(e => e.AppraisalDetailsId);

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalGradeSettings>(entity =>
    {
      entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalGradeWiseAmountMaxRange>(entity =>
    {
      entity.HasKey(e => e.AppraisalGradeWiseMaxAmountId);

      entity.Property(e => e.MaxAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AppraisalGradeWiseWeight>(entity =>
    {
      entity.HasKey(e => e.WeightId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalImportance>(entity =>
    {
      entity.HasKey(e => e.ImportanceId);
    });

    modelBuilder.Entity<AppraisalIncrementDetails>(entity =>
    {
      entity.HasKey(e => e.AppraisalIncrementHistoryId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.GradeWiseAmountIncrease).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Recommandation).HasMaxLength(150);
      entity.Property(e => e.SpecialAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SystemGeneratedGradeWiseAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SystemGeneratedRecommandation).HasMaxLength(150);
    });

    modelBuilder.Entity<AppraisalMaster>(entity =>
    {
      entity.Property(e => e.Comments).HasMaxLength(550);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.RatingStatus).HasMaxLength(50);
      entity.Property(e => e.TotalRatingValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalMasterLog>(entity =>
    {
      entity.Property(e => e.Comments).HasMaxLength(550);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.RatingStatus).HasMaxLength(50);
      entity.Property(e => e.TotalRatingValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalOpeningMonthWise>(entity =>
    {
      entity.HasKey(e => e.AppraisalOpeningId);

      entity.ToTable("AppraisalOpening_MonthWise");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.ForFfsEmployee).HasColumnName("ForFFsEmployee");
      entity.Property(e => e.FromDate).HasColumnType("datetime");
      entity.Property(e => e.ToDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalRattingRange>(entity =>
    {
      entity.HasKey(e => e.AppraisalRatingRangeId);

      entity.Property(e => e.FromRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RatingPoint).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ToRate).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AppraisalRattingSlab>(entity =>
    {
      entity.HasKey(e => e.RattingSlabId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.FromRattingValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RattingStatus).HasMaxLength(150);
      entity.Property(e => e.ToRattingValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalRecommendation>(entity =>
    {
      entity.Property(e => e.EmailSentDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AppraisalTempData>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CompanyName).HasMaxLength(150);
      entity.Property(e => e.DepartmentName).HasMaxLength(150);
      entity.Property(e => e.Description).HasMaxLength(2050);
      entity.Property(e => e.DesignationName).HasMaxLength(150);
    });

    modelBuilder.Entity<AppraisalTempUploadTable>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AreaCode).HasMaxLength(150);
      entity.Property(e => e.EmployeeId).HasMaxLength(150);
      entity.Property(e => e.MarkAchived).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AppraisalYearEndProcess>(entity =>
    {
      entity.Property(e => e.Comments).HasMaxLength(550);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.RatingStatus).HasMaxLength(50);
      entity.Property(e => e.TotalRatingValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.WfstateId).HasColumnName("WFStateId");
    });

    modelBuilder.Entity<ApprisalImplementPolicy>(entity =>
    {
      entity.HasKey(e => e.ApproverRatingId);

      entity.Property(e => e.NumberOfIncrement)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.NumberOfIncrementSpecial)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<ApproverDetails>(entity =>
    {
      entity.HasKey(e => e.RemarksId).HasName("PK_Remarks");

      entity.HasIndex(e => new { e.MenuId, e.ApproverType, e.ModuleId }, "NonClusteredApproverDetailsIndex");

      entity.HasIndex(e => new { e.MenuId, e.ApplicationId, e.ApproverType, e.ModuleId }, "indx_menuAppId");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
    });

    modelBuilder.Entity<ApproverDetails14012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ApproverDetails_14_01_2024");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.RemarksId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<ApproverDetails27122022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ApproverDetails_27_12_2022");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.RemarksId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<ApproverDetailsRec03012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ApproverDetails_Rec_03_01_2024");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.RemarksId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<ApproverHistory>(entity =>
    {
      entity.HasKey(e => e.AssignApproverId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DeleteDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ApproverOrder>(entity =>
    {
      entity.Property(e => e.ApproverOrderId).ValueGeneratedNever();
      entity.Property(e => e.OrderTitle)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<ApproverType>(entity =>
    {
      entity.Property(e => e.ApproverTypeId).ValueGeneratedNever();
      entity.Property(e => e.ApproverTypeName)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<ApproverTypeToGroupMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ApproverTypeMapId).ValueGeneratedOnAdd();

      entity.HasOne(d => d.ApproverType).WithMany()
              .HasForeignKey(d => d.ApproverTypeId)
              .HasConstraintName("FK_ApproverTypeToGroupMapping_ApproverType");
    });

    modelBuilder.Entity<AppsTokenInfo>(entity =>
    {
      entity.Property(e => e.AppsUserId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
      entity.Property(e => e.IssueDate).HasColumnType("datetime");
      entity.Property(e => e.TokenNumber)
              .HasMaxLength(200)
              .IsUnicode(false);
    });

    modelBuilder.Entity<AppsTransactionLog>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AppsUserId).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(100);
      entity.Property(e => e.Remarks).HasMaxLength(1000);
      entity.Property(e => e.Request).HasMaxLength(200);
      entity.Property(e => e.Response).HasMaxLength(2000);
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionLogId).ValueGeneratedOnAdd();
      entity.Property(e => e.TransactionType).HasMaxLength(100);
    });

    modelBuilder.Entity<AssemblyInfo>(entity =>
    {
      entity.Property(e => e.AssemblyInfoId)
              .ValueGeneratedNever()
              .HasComment("");
      entity.Property(e => e.AssemblyCompany)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.AssemblyCopyright)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.AssemblyDescription)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.AssemblyProduct)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.AssemblyTitle)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.AssemblyVersion)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.CvBankPath).HasMaxLength(250);
      entity.Property(e => e.IsAttendanceByLogin).HasComment("false=Attedance by login inactive feature");
      entity.Property(e => e.PoweredBy)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.PoweredByUrl)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.ProductBanner)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.ProductStyleSheet)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Assessmenttype>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ASSESSMENTTYPE");

      entity.Property(e => e.Assessmenttype1)
              .HasMaxLength(100)
              .HasColumnName("ASSESSMENTTYPE");
      entity.Property(e => e.Assessmenttypeid).HasColumnName("ASSESSMENTTYPEID");
    });

    modelBuilder.Entity<Asset>(entity =>
    {
      entity.ToTable("ASSET");

      entity.Property(e => e.Assetid).HasColumnName("ASSETID");
      entity.Property(e => e.Assetdescription)
              .HasMaxLength(2000)
              .HasColumnName("ASSETDESCRIPTION");
      entity.Property(e => e.Assetidentificationid).HasColumnName("ASSETIDENTIFICATIONID");
      entity.Property(e => e.Assetname)
              .HasMaxLength(100)
              .HasColumnName("ASSETNAME");
      entity.Property(e => e.Assetstatus)
              .HasMaxLength(100)
              .HasColumnName("ASSETSTATUS");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Isreturn).HasColumnName("ISRETURN");
      entity.Property(e => e.Issuedate)
              .HasColumnType("datetime")
              .HasColumnName("ISSUEDATE");
      entity.Property(e => e.Returndate)
              .HasColumnType("datetime")
              .HasColumnName("RETURNDATE");
    });

    modelBuilder.Entity<AssetAmortization>(entity =>
    {
      entity.Property(e => e.AssetAmortizationDate).HasColumnType("datetime");
      entity.Property(e => e.SaleValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AssetCategory>(entity =>
    {
      entity.Property(e => e.AssetCategoryCode)
              .HasMaxLength(30)
              .IsUnicode(false);
      entity.Property(e => e.AssetCategoryName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<AssetIdentification>(entity =>
    {
      entity.Property(e => e.AssetIdentificationBarCode)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.AssetIdentificationCode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.AssetIdentificationName)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<AssetLocation>(entity =>
    {
      entity.Property(e => e.RelocationDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AssetProcurement>(entity =>
    {
      entity.Property(e => e.AssetWarrantyPeriod)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.CountryOrigin).HasMaxLength(500);
      entity.Property(e => e.ManufacturingYear).HasMaxLength(50);
      entity.Property(e => e.ProcurementValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProductModel).HasMaxLength(500);
      entity.Property(e => e.ProductName).HasMaxLength(500);
      entity.Property(e => e.ProductSlNo).HasMaxLength(500);
      entity.Property(e => e.SupplierAddress)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.VendorName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<AssetReturnDetails>(entity =>
    {
      entity.ToTable("ASSET_RETURN_DETAILS");

      entity.Property(e => e.AssetReturnDetailsId).HasColumnName("ASSET_RETURN_DETAILS_ID");
      entity.Property(e => e.Assetid).HasColumnName("ASSETID");
      entity.Property(e => e.Disposual)
              .HasMaxLength(100)
              .HasColumnName("DISPOSUAL");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Place)
              .HasMaxLength(50)
              .HasColumnName("PLACE");
      entity.Property(e => e.ReceiverId).HasColumnName("RECEIVER_ID");
      entity.Property(e => e.ReceivingGoodsConditionId)
              .HasMaxLength(100)
              .HasColumnName("RECEIVING_GOODS_CONDITION_ID");
      entity.Property(e => e.Remarks)
              .HasMaxLength(100)
              .HasColumnName("REMARKS");
      entity.Property(e => e.ReturnDate)
              .HasColumnType("datetime")
              .HasColumnName("RETURN_DATE");
    });

    modelBuilder.Entity<AssetStatus>(entity =>
    {
      entity.Property(e => e.AssetStatusName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<AssetValuation>(entity =>
    {
      entity.Property(e => e.ValuationPercentage).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AssignApprover>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.ModuleId, e.Type, e.SortOrder }, "IX_AssignApprover");

      entity.HasIndex(e => new { e.HrRecordId, e.ModuleId, e.SortOrder, e.Type }, "IX_DuplicateApprover").IsUnique();

      entity.HasIndex(e => new { e.ApproverId, e.ModuleId, e.Type }, "NonClusteredAssignApproverIndex");

      entity.Property(e => e.AssignApproverId).ValueGeneratedOnAdd();
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AssignApprover06022024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AssignApprover_06_02_2024");

      entity.Property(e => e.AssignApproverId).ValueGeneratedOnAdd();
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AssignApprover060724>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AssignApprover_06_07_24");

      entity.Property(e => e.AssignApproverId).ValueGeneratedOnAdd();
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AssignApprover23092023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AssignApprover_23_09_2023");

      entity.Property(e => e.AssignApproverId).ValueGeneratedOnAdd();
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AssignApproverRec03012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AssignApprover_Rec_03_01_2024");

      entity.Property(e => e.AssignApproverId).ValueGeneratedOnAdd();
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AssignExceptionEmployee>(entity =>
    {
      entity.HasKey(e => e.ExceptionRecId);
    });

    modelBuilder.Entity<AttachFileForPo>(entity =>
    {
      entity.HasKey(e => e.AttachFileId).HasName("PK_AttachFileForCusCare");

      entity.ToTable("AttachFileForPO");

      entity.Property(e => e.AttachedDocument).HasMaxLength(100);
      entity.Property(e => e.Describtion).HasMaxLength(100);
      entity.Property(e => e.TitleOfDocument).HasMaxLength(100);
    });

    modelBuilder.Entity<AttendanceAdjustment>(entity =>
    {
      entity.HasKey(e => e.AdjustmentRequestId);

      entity.HasIndex(e => e.StateId, "NonClusteredAttendanceAdjustmentIndex");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.AttendanceDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Reason).HasMaxLength(500);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AttendanceFlag>(entity =>
    {
      entity.Property(e => e.Code)
              .HasMaxLength(10)
              .IsUnicode(false);
      entity.Property(e => e.Description)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.KeyValue)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<AttendanceLog>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable(tb =>
              {
              tb.HasTrigger("AfterDeleteLateAttLog");
              tb.HasTrigger("AfterInsertLateAttLog");
              tb.HasTrigger("tr_Update_AttLog_IsAttendanceClearOut");
            });

      entity.HasIndex(e => new { e.UserId, e.AttendanceDate, e.IsAttendanceClearOut, e.IsLate, e.DefalterType }, "AttendanceLogNONCLUSTEREDIndex");

      entity.HasIndex(e => new { e.UserId, e.AttendanceDate }, "IX_AttendanceLog_Duplicate").IsUnique();

      entity.HasIndex(e => e.AttendanceId, "Ix_AttendanceLog_AttendanceId");

      entity.HasIndex(e => new { e.ShiftId, e.IsLate }, "NonClusteredAttendanceLogIndex");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLog14092023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_14_09_2023");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLog21800221>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_21800221");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogArchive2017>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_Archive_2017");

      entity.Property(e => e.AttendanceDate).HasColumnType("smalldatetime");
      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogArchive2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_Archive_2018");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogArchive2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_Archive_2019");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_Archive_2020");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_Archive_2021");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_Archive_2022");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceLog_Archive_2023");

      entity.Property(e => e.AttendanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogMigration>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AttendanceDate).HasColumnType("smalldatetime");
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceLogReprocess>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AttendanceDate).HasColumnType("smalldatetime");
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastPunch).HasMaxLength(50);
      entity.Property(e => e.LateReason).HasMaxLength(500);
      entity.Property(e => e.LoginTime).HasMaxLength(50);
      entity.Property(e => e.LogoutTime).HasMaxLength(50);
      entity.Property(e => e.ShiftIntime).HasMaxLength(50);
      entity.Property(e => e.ShiftOutTime).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendanceMonthEnd>(entity =>
    {
      entity.HasKey(e => e.AttendanceMonthEndId).HasName("PK_AttendanceMonthEndProcess");

      entity.HasIndex(e => new { e.HrRecordId, e.AttendanceMonth, e.Status, e.CompanyId }, "NonClasterAttendanceMonthEndIndex");

      entity.HasIndex(e => new { e.AttendanceMonth, e.Status }, "NonClastereAttendanceMonthEndIndex");

      entity.Property(e => e.AbsentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AcumulatedDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CutOfDate).HasColumnType("datetime");
      entity.Property(e => e.DayOffHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnWeekend).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FinalDeductionDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FirstLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.LateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPayBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OnsiteDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeHours).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateDays).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AttendanceMonthEndApprovalNotification>(entity =>
    {
      entity.HasKey(e => e.AttMonthEndAppNotifyId);

      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<AttendanceMonthEndApprovalNotification20231105>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceMonthEndApprovalNotification_2023_11_05");

      entity.Property(e => e.AttMonthEndAppNotifyId).ValueGeneratedOnAdd();
      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<AttendanceMonthEndArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceMonthEnd_Archive_2020");

      entity.Property(e => e.AbsentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AcumulatedDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonthEndId).ValueGeneratedOnAdd();
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CutOfDate).HasColumnType("datetime");
      entity.Property(e => e.DayOffHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnWeekend).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FinalDeductionDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FirstLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.LateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPayBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OnsiteDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeHours).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateDays).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AttendanceMonthEndArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceMonthEnd_Archive_2021");

      entity.Property(e => e.AbsentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AcumulatedDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonthEndId).ValueGeneratedOnAdd();
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CutOfDate).HasColumnType("datetime");
      entity.Property(e => e.DayOffHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnWeekend).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FinalDeductionDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FirstLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.LateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPayBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OnsiteDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeHours).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateDays).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AttendanceMonthEndArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceMonthEnd_Archive_2022");

      entity.Property(e => e.AbsentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AcumulatedDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonthEndId).ValueGeneratedOnAdd();
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CutOfDate).HasColumnType("datetime");
      entity.Property(e => e.DayOffHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnWeekend).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FinalDeductionDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FirstLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.LateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPayBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OnsiteDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeHours).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateDays).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AttendanceMonthEndArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AttendanceMonthEnd_Archive_2023");

      entity.Property(e => e.AbsentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AcumulatedDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.AttendanceMonthEndId).ValueGeneratedOnAdd();
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CutOfDate).HasColumnType("datetime");
      entity.Property(e => e.DayOffHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnHoliday).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DutyHourOnWeekend).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FinalDeductionDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FirstLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.LateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveWithoutPayBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OnsiteDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeHours).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateDays).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<AttendanceMonthEndHeldUpWithdrawn>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AttMonEndHeldUpWithdrawId).ValueGeneratedOnAdd();
      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<AttendanceStatus>(entity =>
    {
      entity.HasKey(e => e.AttendanceStatusSettingsId);

      entity.Property(e => e.NewAbsentText)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.NewAvailableText)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.NewDayOffText)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Attendancelogtemp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ATTENDANCELOGTEMP");

      entity.Property(e => e.Attendancedate)
              .HasColumnType("datetime")
              .HasColumnName("ATTENDANCEDATE");
      entity.Property(e => e.Isattendanceclearout).HasColumnName("ISATTENDANCECLEAROUT");
      entity.Property(e => e.Isholiday).HasColumnName("ISHOLIDAY");
      entity.Property(e => e.Islate).HasColumnName("ISLATE");
      entity.Property(e => e.Latereason)
              .HasMaxLength(2000)
              .HasColumnName("LATEREASON");
      entity.Property(e => e.Logintime)
              .HasMaxLength(50)
              .HasColumnName("LOGINTIME");
      entity.Property(e => e.Logouttime)
              .HasMaxLength(50)
              .HasColumnName("LOGOUTTIME");
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.Userid).HasColumnName("USERID");
    });

    modelBuilder.Entity<AttendenceCode>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AttendenceCode1)
              .HasMaxLength(50)
              .HasColumnName("AttendenceCode");
      entity.Property(e => e.AttendenceCodeId).ValueGeneratedOnAdd();
      entity.Property(e => e.AttendenceName).HasMaxLength(50);
    });

    modelBuilder.Entity<AttendenceRemarksSettings>(entity =>
    {
      entity.HasKey(e => e.AttendenceRemarksId).HasName("PK_AttendenceCode");

      entity.Property(e => e.RemarksCode).HasMaxLength(50);
      entity.Property(e => e.RemarksName).HasMaxLength(50);
    });

    modelBuilder.Entity<AuditLog>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ActionName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.AuditDate).HasColumnType("datetime");
      entity.Property(e => e.AuditId).ValueGeneratedOnAdd();
      entity.Property(e => e.AuditStatus)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.BrowserInfo)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.ClientIp)
              .HasMaxLength(50)
              .HasColumnName("ClientIP");
      entity.Property(e => e.ClientUser).HasMaxLength(500);
      entity.Property(e => e.ControllerName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.DomainName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.ExceptionLog).IsUnicode(false);
      entity.Property(e => e.MacAddress).HasMaxLength(500);
      entity.Property(e => e.ReferrerUrl)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.RequestedParams).IsUnicode(false);
      entity.Property(e => e.RequestedUrl).IsUnicode(false);
      entity.Property(e => e.TableName)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<AuditTrail>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("AUDIT_TRAIL");

      entity.Property(e => e.ActionDate)
              .HasColumnType("datetime")
              .HasColumnName("ACTION_DATE");
      entity.Property(e => e.AuditDescription).HasColumnName("AUDIT_DESCRIPTION");
      entity.Property(e => e.AuditId)
              .ValueGeneratedOnAdd()
              .HasColumnName("AUDIT_ID");
      entity.Property(e => e.AuditStatus)
              .HasMaxLength(50)
              .HasColumnName("Audit_Status");
      entity.Property(e => e.AuditType)
              .HasMaxLength(500)
              .HasColumnName("AUDIT_TYPE");
      entity.Property(e => e.ClientIp)
              .HasMaxLength(50)
              .HasColumnName("CLIENT_IP");
      entity.Property(e => e.ClientUser)
              .HasMaxLength(500)
              .HasColumnName("CLIENT_USER");
      entity.Property(e => e.RequestedUrl).HasColumnName("Requested_Url");
      entity.Property(e => e.Shortdescription).HasColumnName("SHORTDESCRIPTION");
      entity.Property(e => e.UserId).HasColumnName("USER_ID");
    });

    modelBuilder.Entity<AuditType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AuditType1)
              .HasMaxLength(250)
              .IsUnicode(false)
              .HasColumnName("AuditType");
    });

    modelBuilder.Entity<BalanceSheet>(entity =>
    {
      entity.Property(e => e.AccountHeadCode).HasMaxLength(50);
      entity.Property(e => e.AccountHeadName).HasMaxLength(500);
      entity.Property(e => e.CurrentYearAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PrevYearAmt).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<Bank>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.BankCode).HasMaxLength(50);
      entity.Property(e => e.BankId).ValueGeneratedOnAdd();
      entity.Property(e => e.BankName).HasMaxLength(150);
    });

    modelBuilder.Entity<BankAccountType>(entity =>
    {
      entity.HasKey(e => e.AccountTypeId).HasName("PK_AccountType");

      entity.Property(e => e.AccountTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<BankBranch>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Address).HasMaxLength(500);
      entity.Property(e => e.BranchCode).HasMaxLength(50);
      entity.Property(e => e.BranchId).ValueGeneratedOnAdd();
      entity.Property(e => e.BranchName).HasMaxLength(150);
    });

    modelBuilder.Entity<BdgBasicIncrement>(entity =>
    {
      entity.HasKey(e => e.BasicIncrementId);

      entity.ToTable("BDG_BasicIncrement");

      entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgBudgetGradeSettings>(entity =>
    {
      entity.HasKey(e => e.BudgetGradeSettingsId);

      entity.ToTable("BDG_BudgetGradeSettings");

      entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgBudgetYearCompanyMap>(entity =>
    {
      entity.HasKey(e => e.BudgetYearCompanyMapId).HasName("PK_BDG_BudgetFiscalYearMap");

      entity.ToTable("BDG_BudgetYearCompanyMap");
    });

    modelBuilder.Entity<BdgBudgetYearConfig>(entity =>
    {
      entity.HasKey(e => e.BudgetYearSettingsId);

      entity.ToTable("BDG_BudgetYearConfig");

      entity.Property(e => e.BudgetStartDate).HasColumnType("datetime");
      entity.Property(e => e.BudgetYearName)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.MaximumAllownce).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.MinimumAllownce).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalution>(entity =>
    {
      entity.HasKey(e => e.EmpEvaluationId);

      entity.ToTable("BDG_EmpEvalution");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsReviewed)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RequestBasicAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionAppprover>(entity =>
    {
      entity.HasKey(e => e.EmpEvalutionAppproverId);

      entity.ToTable("BDG_EmpEvalutionAppprover");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SpecialAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionAppproverDraft>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BDG_EmpEvalutionAppproverDraft");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EmpEvalutionAppproverDraftId).ValueGeneratedOnAdd();
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SpecialAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionAppproverHistory>(entity =>
    {
      entity.HasKey(e => e.EmpEvalutionAppproverHistoryId);

      entity.ToTable("BDG_EmpEvalutionAppproverHistory");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SpecialAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionDetails>(entity =>
    {
      entity.ToTable("BDG_EmpEvalutionDetails");

      entity.Property(e => e.BdgEmpEvalutionDetailsId).HasColumnName("BDG_EmpEvalutionDetailsId");
      entity.Property(e => e.IncrementedValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncrimentPersentage).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewCtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionDetailsLog>(entity =>
    {
      entity.ToTable("BDG_EmpEvalutionDetailsLog");

      entity.Property(e => e.BdgEmpEvalutionDetailsLogId).HasColumnName("BDG_EmpEvalutionDetailsLogId");
      entity.Property(e => e.BdgEmpEvalutionDetailsId).HasColumnName("BDG_EmpEvalutionDetailsId");
      entity.Property(e => e.IncrementedValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncrimentPersentage).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewCtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionFinal>(entity =>
    {
      entity.HasKey(e => e.EmpEvalutionFinalId);

      entity.ToTable("BDG_EmpEvalutionFinal");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.CustomBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SpecialAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionFinalDraft>(entity =>
    {
      entity.HasKey(e => e.EmpEvalutionFinalDraftId);

      entity.ToTable("BDG_EmpEvalutionFinalDraft");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.CustomBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SpecialAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionHalfYearly>(entity =>
    {
      entity.HasKey(e => e.EmpEvaluationId);

      entity.ToTable("BDG_EmpEvalutionHalfYearly");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsReviewed)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionHalfYearlyDetails>(entity =>
    {
      entity.HasKey(e => e.BdgEmpEvalutionDetailsId);

      entity.ToTable("BDG_EmpEvalutionHalfYearlyDetails");

      entity.Property(e => e.BdgEmpEvalutionDetailsId).HasColumnName("BDG_EmpEvalutionDetailsId");
      entity.Property(e => e.IncrementedValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewCtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PresentValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionHalfYearlyPayroll>(entity =>
    {
      entity.HasKey(e => e.BdgEmpEvalutionPayrollId);

      entity.ToTable("BDG_EmpEvalutionHalfYearlyPayroll");

      entity.Property(e => e.BdgEmpEvalutionPayrollId).HasColumnName("BDG_EmpEvalutionPayrollId");
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentGradeBenifit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentGth).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressGradeBenifit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressGth).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewGradeBenifit).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.NewGth).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionPayroll>(entity =>
    {
      entity.ToTable("BDG_EmpEvalutionPayroll");

      entity.Property(e => e.BdgEmpEvalutionPayrollId).HasColumnName("BDG_EmpEvalutionPayrollId");
      entity.Property(e => e.Comments).HasMaxLength(550);
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentGradeBenifit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentGth).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressGradeBenifit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressGth).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewGradeBenifit).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.NewGth).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionPayrollLog>(entity =>
    {
      entity.ToTable("BDG_EmpEvalutionPayrollLog");

      entity.Property(e => e.BdgEmpEvalutionPayrollLogId).HasColumnName("BDG_EmpEvalutionPayrollLogId");
      entity.Property(e => e.BdgEmpEvalutionPayrollId).HasColumnName("BDG_EmpEvalutionPayrollId");
      entity.Property(e => e.Comments).HasMaxLength(550);
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentGradeBenifit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentGth).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressGradeBenifit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncressGth).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewGradeBenifit).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.NewGth).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmpEvalutionRecommender>(entity =>
    {
      entity.HasKey(e => e.EmpEvalutionRecommenderId);

      entity.ToTable("BDG_EmpEvalutionRecommender");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionRecommenderDraft>(entity =>
    {
      entity.HasKey(e => e.EmpEvalutionRecommenderDraftId);

      entity.ToTable("BDG_EmpEvalutionRecommenderDraft");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionRecommenderHistory>(entity =>
    {
      entity.ToTable("BDG_EmpEvalutionRecommenderHistory");

      entity.Property(e => e.BdgEmpEvalutionRecommenderHistoryId).HasColumnName("BDG_EmpEvalutionRecommenderHistoryId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BdgEmpEvalutionTmp>(entity =>
    {
      entity.ToTable("BDG_EmpEvalutionTmp");

      entity.Property(e => e.BdgEmpEvalutionTmpId).HasColumnName("BDG_EmpEvalutionTmpId");
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.Kpi)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.OtherAllowance).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgEmployeeForPerformenceByYear>(entity =>
    {
      entity.HasKey(e => e.EmployeeForPerformenceByYearId);

      entity.ToTable("BDG_EmployeeForPerformenceByYear");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.YearId).HasColumnName("yearId");
    });

    modelBuilder.Entity<BdgEvaluationHistory>(entity =>
    {
      entity.ToTable("BDG_EvaluationHistory");

      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RequestBasicAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BdgFieldForceYearlyData>(entity =>
    {
      entity.HasKey(e => e.FieldForceYearlyDataId);

      entity.ToTable("BDG_FieldForceYearlyData");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.GrowthNew).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.GrowthOld).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.SalesAchieved).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.Ytdrx)
              .HasColumnType("numeric(18, 2)")
              .HasColumnName("YTDRX");
    });

    modelBuilder.Entity<BdgFieldForceYearlyDataTemp>(entity =>
    {
      entity.HasKey(e => e.FieldForceYearlyDataId);

      entity.ToTable("BDG_FieldForceYearlyDataTemp");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.GrowthNew).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.GrowthOld).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.SalesAchieved).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.Ytdrx)
              .HasColumnType("numeric(18, 2)")
              .HasColumnName("YTDRX");
    });

    modelBuilder.Entity<BdgHalfYearlyKpiBehaviour>(entity =>
    {
      entity.HasKey(e => e.HalfYearlyKpiBehaviourId).HasName("PK_BGD_HalfYearlyKpiBehaviour");

      entity.ToTable("BDG_HalfYearlyKpiBehaviour");

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Ot)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OT");
    });

    modelBuilder.Entity<BdgHalfYearlyKpiBehaviourGradeMap>(entity =>
    {
      entity.ToTable("BDG_HalfYearlyKpiBehaviourGradeMap");

      entity.Property(e => e.BdgHalfYearlyKpiBehaviourGradeMapId).HasColumnName("BDG_HalfYearlyKpiBehaviourGradeMapId");
    });

    modelBuilder.Entity<BdgKpiConfig>(entity =>
    {
      entity.HasKey(e => e.KpiConfigId);

      entity.ToTable("BDG_KpiConfig");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.KpiConfigCode).HasMaxLength(250);
      entity.Property(e => e.KpiConfigName).HasMaxLength(250);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BfbilBulkLeavePosting03052023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BFBIL_Bulk_Leave_Posting_03_05_2023");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);
    });

    modelBuilder.Entity<BfbilEmpAcc10042023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BFBIL_Emp_Acc_10_04_2023");

      entity.Property(e => e.AccNo).HasMaxLength(50);
      entity.Property(e => e.Branch).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Name).HasMaxLength(50);
    });

    modelBuilder.Entity<BgCost>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BG_Cost");

      entity.Property(e => e.Costcenter).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<BgPmsCompnayDepartmentMapping>(entity =>
    {
      entity.HasKey(e => e.CompanyDepartmentMappingId);

      entity.ToTable("BG_PMS_CompnayDepartmentMapping");
    });

    modelBuilder.Entity<BgRecCompnayDepartmentMapping>(entity =>
    {
      entity.HasKey(e => e.CompanyDepartmentMappingId);

      entity.ToTable("BG_REC_CompnayDepartmentMapping");

      entity.Property(e => e.Date).HasColumnName("date");
    });

    modelBuilder.Entity<BgRecEmployeeWiseCompetencyMapping>(entity =>
    {
      entity.HasKey(e => e.CompetencyMappingId);

      entity.ToTable("BG_Rec_EmployeeWiseCompetencyMapping");
    });

    modelBuilder.Entity<BgdManpowerReqGradeMap>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BGD_ManpowerReqGradeMap");

      entity.Property(e => e.ManpowerGradeMapId).ValueGeneratedOnAdd();
      entity.Property(e => e.Remarks).HasMaxLength(250);
    });

    modelBuilder.Entity<BgdManpowerRequisition>(entity =>
    {
      entity.HasKey(e => e.ManpowerReqId);

      entity.ToTable("BGD_ManpowerRequisition");

      entity.Property(e => e.Attachment).HasMaxLength(1500);
      entity.Property(e => e.DeletedDate).HasColumnType("datetime");
      entity.Property(e => e.SaveDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Bihq2SecALv04092022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BIHQ_2_Sec_A_LV_04_09_2022");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);
      entity.Property(e => e.Name).HasMaxLength(50);
    });

    modelBuilder.Entity<Bihq2SecALv07052022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BIHQ_2_Sec_A_LV_07_05_2022");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<BmfNonMgntHouseRent10072024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BMF_NON_MGNT_HOUSE_RENT_10_07_2024");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.HouseRent).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<BmfplBulkLeavePosting03052023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BMFPL_Bulk_Leave_Posting_03_05_2023");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);
    });

    modelBuilder.Entity<BoardInstitute>(entity =>
    {
      entity.Property(e => e.BoardInstituteName)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Bonus>(entity =>
    {
      entity.Property(e => e.Bonusid).HasColumnName("BONUSID");
      entity.Property(e => e.Accountsuserid).HasColumnName("ACCOUNTSUSERID");
      entity.Property(e => e.Approvedate)
              .HasColumnType("datetime")
              .HasColumnName("APPROVEDATE");
      entity.Property(e => e.Approverid).HasColumnName("APPROVERID");
      entity.Property(e => e.Bonusamount).HasColumnName("BONUSAMOUNT");
      entity.Property(e => e.Bonusmonth)
              .HasColumnType("datetime")
              .HasColumnName("BONUSMONTH");
      entity.Property(e => e.Bonustype)
              .HasComment("1=Festival Bonus, 2=Performance Bonus, 3=Project Bonus, 4=Profit Sharing")
              .HasColumnName("BONUSTYPE");
      entity.Property(e => e.Generatedate)
              .HasColumnType("datetime")
              .HasColumnName("GENERATEDATE");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Lastupdatedate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.Messageid).HasColumnName("MESSAGEID");
      entity.Property(e => e.Paymentmode)
              .HasMaxLength(50)
              .HasColumnName("PAYMENTMODE");
      entity.Property(e => e.Stateid).HasColumnName("STATEID");
    });

    modelBuilder.Entity<BonusPolicy>(entity =>
    {
      entity.Property(e => e.BonusPercentage).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.BonusPolicyName).HasMaxLength(50);
      entity.Property(e => e.FixedValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<BonusTaxSetup>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CutOfDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BonusTypeName>(entity =>
    {
      entity.HasKey(e => e.BonusTypeId);

      entity.Property(e => e.BonusTypeName1)
              .HasMaxLength(100)
              .HasColumnName("BonusTypeName");
    });

    modelBuilder.Entity<Bonustemp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("BONUSTEMP");

      entity.Property(e => e.Accountsuserid).HasColumnName("ACCOUNTSUSERID");
      entity.Property(e => e.Approvedate)
              .HasColumnType("datetime")
              .HasColumnName("APPROVEDATE");
      entity.Property(e => e.Approverid).HasColumnName("APPROVERID");
      entity.Property(e => e.Bonusamount).HasColumnName("BONUSAMOUNT");
      entity.Property(e => e.Bonusmonth)
              .HasColumnType("datetime")
              .HasColumnName("BONUSMONTH");
      entity.Property(e => e.Bonustype).HasColumnName("BONUSTYPE");
      entity.Property(e => e.Generatedate)
              .HasColumnType("datetime")
              .HasColumnName("GENERATEDATE");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Lastupdatedate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.Messageid).HasColumnName("MESSAGEID");
      entity.Property(e => e.Paymentmode).HasColumnName("PAYMENTMODE");
      entity.Property(e => e.Stateid).HasColumnName("STATEID");
    });

    modelBuilder.Entity<Branch>(entity =>
    {
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.BranchAddress)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.Branchcode)
              .HasMaxLength(50)
              .HasColumnName("BRANCHCODE");
      entity.Property(e => e.Branchdescription)
              .HasMaxLength(2000)
              .HasColumnName("BRANCHDESCRIPTION");
      entity.Property(e => e.Branchname)
              .HasMaxLength(100)
              .HasColumnName("BRANCHNAME");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<BranchShift>(entity =>
    {
      entity.Property(e => e.Branchshiftid).HasColumnName("BRANCHSHIFTID");
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
    });

    modelBuilder.Entity<Canteen>(entity =>
    {
      entity.Property(e => e.CanteenCode).HasMaxLength(50);
      entity.Property(e => e.CanteenName).HasMaxLength(200);
    });

    modelBuilder.Entity<CanteenBilling>(entity =>
    {
      entity.HasKey(e => e.CanteenBillingId).HasName("PK_CanteenBilling_1");

      entity.Property(e => e.FromDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasMaxLength(500);
      entity.Property(e => e.ToDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CanteenBillingList>(entity =>
    {
      entity.HasKey(e => e.CanteenBillingListId).HasName("PK_CanteenBilling");

      entity.Property(e => e.MealDate).HasColumnType("datetime");

      entity.HasOne(d => d.CanteenBilling).WithMany(p => p.CanteenBillingList)
              .HasForeignKey(d => d.CanteenBillingId)
              .HasConstraintName("FK_CanteenBillingList_CanteenBilling");
    });

    modelBuilder.Entity<CanteenBillingPenalty>(entity =>
    {
      entity.HasKey(e => e.CanteenPenaltyId);

      entity.Property(e => e.PenaltyDate).HasColumnType("datetime");
      entity.Property(e => e.PenaltyReason).HasMaxLength(500);

      entity.HasOne(d => d.CanteenBilling).WithMany(p => p.CanteenBillingPenalty)
              .HasForeignKey(d => d.CanteenBillingId)
              .HasConstraintName("FK_CanteenBillingPenalty_CanteenBilling");

      entity.HasOne(d => d.CanteenBillingList).WithMany(p => p.CanteenBillingPenalty)
              .HasForeignKey(d => d.CanteenBillingListId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_CanteenBillingPenalty_CanteenBillingList");
    });

    modelBuilder.Entity<CanteenBookingGuest>(entity =>
    {
      entity.Property(e => e.FromLocation).HasMaxLength(100);
      entity.Property(e => e.GuestName).HasMaxLength(100);
      entity.Property(e => e.Purpose).HasMaxLength(250);

      entity.HasOne(d => d.BookingCalenderMonthly).WithMany(p => p.CanteenBookingGuest)
              .HasForeignKey(d => d.BookingCalenderMonthlyId)
              .HasConstraintName("FK_CanteenBookingGuest_CanteenBookingMonthlyMeals1");
    });

    modelBuilder.Entity<CanteenBookingMonthlyMeals>(entity =>
    {
      entity.HasKey(e => e.BookingCalenderMonthlyId);

      entity.Property(e => e.ChangeType).HasMaxLength(10);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.MealDate).HasColumnType("datetime");

      entity.HasOne(d => d.CanteenBooking).WithMany(p => p.CanteenBookingMonthlyMeals)
              .HasForeignKey(d => d.CanteenBookingId)
              .HasConstraintName("FK_CanteenBookingMonthlyMeals_CanteenBooking");
    });

    modelBuilder.Entity<CanteenDeduction>(entity =>
    {
      entity.HasKey(e => e.EmployeeDeductionId).HasName("PK_CanteenDeduction_1");

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<CanteenEmployeeSubsidyMapping>(entity =>
    {
      entity.HasKey(e => e.EmployeeSubsidyId);

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<CanteenEnrollment>(entity =>
    {
      entity.HasKey(e => e.EnrollmentId);

      entity.Property(e => e.CompanyId)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<CanteenEnrollmentWeekdayMeals>(entity =>
    {
      entity.HasKey(e => e.EnrollmentWeekdayMealId);
    });

    modelBuilder.Entity<CanteenGuestBillPay>(entity =>
    {
      entity.HasKey(e => e.GuestBillPayId);

      entity.Property(e => e.GuestBillPayId).ValueGeneratedNever();
      entity.Property(e => e.GuestBillPayBy).HasMaxLength(50);
    });

    modelBuilder.Entity<CanteenMealCalender>(entity =>
    {
      entity.HasKey(e => e.MealCalenderId);

      entity.Property(e => e.CutOffTime).HasMaxLength(10);
      entity.Property(e => e.MealCalenderName).HasMaxLength(100);
    });

    modelBuilder.Entity<CanteenMealCalenderWeekdayPlanning>(entity =>
    {
      entity.HasKey(e => e.WeekDayPlanningId);
    });

    modelBuilder.Entity<CanteenMealType>(entity =>
    {
      entity.HasKey(e => e.MealTypeId).HasName("PK_CanteenMealTypeSummary");

      entity.Property(e => e.MealCode).HasMaxLength(10);
      entity.Property(e => e.MealName).HasMaxLength(100);
      entity.Property(e => e.SmsCode).HasMaxLength(10);
    });

    modelBuilder.Entity<CanteenMealTypeSubsidiary>(entity =>
    {
      entity.HasKey(e => e.SubsidiaryId).HasName("PK_CanteenMealTypeSubsidiaryDetails");

      entity.Property(e => e.SubsidiaryTypeName).HasMaxLength(100);
    });

    modelBuilder.Entity<CanteenSubsidiaryType>(entity =>
    {
      entity.HasKey(e => e.SubsidiaryTypeId);

      entity.Property(e => e.CreatedAt).HasColumnType("datetime");
      entity.Property(e => e.SubsidiaryTypeName).HasMaxLength(100);
    });

    modelBuilder.Entity<CanteenSupplier>(entity =>
    {
      entity.HasKey(e => e.SupplierId);

      entity.Property(e => e.Address).HasMaxLength(500);
      entity.Property(e => e.BillingContactPerson).HasMaxLength(50);
      entity.Property(e => e.Email).HasMaxLength(100);
      entity.Property(e => e.Mobile).HasMaxLength(50);
      entity.Property(e => e.Phone).HasMaxLength(50);
      entity.Property(e => e.PrimaryContactPerson).HasMaxLength(50);
      entity.Property(e => e.SupplierCode).HasMaxLength(50);
      entity.Property(e => e.SupplierName).HasMaxLength(200);
    });

    modelBuilder.Entity<CanteenSupplierLedger>(entity =>
    {
      entity.HasKey(e => e.SupplierLedgerId);

      entity.Property(e => e.LedgerReferenceNumber).HasMaxLength(250);
      entity.Property(e => e.SupplierLedgerDate).HasColumnType("datetime");

      entity.HasOne(d => d.CanteenBillingList).WithMany(p => p.CanteenSupplierLedger)
              .HasForeignKey(d => d.CanteenBillingListId)
              .HasConstraintName("FK_CanteenSupplierLedger_CanteenBillingList");
    });

    modelBuilder.Entity<CareerHistory>(entity =>
    {
      entity.HasKey(e => e.EmploymentHistoryId).HasName("PK_EmploymentHistory");

      entity.HasIndex(e => e.TransferPromotionId, "NonClasteredCareerHistoryIndex");

      entity.HasIndex(e => e.HrrecordId, "NonClusteredCareerHistoryIndex");

      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EffectiveEndDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveStartDate).HasColumnType("datetime");
      entity.Property(e => e.EmpId).HasMaxLength(300);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks).HasMaxLength(300);
    });

    modelBuilder.Entity<CareerHistory18122022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("CareerHistory_18_12_2022");

      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EffectiveEndDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveStartDate).HasColumnType("datetime");
      entity.Property(e => e.EmpId).HasMaxLength(50);
      entity.Property(e => e.EmploymentHistoryId).ValueGeneratedOnAdd();
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Remarks).HasMaxLength(50);
    });

    modelBuilder.Entity<CareerHistoryTempCurrent18122022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("CareerHistory_Temp_Current_18_12_2022");
    });

    modelBuilder.Entity<CareerHistoryTmpUpload04042024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("CareerHistory_Tmp_Upload_04_04_2024");

      entity.Property(e => e.EmployeeId)
              .HasMaxLength(150)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.FormDepertment)
              .HasMaxLength(150)
              .HasColumnName("Form_Depertment");
      entity.Property(e => e.FromCompany)
              .HasMaxLength(150)
              .HasColumnName("From_Company");
      entity.Property(e => e.FromCostCentre)
              .HasMaxLength(150)
              .HasColumnName("From_CostCentre");
      entity.Property(e => e.FromDesignation).HasColumnName("From_Designation");
      entity.Property(e => e.FromLocation)
              .HasMaxLength(150)
              .HasColumnName("From_Location");
      entity.Property(e => e.FromPayScale)
              .HasMaxLength(150)
              .HasColumnName("From_Pay_Scale");
      entity.Property(e => e.FullName).HasMaxLength(150);
      entity.Property(e => e.PostingTypeName).HasMaxLength(150);
      entity.Property(e => e.PostingTypeRemarks).HasMaxLength(250);
      entity.Property(e => e.ToCompany)
              .HasMaxLength(150)
              .HasColumnName("To_Company");
      entity.Property(e => e.ToCostCentre)
              .HasMaxLength(150)
              .HasColumnName("To_CostCentre");
      entity.Property(e => e.ToDepertment)
              .HasMaxLength(150)
              .HasColumnName("To_Depertment");
      entity.Property(e => e.ToDesignation).HasColumnName("To_Designation");
      entity.Property(e => e.ToLocation)
              .HasMaxLength(150)
              .HasColumnName("To_Location");
      entity.Property(e => e.ToPayScale)
              .HasMaxLength(150)
              .HasColumnName("To_Pay_Scale");
    });

    modelBuilder.Entity<CareerHistoryTmpUpload13062024>(entity =>
    {
      entity.HasKey(e => e.CareerHistoryTmpUploadId).HasName("PK_CareerHistory_Tmp_Upload_13_05_2024");

      entity.ToTable("CareerHistory_Tmp_Upload_13_06_2024");

      entity.Property(e => e.EmployeeId)
              .HasMaxLength(150)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.FormDepertment)
              .HasMaxLength(150)
              .HasColumnName("Form_Depertment");
      entity.Property(e => e.FromCompany)
              .HasMaxLength(150)
              .HasColumnName("From_Company");
      entity.Property(e => e.FromCostCentre)
              .HasMaxLength(150)
              .HasColumnName("From_CostCentre");
      entity.Property(e => e.FromDesignation).HasColumnName("From_Designation");
      entity.Property(e => e.FromLocation)
              .HasMaxLength(150)
              .HasColumnName("From_Location");
      entity.Property(e => e.FromPayScale)
              .HasMaxLength(150)
              .HasColumnName("From_Pay_Scale");
      entity.Property(e => e.FullName).HasMaxLength(150);
      entity.Property(e => e.PostingTypeName).HasMaxLength(150);
      entity.Property(e => e.PostingTypeRemarks).HasMaxLength(250);
      entity.Property(e => e.ToCompany)
              .HasMaxLength(150)
              .HasColumnName("To_Company");
      entity.Property(e => e.ToCostCentre)
              .HasMaxLength(150)
              .HasColumnName("To_CostCentre");
      entity.Property(e => e.ToDepertment)
              .HasMaxLength(150)
              .HasColumnName("To_Depertment");
      entity.Property(e => e.ToDesignation).HasColumnName("To_Designation");
      entity.Property(e => e.ToLocation)
              .HasMaxLength(150)
              .HasColumnName("To_Location");
      entity.Property(e => e.ToPayScale)
              .HasMaxLength(150)
              .HasColumnName("To_Pay_Scale");
    });

    modelBuilder.Entity<CareerHistoryTmpUpload13062024Food>(entity =>
    {
      entity.HasKey(e => e.CareerHistoryTmpUploadId).HasName("PK_CareerHistory_Tmp_Upload_13_05_2024_FOOD");

      entity.ToTable("CareerHistory_Tmp_Upload_13_06_2024_FOOD");

      entity.Property(e => e.EmployeeId)
              .HasMaxLength(150)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.FormDepertment)
              .HasMaxLength(150)
              .HasColumnName("Form_Depertment");
      entity.Property(e => e.FromCompany)
              .HasMaxLength(150)
              .HasColumnName("From_Company");
      entity.Property(e => e.FromCostCentre)
              .HasMaxLength(150)
              .HasColumnName("From_CostCentre");
      entity.Property(e => e.FromDesignation).HasColumnName("From_Designation");
      entity.Property(e => e.FromLocation)
              .HasMaxLength(150)
              .HasColumnName("From_Location");
      entity.Property(e => e.FromPayScale)
              .HasMaxLength(150)
              .HasColumnName("From_Pay_Scale");
      entity.Property(e => e.FullName).HasMaxLength(150);
      entity.Property(e => e.PostingTypeName).HasMaxLength(150);
      entity.Property(e => e.PostingTypeRemarks).HasMaxLength(250);
      entity.Property(e => e.ToCompany)
              .HasMaxLength(150)
              .HasColumnName("To_Company");
      entity.Property(e => e.ToCostCentre)
              .HasMaxLength(150)
              .HasColumnName("To_CostCentre");
      entity.Property(e => e.ToDepertment)
              .HasMaxLength(150)
              .HasColumnName("To_Depertment");
      entity.Property(e => e.ToDesignation).HasColumnName("To_Designation");
      entity.Property(e => e.ToLocation)
              .HasMaxLength(150)
              .HasColumnName("To_Location");
      entity.Property(e => e.ToPayScale)
              .HasMaxLength(150)
              .HasColumnName("To_Pay_Scale");
    });

    modelBuilder.Entity<Certificatetype>(entity =>
    {
      entity.ToTable("CERTIFICATETYPE");

      entity.Property(e => e.Certificatetypeid).HasColumnName("CERTIFICATETYPEID");
      entity.Property(e => e.Certificatecode)
              .HasMaxLength(50)
              .HasColumnName("CERTIFICATECODE");
      entity.Property(e => e.Certificatetypename)
              .HasMaxLength(250)
              .HasColumnName("CERTIFICATETYPENAME");
      entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
    });

    modelBuilder.Entity<ChallanNumberUpload>(entity =>
    {
      entity.HasKey(e => e.ChallanNumberId);

      entity.Property(e => e.Ammount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AssesmentYearId).HasMaxLength(50);
      entity.Property(e => e.BankBranchName).HasMaxLength(500);
      entity.Property(e => e.BankName).HasMaxLength(50);
      entity.Property(e => e.CarryingPerson).HasMaxLength(50);
      entity.Property(e => e.ChallanDate).HasColumnType("datetime");
      entity.Property(e => e.ChallanNumber).HasMaxLength(150);
      entity.Property(e => e.ChallanSubmissionDate).HasColumnType("datetime");
      entity.Property(e => e.ChequeNumber).HasMaxLength(50);
      entity.Property(e => e.PayOrderDate).HasColumnType("datetime");
      entity.Property(e => e.PobankBranchId)
              .HasMaxLength(50)
              .HasColumnName("POBankBranchId");
      entity.Property(e => e.PobankId)
              .HasMaxLength(50)
              .HasColumnName("POBankId");
      entity.Property(e => e.ReferenceNumber).HasMaxLength(300);
      entity.Property(e => e.SalaryYearId).HasMaxLength(50);
      entity.Property(e => e.TotalAmountInChallan).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ChallanNumberUploadTemp>(entity =>
    {
      entity.HasKey(e => e.ChallanNumberId);

      entity.ToTable("ChallanNumberUpload_Temp");

      entity.Property(e => e.Ammount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.BankBranchName).HasMaxLength(500);
      entity.Property(e => e.ChallanNumber).HasMaxLength(150);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.TotalAmountInChallan).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ChatMessage>(entity =>
    {
      entity.HasKey(e => e.ChatingId);

      entity.Property(e => e.ChatLocation)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.ChatMessage1).HasColumnName("ChatMessage");
      entity.Property(e => e.ChatTime).HasColumnType("datetime");
      entity.Property(e => e.ConnectionId)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.SeenTime).HasColumnType("datetime");
      entity.Property(e => e.SentTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<ChatStatus>(entity =>
    {
      entity.Property(e => e.ChatStatusId).ValueGeneratedNever();
      entity.Property(e => e.ChatStatusTitle)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.IconPath)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<ChatUsers>(entity =>
    {
      entity.HasKey(e => e.ChatUserId).HasName("PK_ChatUser");

      entity.HasIndex(e => e.ConnectionId, "IX_ChatUsers").IsUnique();

      entity.Property(e => e.ConnectionId)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.ConnectionTime).HasColumnType("datetime");
      entity.Property(e => e.DisConnectionTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<Cheque>(entity =>
    {
      entity.Property(e => e.ChequeImagePath).HasMaxLength(150);
      entity.Property(e => e.RptName).HasMaxLength(50);
    });

    modelBuilder.Entity<ChequeLeafMaster>(entity =>
    {
      entity.Property(e => e.ChequebookNo)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Date).HasColumnType("datetime");
    });

    modelBuilder.Entity<ClearenceDetails>(entity =>
    {
      entity.Property(e => e.ClearenceDetailsId).ValueGeneratedNever();
      entity.Property(e => e.Remarks).HasMaxLength(200);
    });

    modelBuilder.Entity<Client>(entity =>
    {
      entity.Property(e => e.ClientCode).HasMaxLength(50);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ContactNo).HasMaxLength(100);
      entity.Property(e => e.Email).HasMaxLength(100);
      entity.Property(e => e.PrimaryContact).HasMaxLength(100);
    });

    modelBuilder.Entity<ClientContact>(entity =>
    {
      entity.ToTable("CLIENT_CONTACT");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.CellPhone)
              .HasMaxLength(50)
              .HasColumnName("CELL_PHONE");
      entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");
      entity.Property(e => e.Designation)
              .HasMaxLength(50)
              .HasColumnName("DESIGNATION");
      entity.Property(e => e.Email)
              .HasMaxLength(50)
              .HasColumnName("EMAIL");
      entity.Property(e => e.EntryDate)
              .HasColumnType("datetime")
              .HasColumnName("ENTRY_DATE");
      entity.Property(e => e.HomeAddress)
              .HasMaxLength(500)
              .HasColumnName("HOME_ADDRESS");
      entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
      entity.Property(e => e.IsPrimaryContact).HasColumnName("IS_PRIMARY_CONTACT");
      entity.Property(e => e.LastUpdateDate)
              .HasColumnType("datetime")
              .HasColumnName("LAST_UPDATE_DATE");
      entity.Property(e => e.OfficeAddress)
              .HasMaxLength(500)
              .HasColumnName("OFFICE_ADDRESS");
      entity.Property(e => e.PersonName)
              .HasMaxLength(100)
              .HasColumnName("PERSON_NAME");
      entity.Property(e => e.UserId).HasColumnName("USER_ID");
    });

    modelBuilder.Entity<CnfAgency>(entity =>
    {
      entity.ToTable("CNF_AGENCY");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.AgencyAddress).HasColumnName("AGENCY_ADDRESS");
      entity.Property(e => e.AgencyCode)
              .HasMaxLength(25)
              .HasColumnName("AGENCY_CODE");
      entity.Property(e => e.AgencyName)
              .HasMaxLength(50)
              .HasColumnName("AGENCY_NAME");
      entity.Property(e => e.AgencyTypeId).HasColumnName("AGENCY_TYPE_ID");
      entity.Property(e => e.ContactNumber)
              .HasMaxLength(50)
              .HasColumnName("CONTACT_NUMBER");
      entity.Property(e => e.ContactPerson)
              .HasMaxLength(50)
              .HasColumnName("CONTACT_PERSON");
      entity.Property(e => e.OfficePhoneNo)
              .HasMaxLength(50)
              .HasColumnName("OFFICE_PHONE_NO");
    });

    modelBuilder.Entity<CnfAgencyType>(entity =>
    {
      entity.HasKey(e => e.AgencyTypeId);

      entity.ToTable("CNF_AGENCY_TYPE");

      entity.Property(e => e.AgencyTypeId).HasColumnName("AGENCY_TYPE_ID");
      entity.Property(e => e.AgencyTypeName)
              .HasMaxLength(200)
              .HasColumnName("AGENCY_TYPE_NAME");
    });

    modelBuilder.Entity<CnfAgents>(entity =>
    {
      entity.HasKey(e => e.AgentId);

      entity.ToTable("CNF_AGENTS");

      entity.Property(e => e.AgentId).HasColumnName("AGENT_ID");
      entity.Property(e => e.AgencyId).HasColumnName("AGENCY_ID");
      entity.Property(e => e.AgentCode)
              .HasMaxLength(50)
              .HasColumnName("AGENT_CODE");
      entity.Property(e => e.AgentName)
              .HasMaxLength(50)
              .HasColumnName("AGENT_NAME");
      entity.Property(e => e.FatherName)
              .HasMaxLength(50)
              .HasColumnName("FATHER_NAME");
      entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
      entity.Property(e => e.JslN0)
              .HasMaxLength(50)
              .HasColumnName("JSL_N0");
      entity.Property(e => e.LocalAddress)
              .HasMaxLength(150)
              .HasColumnName("LOCAL_ADDRESS");
      entity.Property(e => e.PerAddress)
              .HasMaxLength(150)
              .HasColumnName("PER_ADDRESS");
      entity.Property(e => e.Photo)
              .HasMaxLength(250)
              .HasColumnName("PHOTO");
      entity.Property(e => e.PoliceVerification)
              .HasMaxLength(100)
              .HasColumnName("POLICE_VERIFICATION");
    });

    modelBuilder.Entity<CnfLicense>(entity =>
    {
      entity.ToTable("CNF_LICENSE");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("ID");
      entity.Property(e => e.AgentId).HasColumnName("AGENT_ID");
      entity.Property(e => e.IssueDate)
              .HasColumnType("datetime")
              .HasColumnName("ISSUE_DATE");
      entity.Property(e => e.LiIssuingUserId).HasColumnName("LI_ISSUING_USER_ID");
      entity.Property(e => e.MrNo).HasColumnName("MR_NO");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.UpdateDate)
              .HasColumnType("datetime")
              .HasColumnName("UPDATE_DATE");
      entity.Property(e => e.ValiedUpto)
              .HasColumnType("datetime")
              .HasColumnName("VALIED_UPTO");
    });

    modelBuilder.Entity<CnfLicenseHistry>(entity =>
    {
      entity.ToTable("CNF_LICENSE_HISTRY");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.AgentId).HasColumnName("AGENT_ID");
      entity.Property(e => e.IssueDate)
              .HasColumnType("datetime")
              .HasColumnName("ISSUE_DATE");
      entity.Property(e => e.LiIssuingUserId).HasColumnName("LI_ISSUING_USER_ID");
      entity.Property(e => e.MrNo).HasColumnName("MR_NO");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.UpdateDate)
              .HasColumnType("datetime")
              .HasColumnName("UPDATE_DATE");
      entity.Property(e => e.ValiedUpto)
              .HasColumnType("datetime")
              .HasColumnName("VALIED_UPTO");
    });

    modelBuilder.Entity<CoffCertificate>(entity =>
    {
      entity.HasKey(e => e.CoffId)
              .HasName("PK_COffCertificate_1")
              .IsClustered(false);

      entity.ToTable("COffCertificate", tb => tb.HasTrigger("AfterInsertCOffCertificate"));

      entity.HasIndex(e => new { e.HrrecordId, e.DateOfWork }, "ClusteredIndex_20200905_133939").IsClustered();

      entity.HasIndex(e => new { e.HrrecordId, e.DateOfWork, e.StateId }, "IX_Duplicate").IsUnique();

      entity.Property(e => e.CoffId).HasColumnName("COffId");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificate16032024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_16_03_2024");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificateArchive2017>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_Archive_2017");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.OldCoffId).HasColumnName("OldCOffId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificateArchive2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_Archive_2018");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.OldCoffId).HasColumnName("OldCOffId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificateArchive2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_Archive_2019");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.OldCoffId).HasColumnName("OldCOffId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificateArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_Archive_2020");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.OldCoffId).HasColumnName("OldCOffId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificateArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_Archive_2021");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificateArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_Archive_2022");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CoffCertificateArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("COffCertificate_Archive_2023");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CoffId)
              .ValueGeneratedOnAdd()
              .HasColumnName("COffId");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Company>(entity =>
    {
      entity.Property(e => e.Address).HasMaxLength(1000);
      entity.Property(e => e.CompanyAlias).HasMaxLength(50);
      entity.Property(e => e.CompanyCircle).HasMaxLength(200);
      entity.Property(e => e.CompanyCode).HasMaxLength(50);
      entity.Property(e => e.CompanyName).HasMaxLength(50);
      entity.Property(e => e.CompanyRegisterNo).HasMaxLength(250);
      entity.Property(e => e.CompanyTin).HasMaxLength(50);
      entity.Property(e => e.CompanyZone).HasMaxLength(200);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Email).HasMaxLength(100);
      entity.Property(e => e.Fax).HasMaxLength(50);
      entity.Property(e => e.FullLogoPath).HasMaxLength(1000);
      entity.Property(e => e.FullLogoPathForReport).HasMaxLength(1000);
      entity.Property(e => e.IsElautoAddedForCurrentYear).HasColumnName("IsELAutoAddedForCurrentYear");
      entity.Property(e => e.LetterFooter).HasMaxLength(1000);
      entity.Property(e => e.LetterHeader).HasMaxLength(1000);
      entity.Property(e => e.Phone).HasMaxLength(50);
      entity.Property(e => e.PrimaryContact).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CompanyBankBranch>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AccountName).HasMaxLength(100);
      entity.Property(e => e.AccountNo).HasMaxLength(100);
      entity.Property(e => e.CompanyBankBranchId).ValueGeneratedOnAdd();
      entity.Property(e => e.IsActive).HasDefaultValue(true);
    });

    modelBuilder.Entity<CompanyDepartmentMap>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SbuDepartmentMapId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<CompanyDesignatiomMap>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SbuDesignationId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<CompanyDivisionMap>(entity =>
    {
      entity.HasKey(e => e.SbuDivisionMapId);
    });

    modelBuilder.Entity<CompanyFiscalYearMap>(entity =>
    {
      entity.HasKey(e => e.CompanyFiscalYearId);
    });

    modelBuilder.Entity<CompanyLeave>(entity =>
    {
      entity.HasKey(e => e.CompanyLeaveId).HasName("PK_CompanyLeave_1");
    });

    modelBuilder.Entity<CompanyLocationMap>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SbuLocationMapId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<CompanyPayroll>(entity =>
    {
      entity.Property(e => e.CompanyPayrollId).HasColumnName("CompanyPayrollID");
      entity.Property(e => e.BasicSalary)
              .HasComment("1=Salary, 2=Benifits")
              .HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
      entity.Property(e => e.CssfundEmployee)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CSSFundEmployee");
      entity.Property(e => e.CssfundEmployer)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CSSFundEmployer");
      entity.Property(e => e.FestivalBonus).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HouseRent).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MedicalAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MobileAllowance)
              .HasComment("0=Nothing, 1=Addition with Salary, 2=Substruction from Salary")
              .HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtherAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PerformanceBonus).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProfitSharing).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProjectBonus).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<CompanyPf>(entity =>
    {
      entity.HasKey(e => e.ProvidentFundId).HasName("PK_CProvidentFund");

      entity.ToTable("CompanyPF");

      entity.Property(e => e.AmountCr)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("Amount_Cr");
      entity.Property(e => e.AmountDr)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("Amount_Dr");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Particular).HasMaxLength(500);
      entity.Property(e => e.TransectionId).HasMaxLength(50);
    });

    modelBuilder.Entity<Competencies>(entity =>
    {
      entity.Property(e => e.CompetencyName).HasMaxLength(50);
    });

    modelBuilder.Entity<CompetencyAndFunctionMap>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Id).ValueGeneratedOnAdd();

      entity.HasOne(d => d.Competency).WithMany()
              .HasForeignKey(d => d.CompetencyId)
              .HasConstraintName("FK_CompetencyAndFunctionMap_Competencies");

      entity.HasOne(d => d.Function).WithMany()
              .HasForeignKey(d => d.FunctionId)
              .HasConstraintName("FK_CompetencyAndFunctionMap_HrFunction");
    });

    modelBuilder.Entity<CompetencyAreaDepartmentDesignationMapping>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CompetencyAreaGradeMapping>(entity =>
    {
      entity.HasKey(e => e.CompetencyGradeMappingId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CompetencyAreaSection>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_Compitency_Area_Section");

      entity.ToTable("Competency_Area_Section");

      entity.Property(e => e.CompAreaSectionName)
              .HasMaxLength(100)
              .HasColumnName("Comp_Area_Section_Name");
    });

    modelBuilder.Entity<CompetencyAreaSettings>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CompetencyAreaId).ValueGeneratedOnAdd();
      entity.Property(e => e.CompetencyAreaName).HasMaxLength(200);
    });

    modelBuilder.Entity<CompetencyDepartmentDesignationMapping>(entity =>
    {
      entity.HasKey(e => e.CompetencyDepartmentDesignationMappingId).HasName("PK_CompetencyDepartmentMapping");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CompetencyGradeMapping>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CompetencyLevel>(entity =>
    {
      entity.HasKey(e => e.LevelId);

      entity.Property(e => e.LevelTitle).HasMaxLength(50);
      entity.Property(e => e.Remarks)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<CompetencyLevelAndGradeMap>(entity =>
    {
      entity.HasOne(d => d.CompetencyLevel).WithMany(p => p.CompetencyLevelAndGradeMap)
              .HasForeignKey(d => d.CompetencyLevelId)
              .HasConstraintName("FK_CompetencyLevelAndGradeMap_CompetencyLevel");

      entity.HasOne(d => d.Grade).WithMany(p => p.CompetencyLevelAndGradeMap)
              .HasForeignKey(d => d.GradeId)
              .HasConstraintName("FK_CompetencyLevelAndGradeMap_GradeType");
    });

    modelBuilder.Entity<CompetencySectionLevel>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_Compitency_Section_Level");

      entity.ToTable("Competency_Section_Level");

      entity.Property(e => e.CompSectionId).HasColumnName("Comp_Section_Id");
      entity.Property(e => e.Description).HasColumnType("text");

      entity.HasOne(d => d.CompetencyLevel).WithMany(p => p.CompetencySectionLevel)
              .HasForeignKey(d => d.CompetencyLevelId)
              .HasConstraintName("FK_Competency_Section_Level_CompetencyLevel");
    });

    modelBuilder.Entity<CompitencyArea>(entity =>
    {
      entity.Property(e => e.CompitencyAreaName).HasMaxLength(100);
      entity.Property(e => e.Description).HasMaxLength(500);
      entity.Property(e => e.MaxMark).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MinMarks).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ConfirmEmployeePayLab>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Confirm_Employee_PayLab");

      entity.Property(e => e.PayrollCode).HasMaxLength(255);
    });

    modelBuilder.Entity<ConfirmationPolicy>(entity =>
    {
      entity.Property(e => e.ConfirmationPolicyName).HasMaxLength(500);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ContactCategoryType>(entity =>
    {
      entity.HasKey(e => e.CategoryTypeId);

      entity.Property(e => e.CategoryName).HasMaxLength(50);
    });

    modelBuilder.Entity<ContactCompany>(entity =>
    {
      entity.Property(e => e.Address).HasMaxLength(3000);
      entity.Property(e => e.ContactCompanyName).HasMaxLength(500);
      entity.Property(e => e.CustomerCode).HasMaxLength(50);
      entity.Property(e => e.Email).HasMaxLength(50);
      entity.Property(e => e.MobileNo).HasMaxLength(50);
      entity.Property(e => e.PhoneNo).HasMaxLength(50);
      entity.Property(e => e.Website).HasMaxLength(100);
    });

    modelBuilder.Entity<ContactDetails>(entity =>
    {
      entity.HasKey(e => e.ContactId);

      entity.Property(e => e.Designation).HasMaxLength(500);
      entity.Property(e => e.Email).HasMaxLength(150);
      entity.Property(e => e.MobileNo).HasMaxLength(50);
      entity.Property(e => e.Name).HasMaxLength(500);
      entity.Property(e => e.PhoneNo).HasMaxLength(50);
    });

    modelBuilder.Entity<ContractRenewTemp>(entity =>
    {
      entity.HasKey(e => e.ContractRenewTempId).HasName("PK_ContractRenew");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<ConveyancePayment>(entity =>
    {
      entity.HasKey(e => e.PaymentId);

      entity.Property(e => e.PrintDate).HasColumnType("smalldatetime");
      entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ConveyancePaymentDetails>(entity =>
    {
      entity.HasKey(e => e.CpdetailsId);

      entity.Property(e => e.CpdetailsId).HasColumnName("CPDetailsID");
      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Mtype)
              .HasComment("1=Conveyance For Movement, 2= Conveyance for OnSite Client")
              .HasColumnName("MType");

      entity.HasOne(d => d.Payment).WithMany(p => p.ConveyancePaymentDetails)
              .HasForeignKey(d => d.PaymentId)
              .HasConstraintName("FK_ConveyancePaymentDetails_ConveyancePayment");
    });

    modelBuilder.Entity<CopyToDetails>(entity =>
    {
      entity.Property(e => e.CopyToName).HasMaxLength(150);
      entity.Property(e => e.ReferenceTypeName).HasMaxLength(150);
    });

    modelBuilder.Entity<CostCenterAccountHeadMapping>(entity =>
    {
      entity.HasKey(e => e.CcAccountHeadMapId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CostCenterToDeptAndParentCostCenterMapping>(entity =>
    {
      entity.Property(e => e.CostCenter).HasMaxLength(50);
      entity.Property(e => e.DepartmentName).HasMaxLength(255);
      entity.Property(e => e.LocationCode).HasMaxLength(255);
      entity.Property(e => e.LocationName).HasMaxLength(255);
    });

    modelBuilder.Entity<CostCentre>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => e.CostCentreId, "NonClusteredIndex_20200401_173558");

      entity.Property(e => e.CcDescription).HasMaxLength(100);
      entity.Property(e => e.CostCentreCode).HasMaxLength(50);
      entity.Property(e => e.CostCentreId).ValueGeneratedOnAdd();
      entity.Property(e => e.CostCentreName).HasMaxLength(250);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.SjvNumber)
              .HasMaxLength(200)
              .HasColumnName("SJV_Number");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CostCentreBankBranch>(entity =>
    {
      entity.Property(e => e.AccountName).HasMaxLength(100);
      entity.Property(e => e.AccountNo).HasMaxLength(100);
      entity.Property(e => e.IsActive).HasDefaultValue(true);
    });

    modelBuilder.Entity<Country>(entity =>
    {
      entity.Property(e => e.CountryCode).HasMaxLength(50);
      entity.Property(e => e.CountryName).HasMaxLength(100);
    });

    modelBuilder.Entity<CplAltLeaveExpireDaysMap>(entity =>
    {
      entity.HasKey(e => e.LeaveExpireDaysMappingId).HasName("PK_EmployeeLeaveExpireDaysMapping");

      entity.Property(e => e.CplleaveDays).HasColumnName("CPLLeaveDays");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CplAltLeaveExpireDaysMap17122023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("CplAltLeaveExpireDaysMap_17_12_2023");

      entity.Property(e => e.CplleaveDays).HasColumnName("CPLLeaveDays");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.LeaveExpireDaysMappingId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<CplAltLeaveExpireDaysMap24082022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("CplAltLeaveExpireDaysMap_24_08_2022");

      entity.Property(e => e.CplleaveDays).HasColumnName("CPLLeaveDays");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.LeaveExpireDaysMappingId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<CplAltLeaveExpireDaysMap29012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("CplAltLeaveExpireDaysMap_29_01_2024");

      entity.Property(e => e.CplleaveDays).HasColumnName("CPLLeaveDays");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.LeaveExpireDaysMappingId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<CplopeningBalance>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("CPLOpeningBalance");

      entity.Property(e => e.EmpId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
      entity.Property(e => e.OpeningCpl).HasColumnName("OpeningCPL");
      entity.Property(e => e.Remarks)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<CtcAccountHeadMapping>(entity =>
    {
      entity.HasKey(e => e.CtcAccountHeadMapId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CtcCategory>(entity =>
    {
      entity.Property(e => e.CtcCategoryId)
              .ValueGeneratedNever()
              .HasComment("1=Master Category,2=Additional Category");
      entity.Property(e => e.CtcCategoryName)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<CtcPolicy>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.BonusCalculationPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CalculationValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcCode).HasMaxLength(5);
      entity.Property(e => e.CtcId).ValueGeneratedOnAdd();
      entity.Property(e => e.CtcName).HasMaxLength(100);
      entity.Property(e => e.CtcNameAlias).HasMaxLength(50);
      entity.Property(e => e.FixedValueForPersonalUsed).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IsCalcCtc).HasColumnName("IsCalcCTC");
      entity.Property(e => e.IsCalcGth).HasColumnName("IsCalcGTH");
      entity.Property(e => e.IsPfarearDeduction).HasColumnName("IsPFArearDeduction");
      entity.Property(e => e.IsTadaallownce).HasColumnName("IsTADAAllownce");
      entity.Property(e => e.MakeDate).HasColumnType("datetime");
      entity.Property(e => e.MaxExamptedAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtherPaymentStampChange).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CtcPolicyHistory>(entity =>
    {
      entity.Property(e => e.CalculationValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CtcCode).HasMaxLength(5);
      entity.Property(e => e.CtcName).HasMaxLength(100);
      entity.Property(e => e.FixedValueForPersonalUsed).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HistoryUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.MakeDate).HasColumnType("datetime");
      entity.Property(e => e.MaxExamptedAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CtcSlabData>(entity =>
    {
      entity.HasKey(e => e.CtcSlabId);

      entity.Property(e => e.RangePercentage).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SlabTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<CtcTypes>(entity =>
    {
      entity.HasKey(e => e.CtcTypeId);

      entity.Property(e => e.CtcTypeId).ValueGeneratedNever();
      entity.Property(e => e.CtcTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<CurencyRate>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CurrencyMonth).HasColumnType("datetime");
      entity.Property(e => e.CurrencyRateRation).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<CurrencyInfo>(entity =>
    {
      entity.HasKey(e => e.CurrencyId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CurrencyName).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<CurriculumActivities>(entity =>
    {
      entity.Property(e => e.Activitiesname)
              .HasMaxLength(100)
              .HasColumnName("ACTIVITIESNAME");
      entity.Property(e => e.Activitycode)
              .HasMaxLength(50)
              .HasColumnName("ACTIVITYCODE");
      entity.Property(e => e.IsActive).HasComment("0=Inactive,1=Active");
    });

    modelBuilder.Entity<CvsortingCommiteeDetails>(entity =>
    {
      entity.HasKey(e => e.CvSortingCommiteeId).HasName("PK_CVSortingCommiteeDetails_1");

      entity.ToTable("CVSortingCommiteeDetails");
    });

    modelBuilder.Entity<CvsortingReport>(entity =>
    {
      entity.ToTable("CVSortingReport");

      entity.Property(e => e.Remarks).HasMaxLength(250);
      entity.Property(e => e.Source).HasMaxLength(250);
    });

    modelBuilder.Entity<DailySalesAndCollection>(entity =>
    {
      entity.HasKey(e => e.SalesCollectionId);

      entity.Property(e => e.CollectionAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalesAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<DasboardLayoutColumnSettings>(entity =>
    {
      entity.HasKey(e => e.DlColumnId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CssRatio).HasMaxLength(500);
      entity.Property(e => e.CssTitleName).HasMaxLength(250);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<DashboardLayout>(entity =>
    {
      entity.HasKey(e => e.LayoutId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.LayoutTitle).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<DayOff>(entity =>
    {
      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<DayOffInformation>(entity =>
    {
      entity.Property(e => e.Reason).HasColumnType("ntext");
    });

    modelBuilder.Entity<DegreeType>(entity =>
    {
      entity.Property(e => e.DegreeTypeName).HasMaxLength(500);
    });

    modelBuilder.Entity<DeligationInfo>(entity =>
    {
      entity.HasKey(e => e.DeligationId).HasName("PK_Deligation");

      entity.HasIndex(e => new { e.IsActive, e.FromDate, e.ToDate }, "DeligationInfo_INDX_001");
    });

    modelBuilder.Entity<DeligationInfo18012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeligationInfo_18_01_2024");

      entity.Property(e => e.DeligationId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Department>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DepartmentCode).HasMaxLength(50);
      entity.Property(e => e.DepartmentName).HasMaxLength(250);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<DepartmentFacilityMap>(entity =>
    {
      entity.HasKey(e => e.DeptFacilityMapId);
    });

    modelBuilder.Entity<DepartmentSectionMap>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DeptSectionId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Designation>(entity =>
    {
      entity.ToTable("DESIGNATION");

      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DesignationCode).HasMaxLength(50);
      entity.Property(e => e.Designationname)
              .HasMaxLength(250)
              .HasColumnName("DESIGNATIONNAME");
      entity.Property(e => e.DsortOrder).HasColumnName("DSortOrder");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<DesignationType>(entity =>
    {
      entity.Property(e => e.TypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.CardId, e.CardTime }, "NonClusturedDeviceRawTimeRecordIndex");

      entity.HasIndex(e => new { e.CardId, e.CardTime }, "NonClusturedIndex");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord14092023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_14_09_2023");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord20092022Bcdl>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_20_09_2022_BCDL");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2017>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2017");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2018");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2019");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2020");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2021");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2022");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2022FromJuneToSep2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2022_From_June_to_Sep_2022");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2022UptoJune>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2022_Upto_June");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2023");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord2023UpToJun>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_2023_Up_to_Jun");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceRawTimeRecord21800221>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceRawTimeRecord_21800221");

      entity.Property(e => e.Authority).HasMaxLength(50);
      entity.Property(e => e.CardId).HasMaxLength(50);
      entity.Property(e => e.CardSrc).IsUnicode(false);
      entity.Property(e => e.CardTime).HasColumnType("datetime");
      entity.Property(e => e.DevId).HasMaxLength(50);
      entity.Property(e => e.DeviceRawTimeRecordId).ValueGeneratedOnAdd();
      entity.Property(e => e.EmployeeName).HasMaxLength(100);
      entity.Property(e => e.WorkCode).HasMaxLength(50);
    });

    modelBuilder.Entity<DeviceSetup>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CatalogName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Comport)
              .HasMaxLength(50)
              .HasColumnName("COMPort");
      entity.Property(e => e.ConnectionType).HasMaxLength(50);
      entity.Property(e => e.DeviceDescription).HasMaxLength(150);
      entity.Property(e => e.DeviceId).HasMaxLength(50);
      entity.Property(e => e.DeviceModel).HasMaxLength(50);
      entity.Property(e => e.DevicePassword).HasMaxLength(50);
      entity.Property(e => e.DevicePort).HasMaxLength(50);
      entity.Property(e => e.DeviceSetupId).ValueGeneratedOnAdd();
      entity.Property(e => e.DeviceType).HasMaxLength(50);
      entity.Property(e => e.DeviceUser).HasMaxLength(50);
      entity.Property(e => e.Ipaddress)
              .HasMaxLength(250)
              .HasColumnName("IPAddress");
      entity.Property(e => e.Manufacturer)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ServiceType)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<DeviceSetup11102023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceSetup_11_10_2023");

      entity.Property(e => e.CatalogName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Comport)
              .HasMaxLength(50)
              .HasColumnName("COMPort");
      entity.Property(e => e.ConnectionType).HasMaxLength(50);
      entity.Property(e => e.DeviceDescription).HasMaxLength(150);
      entity.Property(e => e.DeviceId).HasMaxLength(50);
      entity.Property(e => e.DeviceModel).HasMaxLength(50);
      entity.Property(e => e.DevicePassword).HasMaxLength(50);
      entity.Property(e => e.DevicePort).HasMaxLength(50);
      entity.Property(e => e.DeviceSetupId).ValueGeneratedOnAdd();
      entity.Property(e => e.DeviceType).HasMaxLength(50);
      entity.Property(e => e.DeviceUser).HasMaxLength(50);
      entity.Property(e => e.Ipaddress)
              .HasMaxLength(250)
              .HasColumnName("IPAddress");
      entity.Property(e => e.Manufacturer)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ServiceType)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<DeviceSetup1804ForDelete>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DeviceSetup_18_04_ForDelete");

      entity.Property(e => e.CatalogName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Comport)
              .HasMaxLength(50)
              .HasColumnName("COMPort");
      entity.Property(e => e.ConnectionType).HasMaxLength(50);
      entity.Property(e => e.DeviceDescription).HasMaxLength(150);
      entity.Property(e => e.DeviceId).HasMaxLength(50);
      entity.Property(e => e.DeviceModel).HasMaxLength(50);
      entity.Property(e => e.DevicePassword).HasMaxLength(50);
      entity.Property(e => e.DevicePort).HasMaxLength(50);
      entity.Property(e => e.DeviceSetupId).ValueGeneratedOnAdd();
      entity.Property(e => e.DeviceType).HasMaxLength(50);
      entity.Property(e => e.DeviceUser).HasMaxLength(50);
      entity.Property(e => e.Ipaddress)
              .HasMaxLength(250)
              .HasColumnName("IPAddress");
      entity.Property(e => e.Manufacturer)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ServiceType)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<DeviceSetup25052024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("deviceSetup_25_05_2024");

      entity.Property(e => e.CatalogName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Comport)
              .HasMaxLength(50)
              .HasColumnName("COMPort");
      entity.Property(e => e.ConnectionType).HasMaxLength(50);
      entity.Property(e => e.DeviceDescription).HasMaxLength(150);
      entity.Property(e => e.DeviceId).HasMaxLength(50);
      entity.Property(e => e.DeviceModel).HasMaxLength(50);
      entity.Property(e => e.DevicePassword).HasMaxLength(50);
      entity.Property(e => e.DevicePort).HasMaxLength(50);
      entity.Property(e => e.DeviceSetupId).ValueGeneratedOnAdd();
      entity.Property(e => e.DeviceType).HasMaxLength(50);
      entity.Property(e => e.DeviceUser).HasMaxLength(50);
      entity.Property(e => e.Ipaddress)
              .HasMaxLength(250)
              .HasColumnName("IPAddress");
      entity.Property(e => e.Manufacturer)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ServiceType)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<DisciplinaryAction>(entity =>
    {
      entity.Property(e => e.DateofPunishment).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.ReleasefromPunishment).HasColumnType("datetime");
      entity.Property(e => e.Uploadfilepath)
              .HasMaxLength(2000)
              .HasColumnName("UPLOADFILEPATH");
    });

    modelBuilder.Entity<Discipline>(entity =>
    {
      entity.Property(e => e.DisciplineName)
              .HasMaxLength(350)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Diseasesinformation>(entity =>
    {
      entity.ToTable("DISEASESINFORMATION");

      entity.Property(e => e.Diseasesinformationid).HasColumnName("DISEASESINFORMATIONID");
      entity.Property(e => e.Description)
              .HasMaxLength(2000)
              .HasColumnName("DESCRIPTION");
      entity.Property(e => e.Disdate)
              .HasColumnType("datetime")
              .HasColumnName("DISDATE");
      entity.Property(e => e.Disstatus)
              .HasMaxLength(200)
              .HasColumnName("DISSTATUS");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
    });

    modelBuilder.Entity<District>(entity =>
    {
      entity.Property(e => e.DistrictCode).HasMaxLength(50);
      entity.Property(e => e.DistrictName).HasMaxLength(100);
      entity.Property(e => e.DistrictNameBn)
              .HasMaxLength(100)
              .HasColumnName("DistrictName_bn");
    });

    modelBuilder.Entity<Division>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DivisionCode).HasMaxLength(50);
      entity.Property(e => e.DivisionName).HasMaxLength(500);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<DivisionDepartmentMap>(entity =>
    {
      entity.HasKey(e => e.DivisonDeptMapId);
    });

    modelBuilder.Entity<Docmdetails>(entity =>
    {
      entity.HasKey(e => e.DocumentId);

      entity.ToTable("DOCMDETAILS");

      entity.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");
      entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
      entity.Property(e => e.Filedescription)
              .HasMaxLength(500)
              .HasColumnName("FILEDESCRIPTION");
      entity.Property(e => e.Filename)
              .HasMaxLength(500)
              .HasColumnName("FILENAME");
      entity.Property(e => e.Fullpath)
              .HasMaxLength(1000)
              .HasColumnName("FULLPATH");
      entity.Property(e => e.Lastopenorclosebyid).HasColumnName("LASTOPENORCLOSEBYID");
      entity.Property(e => e.Lastupdate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATE");
      entity.Property(e => e.Remarks)
              .HasMaxLength(1000)
              .HasColumnName("REMARKS");
      entity.Property(e => e.Responsiblepersonto).HasColumnName("RESPONSIBLEPERSONTO");
      entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
      entity.Property(e => e.Subject)
              .HasMaxLength(500)
              .HasColumnName("SUBJECT");
      entity.Property(e => e.UploadedBy).HasColumnName("UPLOADED_BY");
      entity.Property(e => e.UploadedDate)
              .HasColumnType("datetime")
              .HasColumnName("UPLOADED_DATE");
    });

    modelBuilder.Entity<Docmdetailshistory>(entity =>
    {
      entity.HasKey(e => e.DocumentHistoryId);

      entity.ToTable("DOCMDETAILSHISTORY");

      entity.Property(e => e.DocumentHistoryId).HasColumnName("DOCUMENT_HISTORY_ID");
      entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
      entity.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");
      entity.Property(e => e.Filedescription)
              .HasMaxLength(500)
              .HasColumnName("FILEDESCRIPTION");
      entity.Property(e => e.Filename)
              .HasMaxLength(500)
              .HasColumnName("FILENAME");
      entity.Property(e => e.Fullpath)
              .HasMaxLength(1000)
              .HasColumnName("FULLPATH");
      entity.Property(e => e.Lastopenorclosebyid).HasColumnName("LASTOPENORCLOSEBYID");
      entity.Property(e => e.Lastupdate)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATE");
      entity.Property(e => e.Remarks)
              .HasMaxLength(1000)
              .HasColumnName("REMARKS");
      entity.Property(e => e.Responsiblepersonto).HasColumnName("RESPONSIBLEPERSONTO");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.Subject)
              .HasMaxLength(500)
              .HasColumnName("SUBJECT");
      entity.Property(e => e.UploadedBy).HasColumnName("UPLOADED_BY");
      entity.Property(e => e.UploadedDate)
              .HasColumnType("datetime")
              .HasColumnName("UPLOADED_DATE");
    });

    modelBuilder.Entity<Documanttype>(entity =>
    {
      entity.HasKey(e => e.Documenttypeid);

      entity.ToTable("DOCUMANTTYPE");

      entity.Property(e => e.Documenttypeid).HasColumnName("DOCUMENTTYPEID");
      entity.Property(e => e.Description)
              .HasColumnType("text")
              .HasColumnName("DESCRIPTION");
      entity.Property(e => e.Documentname)
              .HasMaxLength(100)
              .HasColumnName("DOCUMENTNAME");
      entity.Property(e => e.Initiationdate)
              .HasColumnType("datetime")
              .HasColumnName("INITIATIONDATE");
      entity.Property(e => e.UseType)
              .HasDefaultValue(1)
              .HasComment("1=Personal Document,2=Applicant Document");
    });

    modelBuilder.Entity<Document>(entity =>
    {
      entity.ToTable("DOCUMENT");

      entity.Property(e => e.Documentid).HasColumnName("DOCUMENTID");
      entity.Property(e => e.Attacheddocument)
              .HasMaxLength(200)
              .HasColumnName("ATTACHEDDOCUMENT");
      entity.Property(e => e.Documenttypeid).HasColumnName("DOCUMENTTYPEID");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Summary)
              .HasMaxLength(2000)
              .HasColumnName("SUMMARY");
      entity.Property(e => e.Titleofdocument)
              .HasMaxLength(200)
              .HasColumnName("TITLEOFDOCUMENT");
    });

    modelBuilder.Entity<DocumentParameter>(entity =>
    {
      entity.HasKey(e => e.ParameterId);

      entity.Property(e => e.CaseCading)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.ControlRole)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Control_Role");
      entity.Property(e => e.DataSource)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.DataTextField)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.DataValueField)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ParameterKey)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ParameterName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<DocumentParameterMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.IsVisible).HasDefaultValue(true);
      entity.Property(e => e.MappingId).ValueGeneratedOnAdd();

      entity.HasOne(d => d.DocumentType).WithMany()
              .HasForeignKey(d => d.DocumentTypeId)
              .HasConstraintName("FK_DocumentParameterMapping_DOCUMANTTYPE");

      entity.HasOne(d => d.Parameter).WithMany()
              .HasForeignKey(d => d.ParameterId)
              .HasConstraintName("FK_DocumentParameterMapping_DocumentParameter");
    });

    modelBuilder.Entity<DocumentQueryMapping>(entity =>
    {
      entity.HasKey(e => e.DocumentQueryId);

      entity.HasIndex(e => new { e.DocumentTypeId, e.ReportHeaderId }, "IX_DocumentQueryMapping").IsUnique();

      entity.Property(e => e.ParameterDefination)
              .HasMaxLength(1000)
              .IsUnicode(false);
    });

    modelBuilder.Entity<DocumentTemplate>(entity =>
    {
      entity.HasKey(e => e.DocumentId);

      entity.HasIndex(e => e.TemplateName, "IX_DocumentTemplate").IsUnique();

      entity.Property(e => e.DocumentText).IsUnicode(false);
      entity.Property(e => e.DocumentTitle).HasMaxLength(200);
      entity.Property(e => e.TemplateName).HasMaxLength(100);
    });

    modelBuilder.Entity<DottedLineCompanyMapping>(entity =>
    {
      entity.HasKey(e => e.DottedLineCompanyMapId);
    });

    modelBuilder.Entity<DottedLineEmailConfig>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DottedLineEmailConfigId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<DottedLineEmailConfig22012022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("DottedLineEmailConfig_22_01_2022");

      entity.Property(e => e.DottedLineEmailConfigId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<DottedLineEmailConfigHistory>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DottedLineEmailConfigHistoryId).ValueGeneratedOnAdd();
      entity.Property(e => e.HistoryCreateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<DottedLineGradeMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DottedLineGradeMapId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<DottedLineLocationMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DottedLineLocationMapId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<EarlyExitOutPunchMissApprovedLogForEl>(entity =>
    {
      entity.HasKey(e => e.EarlyExitOutPunchApprovedLogId).HasName("PK_EarlyExistOutPunchMissApprovedLogForEL");

      entity.ToTable("EarlyExitOutPunchMissApprovedLogForEL");

      entity.Property(e => e.AttendanceDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EarlyExitOutPunchMissDailyNotifyLog>(entity =>
    {
      entity.HasKey(e => e.EarlyExitOutPunchDailyNotyId);

      entity.Property(e => e.AdjustmentDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EarlyExitOutPunchMissThreeTimesNotifyLog>(entity =>
    {
      entity.HasKey(e => e.EarlyExitOutPunchId);

      entity.Property(e => e.AdjustmentDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EarnedLeaveAdjustmentLog>(entity =>
    {
      entity.HasKey(e => e.LeaveAdjustmentId);

      entity.HasIndex(e => new { e.LeaveAdjustmentId, e.HrrecordId, e.AdjustmentDate, e.TransactionTypeId }, "NonClusteredIndex_20210204_090622");

      entity.Property(e => e.AdjustmentDate).HasColumnType("datetime");
      entity.Property(e => e.ClosingLeaveBalanceNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ClosingLeaveBalanceOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForwardNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForwardOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeductedNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeductedOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoiedNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoiedOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalanceNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalanceOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Education>(entity =>
    {
      entity.HasKey(e => e.EducationHistoryId);

      entity.Property(e => e.Board)
              .HasMaxLength(250)
              .HasColumnName("BOARD");
      entity.Property(e => e.Certificate).HasMaxLength(50);
      entity.Property(e => e.Certificatetypeid).HasColumnName("CERTIFICATETYPEID");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Institute).HasMaxLength(250);
      entity.Property(e => e.Result).HasMaxLength(250);
      entity.Property(e => e.Yearofcompletion).HasMaxLength(50);
    });

    modelBuilder.Entity<EducationDynamic>(entity =>
    {
      entity.HasKey(e => e.EducationHistoryDynamicId);

      entity.Property(e => e.Certificate).HasMaxLength(50);
      entity.Property(e => e.Certificatetypeid).HasColumnName("CERTIFICATETYPEID");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Result).HasMaxLength(250);
    });

    modelBuilder.Entity<EducationTemp>(entity =>
    {
      entity.Property(e => e.Board)
              .HasMaxLength(500)
              .HasColumnName("BOARD");
      entity.Property(e => e.Certificate).HasMaxLength(500);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.HighestEducation).HasMaxLength(50);
      entity.Property(e => e.Institute).HasMaxLength(500);
      entity.Property(e => e.Result).HasMaxLength(50);
      entity.Property(e => e.ResultStatus).HasMaxLength(500);
      entity.Property(e => e.Yearofcompletion).HasMaxLength(500);
    });

    modelBuilder.Entity<Eligibility>(entity =>
    {
      entity.Property(e => e.EligibilityName)
              .HasMaxLength(200)
              .IsUnicode(false);
    });

    modelBuilder.Entity<EmailConfigLocationMapping>(entity =>
    {
      entity.HasKey(e => e.EmailConfigLocationMapId);
    });

    modelBuilder.Entity<EmailContent>(entity =>
    {
      entity.HasIndex(e => e.EmailContentId, "IX_EmailContent").IsUnique();

      entity.HasIndex(e => new { e.EmailNotificationId, e.EmailTitleId }, "IX_EmailContent_1").IsUnique();

      entity.Property(e => e.ParamDefination).IsUnicode(false);
      entity.Property(e => e.Smsbody)
              .HasMaxLength(1000)
              .HasColumnName("SMSBody");
    });

    modelBuilder.Entity<EmailContent03062024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmailContent_03_06_2024");

      entity.Property(e => e.EmailContentId).ValueGeneratedOnAdd();
      entity.Property(e => e.ParamDefination).IsUnicode(false);
      entity.Property(e => e.Smsbody)
              .HasMaxLength(1000)
              .HasColumnName("SMSBody");
    });

    modelBuilder.Entity<EmailContentSourovRec>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmailContentSourov_Rec");

      entity.Property(e => e.EmailContentId).ValueGeneratedOnAdd();
      entity.Property(e => e.ParamDefination).IsUnicode(false);
      entity.Property(e => e.Smsbody)
              .HasMaxLength(1000)
              .HasColumnName("SMSBody");
    });

    modelBuilder.Entity<EmailContentTraining>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmailContent_Training");

      entity.Property(e => e.EmailContentId).ValueGeneratedOnAdd();
      entity.Property(e => e.ParamDefination).IsUnicode(false);
      entity.Property(e => e.Smsbody)
              .HasMaxLength(1000)
              .HasColumnName("SMSBody");
    });

    modelBuilder.Entity<EmailNotificationConfig>(entity =>
    {
      entity.Property(e => e.EmailNotificationConfigId).ValueGeneratedNever();
      entity.Property(e => e.NotificationKey)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.NotificationTitle).HasMaxLength(550);
      entity.Property(e => e.ScheduleType)
              .HasMaxLength(20)
              .IsUnicode(false);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmailNotificationLog>(entity =>
    {
      entity.Property(e => e.GeneratedTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmbassyInfo>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ContryName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.EmbassyAddress)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.EmbassyName)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.EmbassyTitle)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<EmpBankInfo>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Emp_Bank_Info");

      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<EmpFinalSettlementInfo>(entity =>
    {
      entity.HasKey(e => e.EmpFinalSettleInfoId);

      entity.Property(e => e.CcissueDate).HasColumnName("CCIssueDate");
    });

    modelBuilder.Entity<EmpGuranteersInfo>(entity =>
    {
      entity.HasKey(e => e.GuranteerId);

      entity.Property(e => e.GuranteerId).HasColumnName("GuranteerID");
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.GdateOfBirth).HasColumnName("GDateOfBirth");
      entity.Property(e => e.GhrRecordId).HasColumnName("GHrRecordId");
      entity.Property(e => e.GnationalId)
              .HasMaxLength(250)
              .HasColumnName("GNationalID");
      entity.Property(e => e.GofficeAdd).HasColumnName("GOfficeAdd");
      entity.Property(e => e.GpermanentAddress).HasColumnName("GPermanentAddress");
      entity.Property(e => e.GphoneNumber)
              .HasMaxLength(250)
              .HasColumnName("GPhoneNumber");
      entity.Property(e => e.GpresentAddress).HasColumnName("GPresentAddress");
      entity.Property(e => e.Gprofession)
              .HasMaxLength(250)
              .HasColumnName("GProfession");
      entity.Property(e => e.GprofessionTypeId).HasColumnName("GProfessionTypeId");
      entity.Property(e => e.GprofilePicture)
              .HasMaxLength(250)
              .HasColumnName("GProfilePicture");
      entity.Property(e => e.GrelationId).HasColumnName("GRelationId");
      entity.Property(e => e.GstampNo)
              .HasMaxLength(250)
              .HasColumnName("GStampNo");
      entity.Property(e => e.GuranteerName).HasMaxLength(250);
      entity.Property(e => e.UserId).HasColumnName("userId");
    });

    modelBuilder.Entity<EmpManualAttendance>(entity =>
    {
      entity.HasKey(e => e.EmployeeLogId);

      entity.Property(e => e.EmployeeLogId).HasColumnName("EmployeeLogID");
      entity.Property(e => e.Date).HasColumnType("datetime");
      entity.Property(e => e.Status)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<EmpManualAttendanceData>(entity =>
    {
      entity.HasKey(e => e.ManualAttendanceId);

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.InsertDate).HasColumnType("datetime");
      entity.Property(e => e.IsPresent).HasMaxLength(250);
    });

    modelBuilder.Entity<Employee>(entity =>
    {
      entity.HasKey(e => e.HrrecordId);

      entity.ToTable(tb => tb.HasTrigger("trg_EmployeeHist"));

      entity.HasIndex(e => e.StateId, "indx_Emploee_State");

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.AdditionalInfo).HasMaxLength(50);
      entity.Property(e => e.ApproveDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Birthidentification)
              .HasMaxLength(100)
              .HasColumnName("BIRTHIDENTIFICATION");
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.DateofBirth).HasColumnType("datetime");
      entity.Property(e => e.DateofMarriage).HasColumnType("datetime");
      entity.Property(e => e.FatherName).HasMaxLength(500);
      entity.Property(e => e.FullName).HasMaxLength(500);
      entity.Property(e => e.Height)
              .HasMaxLength(50)
              .HasColumnName("HEIGHT");
      entity.Property(e => e.Hobby)
              .HasMaxLength(2000)
              .HasColumnName("HOBBY");
      entity.Property(e => e.HomePhone).HasMaxLength(50);
      entity.Property(e => e.Identificationmark)
              .HasMaxLength(1000)
              .HasColumnName("IDENTIFICATIONMARK");
      entity.Property(e => e.Investmentamount).HasColumnName("INVESTMENTAMOUNT");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Meritialstatus).HasColumnName("MERITIALSTATUS");
      entity.Property(e => e.MobileNo).HasMaxLength(500);
      entity.Property(e => e.MotherName).HasMaxLength(500);
      entity.Property(e => e.NationalId)
              .HasMaxLength(250)
              .HasColumnName("NationalID");
      entity.Property(e => e.OriginalBirthDay).HasMaxLength(50);
      entity.Property(e => e.PassportNo).HasMaxLength(250);
      entity.Property(e => e.Passportexpiredate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTEXPIREDATE");
      entity.Property(e => e.Passportissuedate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTISSUEDATE");
      entity.Property(e => e.PermanentPostCode).HasMaxLength(50);
      entity.Property(e => e.PersonalEmail).HasMaxLength(250);
      entity.Property(e => e.Placeofpassportissue).HasColumnName("PLACEOFPASSPORTISSUE");
      entity.Property(e => e.PresentPostCode).HasMaxLength(50);
      entity.Property(e => e.Profilepicture)
              .HasMaxLength(2000)
              .HasColumnName("PROFILEPICTURE");
      entity.Property(e => e.Refempid)
              .HasMaxLength(50)
              .HasColumnName("REFEMPID");
      entity.Property(e => e.ShortName).HasMaxLength(50);
      entity.Property(e => e.Signature)
              .HasMaxLength(2000)
              .HasColumnName("SIGNATURE");
      entity.Property(e => e.SpouseName).HasMaxLength(500);
      entity.Property(e => e.Taxexamption).HasColumnName("TAXEXAMPTION");
      entity.Property(e => e.Weight)
              .HasMaxLength(50)
              .HasColumnName("WEIGHT");
    });

    modelBuilder.Entity<Employee03082022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Employee_03_08_2022");

      entity.Property(e => e.AdditionalInfo).HasMaxLength(50);
      entity.Property(e => e.ApproveDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Birthidentification)
              .HasMaxLength(100)
              .HasColumnName("BIRTHIDENTIFICATION");
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.DateofBirth).HasColumnType("datetime");
      entity.Property(e => e.DateofMarriage).HasColumnType("datetime");
      entity.Property(e => e.FatherName).HasMaxLength(500);
      entity.Property(e => e.FullName).HasMaxLength(500);
      entity.Property(e => e.Height)
              .HasMaxLength(50)
              .HasColumnName("HEIGHT");
      entity.Property(e => e.Hobby)
              .HasMaxLength(2000)
              .HasColumnName("HOBBY");
      entity.Property(e => e.HomePhone).HasMaxLength(50);
      entity.Property(e => e.HrrecordId)
              .ValueGeneratedOnAdd()
              .HasColumnName("HRRecordId");
      entity.Property(e => e.Identificationmark)
              .HasMaxLength(1000)
              .HasColumnName("IDENTIFICATIONMARK");
      entity.Property(e => e.Investmentamount).HasColumnName("INVESTMENTAMOUNT");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Meritialstatus).HasColumnName("MERITIALSTATUS");
      entity.Property(e => e.MobileNo).HasMaxLength(500);
      entity.Property(e => e.MotherName).HasMaxLength(500);
      entity.Property(e => e.NationalId)
              .HasMaxLength(250)
              .HasColumnName("NationalID");
      entity.Property(e => e.OriginalBirthDay).HasMaxLength(50);
      entity.Property(e => e.PassportNo).HasMaxLength(250);
      entity.Property(e => e.Passportexpiredate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTEXPIREDATE");
      entity.Property(e => e.Passportissuedate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTISSUEDATE");
      entity.Property(e => e.PermanentPostCode).HasMaxLength(50);
      entity.Property(e => e.PersonalEmail).HasMaxLength(250);
      entity.Property(e => e.Placeofpassportissue).HasColumnName("PLACEOFPASSPORTISSUE");
      entity.Property(e => e.PresentPostCode).HasMaxLength(50);
      entity.Property(e => e.Profilepicture)
              .HasMaxLength(2000)
              .HasColumnName("PROFILEPICTURE");
      entity.Property(e => e.Refempid)
              .HasMaxLength(50)
              .HasColumnName("REFEMPID");
      entity.Property(e => e.ShortName).HasMaxLength(50);
      entity.Property(e => e.Signature)
              .HasMaxLength(2000)
              .HasColumnName("SIGNATURE");
      entity.Property(e => e.SpouseName).HasMaxLength(500);
      entity.Property(e => e.Taxexamption).HasColumnName("TAXEXAMPTION");
      entity.Property(e => e.Weight)
              .HasMaxLength(50)
              .HasColumnName("WEIGHT");
    });

    modelBuilder.Entity<EmployeeAbsentSmsRemarks>(entity =>
    {
      entity.HasKey(e => e.AbsentSmsRemarksId).HasName("PK_AbsentSmsRemarks");

      entity.Property(e => e.Remarks).HasMaxLength(200);
      entity.Property(e => e.RemarksCode).HasMaxLength(50);
      entity.Property(e => e.RemarksSpecification).HasMaxLength(250);
    });

    modelBuilder.Entity<EmployeeAsset>(entity =>
    {
      entity.Property(e => e.AssetName).HasMaxLength(150);
      entity.Property(e => e.BrandNo).HasMaxLength(500);
      entity.Property(e => e.ModelNo).HasMaxLength(500);
      entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(500);
      entity.Property(e => e.SerialNo).HasMaxLength(500);
    });

    modelBuilder.Entity<EmployeeAssetTemp>(entity =>
    {
      entity.Property(e => e.AssetName).HasMaxLength(50);
      entity.Property(e => e.AssetType).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<EmployeeBanglaInformation>(entity =>
    {
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.FatherName).HasMaxLength(250);
      entity.Property(e => e.FullName).HasMaxLength(250);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.MotherName).HasMaxLength(250);
      entity.Property(e => e.SpouseName).HasMaxLength(250);
    });

    modelBuilder.Entity<EmployeeBank>(entity =>
    {
      entity.Property(e => e.BankAccountNo)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<EmployeeBankAccountUploadTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.BankName).HasMaxLength(50);
      entity.Property(e => e.BranchName).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<EmployeeBankBranchMap>(entity =>
    {
      entity.Property(e => e.BankAccountNo)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<EmployeeBusLateAdjustmentInfo>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmpBusLateInfoId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<EmployeeContact>(entity =>
    {
      entity.HasKey(e => e.EmergencyContactId);

      entity.Property(e => e.ContactMobile).HasMaxLength(50);
      entity.Property(e => e.ContactName).HasMaxLength(500);
      entity.Property(e => e.ContactRelation).HasMaxLength(500);
    });

    modelBuilder.Entity<EmployeeContactTemp>(entity =>
    {
      entity.HasKey(e => e.NewEmergencyContactId);

      entity.Property(e => e.ContactMobile).HasMaxLength(50);
      entity.Property(e => e.ContactName).HasMaxLength(500);
      entity.Property(e => e.ContactRelation).HasMaxLength(500);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<EmployeeContract>(entity =>
    {
      entity.HasKey(e => e.ContractId);
    });

    modelBuilder.Entity<EmployeeCostCenterTemp>(entity =>
    {
      entity.HasKey(e => e.EmployeeCctempId);

      entity.Property(e => e.EmployeeCctempId).HasColumnName("EmployeeCCTempId");
      entity.Property(e => e.CostCentreCode).HasMaxLength(50);
      entity.Property(e => e.CostShareRatio)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(50)
              .HasColumnName("EmployeeID");
    });

    modelBuilder.Entity<EmployeeCostCentre>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrRecordId, e.IsActive }, "NonClusteredIndex_20200329_172051");

      entity.Property(e => e.EmployeeCcid)
              .ValueGeneratedOnAdd()
              .HasColumnName("EmployeeCCId");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmployeeEarlyCpfinterest>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeeEarlyCPFInterest");

      entity.Property(e => e.CpfInterestComp)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CPF_Interest_Comp");
      entity.Property(e => e.CpfInterestOwn)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CPF_Interest_Own");
    });

    modelBuilder.Entity<EmployeeHistory>(entity =>
    {
      entity.HasKey(e => e.EmpHistoryId).HasName("PK_EmployeeHistory_1");

      entity.Property(e => e.AdditionalInfo).HasMaxLength(50);
      entity.Property(e => e.ApproveDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Birthidentification)
              .HasMaxLength(100)
              .HasColumnName("BIRTHIDENTIFICATION");
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.DateofBirth).HasColumnType("datetime");
      entity.Property(e => e.DateofMarriage).HasColumnType("datetime");
      entity.Property(e => e.EffectEndDate).HasColumnType("datetime");
      entity.Property(e => e.FatherName).HasMaxLength(250);
      entity.Property(e => e.FullName).HasMaxLength(250);
      entity.Property(e => e.Height)
              .HasMaxLength(50)
              .HasColumnName("HEIGHT");
      entity.Property(e => e.Hobby)
              .HasMaxLength(2000)
              .HasColumnName("HOBBY");
      entity.Property(e => e.HomePhone).HasMaxLength(50);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Identificationmark)
              .HasMaxLength(1000)
              .HasColumnName("IDENTIFICATIONMARK");
      entity.Property(e => e.Investmentamount).HasColumnName("INVESTMENTAMOUNT");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Meritialstatus).HasColumnName("MERITIALSTATUS");
      entity.Property(e => e.MobileNo).HasMaxLength(500);
      entity.Property(e => e.MotherName).HasMaxLength(250);
      entity.Property(e => e.NationalId)
              .HasMaxLength(250)
              .HasColumnName("NationalID");
      entity.Property(e => e.OriginalBirthDay).HasMaxLength(50);
      entity.Property(e => e.PassportNo).HasMaxLength(250);
      entity.Property(e => e.Passportexpiredate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTEXPIREDATE");
      entity.Property(e => e.Passportissuedate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTISSUEDATE");
      entity.Property(e => e.PermanentPostCode).HasMaxLength(50);
      entity.Property(e => e.PersonalEmail).HasMaxLength(250);
      entity.Property(e => e.Placeofpassportissue).HasColumnName("PLACEOFPASSPORTISSUE");
      entity.Property(e => e.PresentPostCode).HasMaxLength(50);
      entity.Property(e => e.Profilepicture)
              .HasMaxLength(2000)
              .HasColumnName("PROFILEPICTURE");
      entity.Property(e => e.Refempid)
              .HasMaxLength(50)
              .HasColumnName("REFEMPID");
      entity.Property(e => e.ShortName).HasMaxLength(50);
      entity.Property(e => e.Signature)
              .HasMaxLength(2000)
              .HasColumnName("SIGNATURE");
      entity.Property(e => e.SpouseName).HasMaxLength(250);
      entity.Property(e => e.Taxexamption).HasColumnName("TAXEXAMPTION");
      entity.Property(e => e.Weight)
              .HasMaxLength(50)
              .HasColumnName("WEIGHT");
    });

    modelBuilder.Entity<EmployeeIndex>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeIndexId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<EmployeeLateInfo>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_LateEmployeeData");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.AdjustmentTypeCode)
              .HasMaxLength(10)
              .IsUnicode(false);
      entity.Property(e => e.AttendanceLogId)
              .HasMaxLength(10)
              .IsUnicode(false);
    });

    modelBuilder.Entity<EmployeeLeave>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmployeeLeave1>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeeLeave$");

      entity.Property(e => e.PrivilegeLeave)
              .HasMaxLength(255)
              .HasColumnName("Privilege Leave");
      entity.Property(e => e.Rf1325)
              .HasMaxLength(255)
              .HasColumnName("RF1325");
    });

    modelBuilder.Entity<EmployeeLevel>(entity =>
    {
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeLevelName).HasMaxLength(50);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmployeeMembership>(entity =>
    {
      entity.HasKey(e => e.EmpMembershipId);
    });

    modelBuilder.Entity<EmployeeMembershipTemp>(entity =>
    {
      entity.HasKey(e => e.EmpMembershipTempId);

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.EndDate).HasMaxLength(50);
      entity.Property(e => e.StartDate).HasMaxLength(50);
    });

    modelBuilder.Entity<EmployeePayroll>(entity =>
    {
      entity.ToTable(tb => tb.HasTrigger("tr_Update_Payroll_History"));

      entity.HasIndex(e => e.HrRecordId, "NonClasterEmployeePayrollindex");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollVerifiedDate).HasColumnType("datetime");
      entity.Property(e => e.Status).HasDefaultValue(1);
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayroll08Jun2024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayroll_08_Jun_2024");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeePayrollId).ValueGeneratedOnAdd();
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollVerifiedDate).HasColumnType("datetime");
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayroll09112023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayroll_09_11_2023");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeePayrollId).ValueGeneratedOnAdd();
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollVerifiedDate).HasColumnType("datetime");
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayroll11072023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayroll_11_07_2023");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeePayrollId).ValueGeneratedOnAdd();
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollVerifiedDate).HasColumnType("datetime");
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayroll28072022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayroll_28_07_2022");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeePayrollId).ValueGeneratedOnAdd();
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollVerifiedDate).HasColumnType("datetime");
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayrollDetails>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => e.CtcId, "NonClasterEmployeePayrollDetailsIndex");

      entity.HasIndex(e => e.EmpPayrollId, "NonClasterEmployeePayrollDetailsIndex1_Emp_Payroll_Id");

      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmpPayrollDetailsId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<EmployeePayrollDetails11072023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayrollDetails_11_07_2023");

      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmpPayrollDetailsId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<EmployeePayrollDetails28072022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayrollDetails_28_07_2022");

      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmpPayrollDetailsId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<EmployeePayrollDetailsHistory>(entity =>
    {
      entity.HasKey(e => e.EmpPayrollDetailsId);

      entity.HasIndex(e => e.EmpPayrollId, "Non_Clustered_EmployeePayrollDetailsHistory");

      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayrollDetailsHistory31072022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayrollDetailsHistory_31_07_2022");

      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmpPayrollDetailsId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<EmployeePayrollHistory>(entity =>
    {
      entity.HasKey(e => e.PayrollHistoryId);

      entity.HasIndex(e => new { e.HrRecordId, e.PayRollEndDate }, "NonClasterEmployeePayrollHistoryIndex");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayRollEndDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayrollHistory31072022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeePayrollHistory_31_07_2022");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayRollEndDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollHistoryId).ValueGeneratedOnAdd();
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePayrollVerification>(entity =>
    {
      entity.HasKey(e => e.EmployeePayrollId);

      entity.ToTable("EmployeePayroll_Verification");

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PayrollApproveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollVerifiedDate).HasColumnType("datetime");
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<EmployeePf>(entity =>
    {
      entity.HasKey(e => e.ProvidentFundId).HasName("PK_ProvidentFund");

      entity.ToTable("EmployeePF");

      entity.Property(e => e.AmountCr)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("Amount_Cr");
      entity.Property(e => e.AmountDr)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("Amount_Dr");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Particular).HasMaxLength(500);
      entity.Property(e => e.TransectionId).HasMaxLength(50);
    });

    modelBuilder.Entity<EmployeeProfileTabs>(entity =>
    {
      entity.HasKey(e => e.TabId);

      entity.HasIndex(e => e.TabId, "IX_EmployeeProfileTabs").IsUnique();

      entity.HasIndex(e => e.TabName, "IX_EmployeeProfileTabs_1").IsUnique();

      entity.Property(e => e.TabId).ValueGeneratedNever();
      entity.Property(e => e.ControlType)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.TabKey)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.TabName)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<EmployeeProfileTabsPermission>(entity =>
    {
      entity.HasKey(e => e.TabPermissionId);
    });

    modelBuilder.Entity<EmployeeReference>(entity =>
    {
      entity.HasKey(e => e.ReferenceId);

      entity.Property(e => e.HrRecordId)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.RefAddress).HasMaxLength(500);
      entity.Property(e => e.RefEmail).HasMaxLength(100);
      entity.Property(e => e.RefMobile).HasMaxLength(50);
      entity.Property(e => e.RefOccupation).HasMaxLength(1000);
    });

    modelBuilder.Entity<EmployeeReferenceTemp>(entity =>
    {
      entity.HasKey(e => e.ReferenceTempId);

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.RefAddress).HasMaxLength(50);
      entity.Property(e => e.RefMobile).HasMaxLength(50);
      entity.Property(e => e.RefOccupation).HasMaxLength(1000);
    });

    modelBuilder.Entity<EmployeeReplacementInfo>(entity =>
    {
      entity.HasKey(e => e.NewReplacementId);
    });

    modelBuilder.Entity<EmployeeSapinfo>(entity =>
    {
      entity.HasKey(e => e.EmpSapinfoId);

      entity.ToTable("EmployeeSAPInfo");

      entity.Property(e => e.EmpSapinfoId).HasColumnName("EmpSAPInfoId");
      entity.Property(e => e.SapcompanyCode)
              .HasMaxLength(50)
              .HasColumnName("SAPCompanyCode");
      entity.Property(e => e.SapcostCentreCode)
              .HasMaxLength(50)
              .HasColumnName("SAPCostCentreCode");
      entity.Property(e => e.SapprofitCentreCode)
              .HasMaxLength(50)
              .HasColumnName("SAPProfitCentreCode");
    });

    modelBuilder.Entity<EmployeeSkilsTemp>(entity =>
    {
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.OtherSkill).HasMaxLength(500);
      entity.Property(e => e.Skill).HasMaxLength(500);
    });

    modelBuilder.Entity<EmployeeTemp>(entity =>
    {
      entity.HasKey(e => e.HrRecordId);

      entity.Property(e => e.Birthidentification)
              .HasMaxLength(100)
              .HasColumnName("BIRTHIDENTIFICATION");
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.DateofBirth).HasColumnType("datetime");
      entity.Property(e => e.DateofMarriage).HasColumnType("datetime");
      entity.Property(e => e.District).HasMaxLength(500);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.FatherName).HasMaxLength(500);
      entity.Property(e => e.FullName).HasMaxLength(500);
      entity.Property(e => e.Gender).HasMaxLength(500);
      entity.Property(e => e.Height)
              .HasMaxLength(50)
              .HasColumnName("HEIGHT");
      entity.Property(e => e.Hobby)
              .HasMaxLength(2000)
              .HasColumnName("HOBBY");
      entity.Property(e => e.HomePhone).HasMaxLength(50);
      entity.Property(e => e.Identificationmark)
              .HasMaxLength(1000)
              .HasColumnName("IDENTIFICATIONMARK");
      entity.Property(e => e.Meritialstatus)
              .HasMaxLength(250)
              .HasColumnName("MERITIALSTATUS");
      entity.Property(e => e.MobileNo).HasMaxLength(500);
      entity.Property(e => e.MotherName).HasMaxLength(500);
      entity.Property(e => e.NationalId)
              .HasMaxLength(250)
              .HasColumnName("NationalID");
      entity.Property(e => e.Nationality).HasMaxLength(500);
      entity.Property(e => e.OriginalBirthDay).HasMaxLength(50);
      entity.Property(e => e.PassportNo).HasMaxLength(250);
      entity.Property(e => e.Passportexpiredate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTEXPIREDATE");
      entity.Property(e => e.Passportissuedate)
              .HasColumnType("datetime")
              .HasColumnName("PASSPORTISSUEDATE");
      entity.Property(e => e.PermanentAddressDistrict).HasMaxLength(500);
      entity.Property(e => e.PermanentAddressThana).HasMaxLength(500);
      entity.Property(e => e.PermanentPostCode).HasMaxLength(50);
      entity.Property(e => e.PersonalEmail).HasMaxLength(250);
      entity.Property(e => e.PlaceofBirth).HasMaxLength(250);
      entity.Property(e => e.Placeofpassportissue)
              .HasMaxLength(250)
              .HasColumnName("PLACEOFPASSPORTISSUE");
      entity.Property(e => e.PresentPostCode).HasMaxLength(50);
      entity.Property(e => e.Profilepicture)
              .HasMaxLength(2000)
              .HasColumnName("PROFILEPICTURE");
      entity.Property(e => e.Religion).HasMaxLength(500);
      entity.Property(e => e.ShortName).HasMaxLength(50);
      entity.Property(e => e.Signature)
              .HasMaxLength(2000)
              .HasColumnName("SIGNATURE");
      entity.Property(e => e.SpouseName).HasMaxLength(500);
      entity.Property(e => e.Thana).HasMaxLength(500);
      entity.Property(e => e.Weight)
              .HasMaxLength(50)
              .HasColumnName("WEIGHT");
    });

    modelBuilder.Entity<EmployeeVariableAllowanceDetails>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeeVariableAllowanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.SubmitDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmployeeVariableAllowanceMaster>(entity =>
    {
      entity.HasKey(e => e.VariableAllowanceMasterId);

      entity.Property(e => e.AllowanceFormDate).HasColumnType("datetime");
      entity.Property(e => e.AllowanceToDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.AvgAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RejectDate).HasColumnType("datetime");
      entity.Property(e => e.SubmitDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmployeeVerificationForm>(entity =>
    {
      entity.Property(e => e.Nidverification).HasColumnName("NIDVerification");
    });

    modelBuilder.Entity<EmployeeWiseOvertimeSettings>(entity =>
    {
      entity.HasKey(e => e.OtsettingsId);

      entity.Property(e => e.OtsettingsId).HasColumnName("OTSettingsId");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeType)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Otamount)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTAmount");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmployeeWiseOvertimeSettingsLog>(entity =>
    {
      entity.HasKey(e => e.OtsettingsId);

      entity.Property(e => e.OtsettingsId).HasColumnName("OTSettingsId");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveEndDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeType)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Otamount)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTAmount");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<EmployeepayrollDetailsVerification>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("EmployeepayrollDetails_Verification");

      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmpPayrollDetailsId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Employeetype>(entity =>
    {
      entity.ToTable("EMPLOYEETYPE");

      entity.Property(e => e.Employeetypeid).HasColumnName("EMPLOYEETYPEID");
      entity.Property(e => e.EmployeeTypeCode).HasMaxLength(50);
      entity.Property(e => e.Employeetypename)
              .HasMaxLength(50)
              .HasColumnName("EMPLOYEETYPENAME");
      entity.Property(e => e.IsContract).HasDefaultValue(false);
    });

    modelBuilder.Entity<Employment>(entity =>
    {
      entity.HasKey(e => e.HrrecordId);

      entity.ToTable(tb =>
              {
              tb.HasTrigger("tr_Employment_LastUpdateDate");
              tb.HasTrigger("tr_Transfer_Promotion_ConfirmationDate");
              tb.HasTrigger("trg_EmploymentHist");
            });

      entity.HasIndex(e => new { e.CompanyId, e.EmploymentDate }, "NonClasterEmploymentIndex");

      entity.HasIndex(e => e.CompanyId, "NonClasterrEmploymentIndex");

      entity.Property(e => e.HrrecordId)
              .ValueGeneratedNever()
              .HasColumnName("HRRecordId");
      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<Employment02072022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Employment_02_07_2022");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<Employment03082022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Employment_03_08_2022");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<Employment13012024BankArch>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Employment_13_01_2024_Bank_ARCH");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<Employment13112023BankArch>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Employment_13_11_2023_Bank_ARCH");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<Employment14052024Poket>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Employment_14_05_2024_POKET");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<Employment21June2022Akhter>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Employment_21_June_2022_Akhter");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<EmploymentHistory>(entity =>
    {
      entity.HasKey(e => e.EmploymentHistoryId).HasName("PK_EmploymentHistory_1");

      entity.HasIndex(e => e.HrrecordId, "NonClusteredIndex_20200930_123009");

      entity.Property(e => e.CompanyName).HasMaxLength(500);
      entity.Property(e => e.Designation).HasMaxLength(500);
      entity.Property(e => e.ExperienceYear).HasMaxLength(500);
      entity.Property(e => e.FromDate).HasMaxLength(50);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.ToDate).HasMaxLength(50);
    });

    modelBuilder.Entity<EmploymentHistoryTemp>(entity =>
    {
      entity.Property(e => e.CompanyName).HasMaxLength(500);
      entity.Property(e => e.Designation).HasMaxLength(500);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.ExperienceYear).HasMaxLength(500);
      entity.Property(e => e.FromDate).HasMaxLength(50);
      entity.Property(e => e.ToDate).HasMaxLength(50);
    });

    modelBuilder.Entity<EmploymentLogHis>(entity =>
    {
      entity.ToTable("Employment_Log_His");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.ContractEndDate).HasColumnType("datetime");
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EffectEndDate).HasColumnType("datetime");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.EmploymentDate).HasColumnType("datetime");
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.JobEndDate).HasColumnType("datetime");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<EmploymentTemp>(entity =>
    {
      entity.HasKey(e => e.HrrecordId);

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Approver).HasMaxLength(500);
      entity.Property(e => e.ApproverDepartmentId).HasMaxLength(500);
      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.Bank).HasMaxLength(500);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.BankBranch).HasMaxLength(500);
      entity.Property(e => e.Branch)
              .HasMaxLength(500)
              .HasColumnName("BRANCH");
      entity.Property(e => e.Company).HasMaxLength(500);
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.ContractEndDate).HasColumnType("datetime");
      entity.Property(e => e.ContractStartDate).HasColumnType("datetime");
      entity.Property(e => e.Department).HasMaxLength(500);
      entity.Property(e => e.Designation)
              .HasMaxLength(500)
              .HasColumnName("DESIGNATION");
      entity.Property(e => e.Division).HasMaxLength(500);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.EmployeeType).HasMaxLength(500);
      entity.Property(e => e.EmploymentDate).HasColumnType("datetime");
      entity.Property(e => e.Facility).HasMaxLength(500);
      entity.Property(e => e.FunctionJobRole)
              .HasMaxLength(500)
              .HasColumnName("Function_JobRole");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.Grade).HasMaxLength(500);
      entity.Property(e => e.IsFieldForce).HasMaxLength(500);
      entity.Property(e => e.IsOteligible)
              .HasMaxLength(500)
              .HasColumnName("IsOTEligible");
      entity.Property(e => e.IsReserved).HasMaxLength(500);
      entity.Property(e => e.JobEndDate).HasColumnType("datetime");
      entity.Property(e => e.JobEndType).HasMaxLength(500);
      entity.Property(e => e.Joiningpost)
              .HasMaxLength(500)
              .HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.PostingType).HasMaxLength(500);
      entity.Property(e => e.ReportTo).HasMaxLength(500);
      entity.Property(e => e.Reportdepid)
              .HasMaxLength(500)
              .HasColumnName("REPORTDEPID");
      entity.Property(e => e.Section).HasMaxLength(500);
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(500);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<EnhancementGuideLine>(entity =>
    {
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ExAppClearMaster>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Clear2Date).HasColumnType("datetime");
      entity.Property(e => e.ClearDate).HasColumnType("datetime");
      entity.Property(e => e.DeptRemarks).HasMaxLength(250);
      entity.Property(e => e.Description).HasMaxLength(250);
      entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ExClearanceParameter>(entity =>
    {
      entity.HasKey(e => e.ParameterId);

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ExClearanceSetupDetail>(entity =>
    {
      entity.HasKey(e => e.ClearanceSetupDetailId);
    });

    modelBuilder.Entity<ExClearanceSetupMaster>(entity =>
    {
      entity.HasKey(e => e.ClearanceSetupMasterId).HasName("PK_ClearanceSetupMaster");

      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.Sequence).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<ExExitInterview>(entity =>
    {
      entity.HasKey(e => e.ExitInterviewFormId);

      entity.Property(e => e.EmployeeSignatureDate).HasColumnType("datetime");
      entity.Property(e => e.InterviewerSignatureDate).HasColumnType("datetime");
      entity.Property(e => e.SalaryHike).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<ExLiabilitiesDetails>(entity =>
    {
      entity.HasKey(e => e.LiabilitiesId);

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ExResignationApplication>(entity =>
    {
      entity.HasKey(e => e.ResignationApplicationId).HasName("PK_ResignationApplicationId");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.AskingSeparationDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DateOfSeparation).HasColumnType("datetime");
      entity.Property(e => e.ExitInterviewStartedDate).HasColumnType("datetime");
      entity.Property(e => e.FinalStatus).HasMaxLength(250);
      entity.Property(e => e.Reason).HasMaxLength(550);
      entity.Property(e => e.Remarks).HasMaxLength(550);
      entity.Property(e => e.ResignationDate).HasColumnType("datetime");
      entity.Property(e => e.ResonForSeparation).HasMaxLength(250);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ExitEmployeeTemp>(entity =>
    {
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.JobEndDate).HasColumnType("datetime");
      entity.Property(e => e.JobEndType).HasMaxLength(50);
      entity.Property(e => e.Status).HasMaxLength(50);
    });

    modelBuilder.Entity<ExternalMovementLog>(entity =>
    {
      entity.HasKey(e => e.MovementId).HasName("PK_External_MovementLog");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Extraciractivities>(entity =>
    {
      entity.ToTable("EXTRACIRACTIVITIES");

      entity.Property(e => e.Extraciractivitiesid).HasColumnName("EXTRACIRACTIVITIESID");
      entity.Property(e => e.Achivement)
              .HasMaxLength(2000)
              .HasColumnName("ACHIVEMENT");
      entity.Property(e => e.Cirtype).HasColumnName("CIRTYPE");
      entity.Property(e => e.Description)
              .HasMaxLength(2000)
              .HasColumnName("DESCRIPTION");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
    });

    modelBuilder.Entity<Facility>(entity =>
    {
      entity.Property(e => e.FacilityCode).HasMaxLength(50);
      entity.Property(e => e.FacilityName).HasMaxLength(500);
    });

    modelBuilder.Entity<Feedback>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.Description).HasMaxLength(500);
      entity.Property(e => e.FeedbackId).ValueGeneratedOnAdd();
      entity.Property(e => e.FeedbackTitle).HasMaxLength(50);
      entity.Property(e => e.Status).HasDefaultValue(0);
    });

    modelBuilder.Entity<FeedbackAnswered>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.FeedbackAnsweredId).ValueGeneratedOnAdd();
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FeedbackDetails>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.FeedbackDetailsId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<FeedbackForEmployee>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AnswerText).HasMaxLength(50);
      entity.Property(e => e.FeedbackForEmployeeId).ValueGeneratedOnAdd();
      entity.Property(e => e.QuestionText).HasMaxLength(50);
      entity.Property(e => e.Status).HasDefaultValue(0);
    });

    modelBuilder.Entity<FeedbackQuestion>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.FeedbackQuestionId).ValueGeneratedOnAdd();
      entity.Property(e => e.QuestionText).HasMaxLength(50);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FeedbackQuestionAnswers>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AnswerText).HasMaxLength(50);
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.FeedbackQuestionAnswerId).ValueGeneratedOnAdd();
      entity.Property(e => e.SortOrder).HasMaxLength(50);
    });

    modelBuilder.Entity<FfAllowanceType>(entity =>
    {
      entity.HasKey(e => e.AllowanceTypeId);

      entity.ToTable("FF_AllowanceType");

      entity.Property(e => e.AllowanceTypeName).HasMaxLength(150);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(150);
      entity.Property(e => e.IsMonthlyDaily).HasColumnName("IsMonthly_Daily");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(150);
    });

    modelBuilder.Entity<FfArea>(entity =>
    {
      entity.HasKey(e => e.AreaId).HasName("PK_PMS_Area");

      entity.ToTable("FF_Area");

      entity.Property(e => e.AreaCode).HasMaxLength(50);
      entity.Property(e => e.AreaName).HasMaxLength(150);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(150);
    });

    modelBuilder.Entity<FfAreaLog>(entity =>
    {
      entity.HasKey(e => e.AreaLogId).HasName("PK_PMS_Area_Log");

      entity.ToTable("FF_Area_Log");

      entity.Property(e => e.AreaCode).HasMaxLength(50);
      entity.Property(e => e.AreaName).HasMaxLength(150);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(150);
    });

    modelBuilder.Entity<FfDailyFieldBenifitMapping>(entity =>
    {
      entity.HasKey(e => e.FfMappingId);

      entity.ToTable("FF_DailyFieldBenifitMapping");

      entity.Property(e => e.FfMappingId).HasColumnName("FF_MappingId");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfEmployeeWiseFieldForceMapping>(entity =>
    {
      entity.HasKey(e => e.EmployeeWiseFieldForceId);

      entity.ToTable("FF_EmployeeWiseFieldForceMapping");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfEmployeeWiseFieldForceMappingHistory>(entity =>
    {
      entity.HasKey(e => e.EmployeeWiseFieldForceHistoryId);

      entity.ToTable("FF_EmployeeWiseFieldForceMappingHistory");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveEndDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfFieldBenifitCategory>(entity =>
    {
      entity.HasKey(e => e.FieldBenifitCategoryId);

      entity.ToTable("FF_FieldBenifitCategory");

      entity.Property(e => e.CategoryName).HasMaxLength(250);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CtcRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfFieldBenifitCategoryHistory>(entity =>
    {
      entity.HasKey(e => e.HistoryId);

      entity.ToTable("FF_FieldBenifitCategoryHistory");

      entity.Property(e => e.CategoryName).HasMaxLength(250);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CtcRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfMonthEndProcess>(entity =>
    {
      entity.ToTable("FF_MonthEndProcess");

      entity.Property(e => e.FfmonthEndProcessId).HasColumnName("FFMonthEndProcessId");
      entity.Property(e => e.AllowanceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfRegion>(entity =>
    {
      entity.HasKey(e => e.RegionId).HasName("PK_PMS_Region");

      entity.ToTable("FF_Region");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.RegionCode).HasMaxLength(50);
      entity.Property(e => e.RegionName).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
    });

    modelBuilder.Entity<FfRegionLog>(entity =>
    {
      entity.HasKey(e => e.RegionLogId).HasName("PK_PMS_Region_Log");

      entity.ToTable("FF_Region_Log");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.RegionCode).HasMaxLength(50);
      entity.Property(e => e.RegionName).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
    });

    modelBuilder.Entity<FfTaDa>(entity =>
    {
      entity.HasKey(e => e.TaDaId);

      entity.ToTable("FF_TA_DA");

      entity.Property(e => e.BenifitRate).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DeleteDate).HasColumnType("datetime");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.TotalDays).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfTaDaHistory>(entity =>
    {
      entity.HasKey(e => e.TaDaIdHistory);

      entity.ToTable("FF_TA_DA_History");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.IsCurrentTerritory).HasMaxLength(50);
      entity.Property(e => e.Tada)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("TADA");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FfTempTada>(entity =>
    {
      entity.HasKey(e => e.Tadaid);

      entity.ToTable("FF_Temp_TADA");

      entity.Property(e => e.Tadaid).HasColumnName("TADAID");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.IsCurrentTerritory).HasMaxLength(50);
      entity.Property(e => e.TotalDays).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<FfTerritory>(entity =>
    {
      entity.HasKey(e => e.TerritoryId).HasName("PK_PMS_Territory");

      entity.ToTable("FF_Territory");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.TerritoryCode).HasMaxLength(50);
      entity.Property(e => e.TerritoryName).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
    });

    modelBuilder.Entity<FfTerritoryLog>(entity =>
    {
      entity.HasKey(e => e.TerritoryLogId).HasName("PK_PMS_Territory_Log");

      entity.ToTable("FF_Territory_Log");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.TerritoryCode).HasMaxLength(50);
      entity.Property(e => e.TerritoryName).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
    });

    modelBuilder.Entity<FfTerritoryWiseCtcAllowanceMapping>(entity =>
    {
      entity.HasKey(e => e.TerritoryWiseAllowanceMappingId).HasName("PK_TerritoryWiseAllowanceMapping");

      entity.ToTable("FF_TerritoryWiseCtc_AllowanceMapping");

      entity.Property(e => e.AllowanceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
    });

    modelBuilder.Entity<FfTerritoryWiseCtcAllowanceMappingHistory>(entity =>
    {
      entity.HasKey(e => e.TerritoryWiseAllowanceMappingHistoryId).HasName("PK_TerritoryWiseAllowanceMapping_History");

      entity.ToTable("FF_TerritoryWiseCtc_AllowanceMapping_History");

      entity.Property(e => e.AllowanceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
    });

    modelBuilder.Entity<FfZone>(entity =>
    {
      entity.HasKey(e => e.ZoneId).HasName("PK_PMS_Zone");

      entity.ToTable("FF_Zone");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
      entity.Property(e => e.ZoneCode).HasMaxLength(50);
      entity.Property(e => e.ZoneName).HasMaxLength(150);
    });

    modelBuilder.Entity<FfZoneLog>(entity =>
    {
      entity.HasKey(e => e.ZoneLogId).HasName("PK_PMS_Zone_Log");

      entity.ToTable("FF_Zone_Log");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CreateUser).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateUser).HasMaxLength(50);
      entity.Property(e => e.ZoneCode).HasMaxLength(50);
      entity.Property(e => e.ZoneName).HasMaxLength(150);
    });

    modelBuilder.Entity<FieldForce>(entity =>
    {
      entity.HasIndex(e => e.FieldForceDate, "FForceDateIndex");

      entity.HasIndex(e => new { e.FieldForceId, e.FieldForceDate, e.HrrecordId, e.CompanyId, e.BranchId }, "NonClusteredIndex_20210704_151755");

      entity.HasIndex(e => new { e.IsPresent, e.FieldForceDate }, "NonClusteredIndex_202312213");

      entity.Property(e => e.FieldForceDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsPresent).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeIn).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeOut).HasMaxLength(50);
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(50)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(50)
              .HasColumnName("RSMRegionCode");
    });

    modelBuilder.Entity<FieldForce1>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForce$");

      entity.Property(e => e.Employeeid).HasMaxLength(255);
    });

    modelBuilder.Entity<FieldForceAbsent>(entity =>
    {
      entity.Property(e => e.AbsentDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<FieldForceAbsentArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForceAbsent_Archive_2020");

      entity.Property(e => e.AbsentDate).HasColumnType("datetime");
      entity.Property(e => e.FieldForceAbsentId).ValueGeneratedOnAdd();
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<FieldForceAbsentArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForceAbsent_Archive_2021");

      entity.Property(e => e.AbsentDate).HasColumnType("datetime");
      entity.Property(e => e.FieldForceAbsentId).ValueGeneratedOnAdd();
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<FieldForceAbsentArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForceAbsent_Archive_2022");

      entity.Property(e => e.AbsentDate).HasColumnType("datetime");
      entity.Property(e => e.FieldForceAbsentId).ValueGeneratedOnAdd();
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<FieldForceAbsentArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForceAbsent_Archive_2023");

      entity.Property(e => e.AbsentDate).HasColumnType("datetime");
      entity.Property(e => e.FieldForceAbsentId).ValueGeneratedOnAdd();
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<FieldForceArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForce_Archive_2020");

      entity.Property(e => e.FieldForceDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsPresent).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeIn).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeOut).HasMaxLength(50);
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(50)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(50)
              .HasColumnName("RSMRegionCode");
    });

    modelBuilder.Entity<FieldForceArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForce_Archive_2021");

      entity.Property(e => e.FieldForceDate).HasColumnType("datetime");
      entity.Property(e => e.FieldForceId).ValueGeneratedOnAdd();
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsPresent).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeIn).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeOut).HasMaxLength(50);
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(50)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(50)
              .HasColumnName("RSMRegionCode");
    });

    modelBuilder.Entity<FieldForceArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForce_Archive_2022");

      entity.Property(e => e.FieldForceDate).HasColumnType("datetime");
      entity.Property(e => e.FieldForceId).ValueGeneratedOnAdd();
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsPresent).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeIn).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeOut).HasMaxLength(50);
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(50)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(50)
              .HasColumnName("RSMRegionCode");
    });

    modelBuilder.Entity<FieldForceArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FieldForce_Archive_2023");

      entity.Property(e => e.FieldForceDate).HasColumnType("datetime");
      entity.Property(e => e.FieldForceId).ValueGeneratedOnAdd();
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GraceIn).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GraceOut).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsPresent).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeIn).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeOut).HasMaxLength(50);
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(50)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(50)
              .HasColumnName("RSMRegionCode");
    });

    modelBuilder.Entity<FieldForceEmployeeTemp>(entity =>
    {
      entity.Property(e => e.EmployeeId).HasMaxLength(150);
    });

    modelBuilder.Entity<FieldForceRsmMappingInfo>(entity =>
    {
      entity.HasKey(e => e.FieldForceRsmMappingId).HasName("PK_FiledForceRsmMappingInfo");

      entity.Property(e => e.DsmlocationCode)
              .HasMaxLength(100)
              .HasColumnName("DSMLocationCode");
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(100)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.PsolocationName)
              .HasMaxLength(100)
              .HasColumnName("PSOLocationName");
      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(100)
              .HasColumnName("RSMRegionCode");
      entity.Property(e => e.RsmregionId).HasColumnName("RSMRegionId");
    });

    modelBuilder.Entity<FieldForceRsmMappingInfoHistory>(entity =>
    {
      entity.HasKey(e => e.FieldForceRsmMappingHistoryId);

      entity.Property(e => e.DsmlocationCode)
              .HasMaxLength(100)
              .HasColumnName("DSMLocationCode");
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveEndDate).HasColumnType("datetime");
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(100)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.PsolocationName)
              .HasMaxLength(100)
              .HasColumnName("PSOLocationName");
      entity.Property(e => e.RsmmanagerHrRecordId).HasColumnName("RSMManagerHrRecordId");
      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(100)
              .HasColumnName("RSMRegionCode");
      entity.Property(e => e.RsmregionId).HasColumnName("RSMRegionId");
    });

    modelBuilder.Entity<FinalSettlement>(entity =>
    {
      entity.HasKey(e => e.SettlementId);

      entity.Property(e => e.ActualCompanyContribution).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ActualCompanyProfit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ActualMemberProfit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ActualOwnContribution).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AuditedProfit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DesignationName).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.EmployeeName).HasMaxLength(500);
      entity.Property(e => e.ExcessLfaamount)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("ExcessLFAAmount");
      entity.Property(e => e.GradeName).HasMaxLength(250);
      entity.Property(e => e.GratuityAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LoanDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetDisbursableAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NoticePayRefund).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Otrate)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTRate");
      entity.Property(e => e.OverTimeAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(10, 2)");
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.PfCompanyContribution).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PfCompanyProfit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PfLoanDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PfMemberContribution).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PfMemberProfit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PfoutStandingTotal)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PFOutStandingTotal");
      entity.Property(e => e.ProratedPfcontribution)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("ProratedPFContribution");
      entity.Property(e => e.ProratedPfinterest)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("ProratedPFInterest");
      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.SalaryAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryAmountPerDay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryOutStandingTotal).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UnAuditedProfit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VoucharNo).HasMaxLength(50);
    });

    modelBuilder.Entity<FinalSettlementArearSalary>(entity =>
    {
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<FinalSettlementDetails>(entity =>
    {
      entity.HasKey(e => e.SettlementDetailId);

      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<FinancerInfo>(entity =>
    {
      entity.HasKey(e => e.FinancerId);

      entity.Property(e => e.FinancerCode).HasMaxLength(50);
      entity.Property(e => e.FinancerName).HasMaxLength(500);
    });

    modelBuilder.Entity<FinancialYear>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.FinancialYearId).ValueGeneratedOnAdd();
      entity.Property(e => e.FinancialYearName).HasMaxLength(50);
    });

    modelBuilder.Entity<FiscalYear>(entity =>
    {
      entity.ToTable("FISCAL_YEAR");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.FiscalYearName).HasMaxLength(100);
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FndProfitLoss>(entity =>
    {
      entity.HasKey(e => e.ProfitLossId);

      entity.Property(e => e.AuditedDate).HasColumnType("datetime");
      entity.Property(e => e.CurrentYearAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PostDate).HasColumnType("datetime");
      entity.Property(e => e.PrevYearAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<FndReceivePayment>(entity =>
    {
      entity.HasKey(e => e.ReceivePaymentId);

      entity.Property(e => e.AuditedDate).HasColumnType("datetime");
      entity.Property(e => e.CurrentYearAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PostDate).HasColumnType("datetime");
      entity.Property(e => e.PrevYearAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<FndTrialBalance>(entity =>
    {
      entity.HasKey(e => e.TrialBalanceId).HasName("PK_TrialBalance");

      entity.Property(e => e.AuditedDate).HasColumnType("datetime");
      entity.Property(e => e.ClosingCredit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ClosingDebit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningCredit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningDebit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PeriodCredit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PeriodDebit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PostDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<FunctionalJob13102022SecA>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("FunctionalJob_13_10_2022_Sec_A");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.FunctionalJob).HasMaxLength(500);
    });

    modelBuilder.Entity<FundYear>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TaxYearId).ValueGeneratedOnAdd();
      entity.Property(e => e.TaxYearName).HasMaxLength(50);
    });

    modelBuilder.Entity<GeneralLedger>(entity =>
    {
      entity.HasKey(e => e.LedgerId);

      entity.Property(e => e.AccountHeadCode).HasMaxLength(50);
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.CrAmount).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.DrAmount).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.LedgerReference).HasMaxLength(50);
      entity.Property(e => e.MotherBalance).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.PostingDate).HasColumnType("datetime");
      entity.Property(e => e.SubLedgerAccountCode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SubLedgerCode).HasMaxLength(50);
      entity.Property(e => e.SubLedgerName).HasMaxLength(500);
    });

    modelBuilder.Entity<GeoFencingUserInfo>(entity =>
    {
      entity.Property(e => e.LoginId).HasMaxLength(50);
      entity.Property(e => e.Password).HasMaxLength(150);
      entity.Property(e => e.Raw)
              .HasMaxLength(150)
              .HasColumnName("raw");
      entity.Property(e => e.Status).HasMaxLength(10);
      entity.Property(e => e.UserName).HasMaxLength(150);
    });

    modelBuilder.Entity<Glsalary>(entity =>
    {
      entity.ToTable("GLSalary");

      entity.Property(e => e.GlsalaryId).HasColumnName("GLSalaryId");
      entity.Property(e => e.GlcompanyId).HasColumnName("GLCompanyId");
      entity.Property(e => e.GltransferType).HasColumnName("GLTransferType");
      entity.Property(e => e.Sjvcode)
              .HasMaxLength(30)
              .IsUnicode(false)
              .HasColumnName("SJVCode");
      entity.Property(e => e.Sjvdesc)
              .HasMaxLength(200)
              .IsUnicode(false)
              .HasColumnName("SJVDesc");
      entity.Property(e => e.Sjvtype).HasColumnName("SJVType");
      entity.Property(e => e.TranId).HasColumnName("TranID");
    });

    modelBuilder.Entity<GlsalaryItem>(entity =>
    {
      entity.ToTable("GLSalaryItem");

      entity.Property(e => e.GlsalaryItemId).HasColumnName("GLSalaryItemId");
      entity.Property(e => e.Adid).HasColumnName("ADID");
      entity.Property(e => e.Description)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.Glcode)
              .HasMaxLength(30)
              .IsUnicode(false)
              .HasColumnName("GLCode");
      entity.Property(e => e.GlcostCentreCode)
              .HasMaxLength(50)
              .HasColumnName("GLCostCentreCode");
      entity.Property(e => e.Side)
              .HasMaxLength(1)
              .IsUnicode(false)
              .IsFixedLength();
      entity.Property(e => e.TranId).HasColumnName("TranID");
    });

    modelBuilder.Entity<GoodsConditionStstus>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("GOODS_CONDITION_STSTUS");

      entity.Property(e => e.GoodsConditionStstusId).HasColumnName("GOODS_CONDITION_STSTUS_ID");
      entity.Property(e => e.Isactive)
              .HasDefaultValue(0)
              .HasColumnName("ISACTIVE");
      entity.Property(e => e.StatusName)
              .HasMaxLength(50)
              .HasColumnName("STATUS_NAME");
    });

    modelBuilder.Entity<Grade>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GradeCode).HasMaxLength(50);
      entity.Property(e => e.GradeId).ValueGeneratedOnAdd();
      entity.Property(e => e.GradeName).HasMaxLength(100);
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncreaseAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxHospitalizationAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<GradeCtc>(entity =>
    {
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<GradeCtcHistory>(entity =>
    {
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<GradeDesignation>(entity =>
    {
      entity.Property(e => e.CtcAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<GradeHistory>(entity =>
    {
      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GradeCode).HasMaxLength(50);
      entity.Property(e => e.GradeName).HasMaxLength(250);
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxHospitalizationAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<GradeLevel>(entity =>
    {
      entity.Property(e => e.GradeLevelName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<GradeSbu>(entity =>
    {
      entity.Property(e => e.CtcAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<GradeSegment>(entity =>
    {
      entity.Property(e => e.GradeSegmentName).HasMaxLength(50);
    });

    modelBuilder.Entity<GradeType>(entity =>
    {
      entity.Property(e => e.GradeTypeName).HasMaxLength(100);
      entity.Property(e => e.Sorder).HasColumnName("SOrder");
    });

    modelBuilder.Entity<GratuityInfomation>(entity =>
    {
      entity.HasKey(e => e.GratuityId);

      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GratuityAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PostedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<GreetingsOrWishNotificationLog>(entity =>
    {
      entity.HasKey(e => e.GreetingsOrWishNotifyId);

      entity.Property(e => e.NotificationType)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<GroupMember>(entity =>
    {
      entity.HasKey(e => new { e.GroupId, e.UserId });

      entity.Property(e => e.GroupOption).HasMaxLength(50);
    });

    modelBuilder.Entity<GroupMemberTemp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("GroupMember_Temp");

      entity.Property(e => e.GroupOption).HasMaxLength(50);
    });

    modelBuilder.Entity<GroupPermission>(entity =>
    {
      entity.HasKey(e => e.PermissionId);

      entity.HasIndex(e => new { e.Permissiontablename, e.Parentpermission }, "Indx_Grp_Per_Table");

      entity.Property(e => e.Groupid).HasColumnName("GROUPID");
      entity.Property(e => e.Parentpermission).HasColumnName("PARENTPERMISSION");
      entity.Property(e => e.Permissiontablename)
              .HasMaxLength(50)
              .HasColumnName("PERMISSIONTABLENAME");
      entity.Property(e => e.Referenceid).HasColumnName("REFERENCEID");
    });

    modelBuilder.Entity<Groups>(entity =>
    {
      entity.HasKey(e => e.GroupId);

      entity.Property(e => e.GroupName).HasMaxLength(100);
    });

    modelBuilder.Entity<HcExpense>(entity =>
    {
      entity.HasKey(e => e.ExpenseTypeId);

      entity.ToTable("HC_Expense");

      entity.Property(e => e.ExpenseType)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Holiday>(entity =>
    {
      entity.HasIndex(e => e.HolidayDate, "IX_Holiday");

      entity.Property(e => e.HolidayId).HasColumnName("HolidayID");
      entity.Property(e => e.DayName)
              .HasMaxLength(20)
              .IsUnicode(false);
      entity.Property(e => e.Description).HasMaxLength(500);
      entity.Property(e => e.Lastupdatedate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.MonthName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
    });

    modelBuilder.Entity<HolidayType>(entity =>
    {
      entity.Property(e => e.Holidaytypeid).HasColumnName("HOLIDAYTYPEID");
      entity.Property(e => e.Holidaytypename)
              .HasMaxLength(100)
              .HasColumnName("HOLIDAYTYPENAME");
    });

    modelBuilder.Entity<HospitailizationNominee>(entity =>
    {
      entity.HasKey(e => e.HospitalizationNomineeId);

      entity.Property(e => e.Age).HasMaxLength(50);
      entity.Property(e => e.BirthDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.NomineeName).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<HospitalInformation>(entity =>
    {
      entity.HasKey(e => e.HospitalId);

      entity.Property(e => e.HospitalName).HasMaxLength(250);
    });

    modelBuilder.Entity<Hospitalization>(entity =>
    {
      entity.Property(e => e.AvailledAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ClaimedAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Relationship).HasMaxLength(100);
      entity.Property(e => e.RemaningBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TreatmentDetails).HasMaxLength(300);
    });

    modelBuilder.Entity<HospitalizationAttachment>(entity =>
    {
      entity.Property(e => e.AttachedDocument).HasMaxLength(200);
      entity.Property(e => e.HospitalizationAttachmentName)
              .HasMaxLength(50)
              .IsUnicode(false);

      entity.HasOne(d => d.Hospitalization).WithMany(p => p.HospitalizationAttachment)
              .HasForeignKey(d => d.HospitalizationId)
              .HasConstraintName("FK_HospitalizationAttachment_Hospitalization1");
    });

    modelBuilder.Entity<HospitalizationClaimDetails>(entity =>
    {
      entity.HasKey(e => e.HospitalizationClaimDetalisId);

      entity.Property(e => e.Conveyance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CostOfMedicine).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DoctorsFee).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Laboratory).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TreatmentDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<HospitalizationClaimMaster>(entity =>
    {
      entity.Property(e => e.ClaimDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<HrDocument>(entity =>
    {
      entity.Property(e => e.DocumentName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.FolderPath)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.ReportFileName)
              .HasMaxLength(200)
              .IsUnicode(false);
    });

    modelBuilder.Entity<HrDocumentSubReport>(entity =>
    {
      entity.HasKey(e => e.HrDocumentSubId);

      entity.Property(e => e.SubReportName)
              .HasMaxLength(200)
              .IsUnicode(false);
    });

    modelBuilder.Entity<HrFunction>(entity =>
    {
      entity.HasKey(e => e.FuncId);

      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.FunctionCode)
              .HasMaxLength(50)
              .HasColumnName("Function_Code");
      entity.Property(e => e.FunctionName)
              .HasMaxLength(100)
              .HasColumnName("Function_Name");
      entity.Property(e => e.JobDescription)
              .HasMaxLength(100)
              .HasColumnName("Job_Description");
    });

    modelBuilder.Entity<HrSignatory>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SignatoryDetails)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.SignatoryName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.SignatoryTitle)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<HrTest>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.LeaveTypeName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.StartDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Hris>(entity =>
    {
      entity.HasKey(e => e.HrisSettingsId);

      entity.Property(e => e.EmployeeOrderSql)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("EmployeeOrderSQL");
      entity.Property(e => e.EmployeeTypeInAchievementState).HasMaxLength(50);
      entity.Property(e => e.EmployeeTypeInTargetState).HasMaxLength(50);
      entity.Property(e => e.IsMultipleApprovalForOtallocation).HasColumnName("IsMultipleApprovalForOTAllocation");
      entity.Property(e => e.IsReportViewerToPdf).HasColumnName("IsReportViewerToPDF");
      entity.Property(e => e.IsWfbalanceCheckingApplicable).HasColumnName("IsWFBalanceCheckingApplicable");
    });

    modelBuilder.Entity<Idpolicy>(entity =>
    {
      entity.HasKey(e => e.IdNo);

      entity.ToTable("IDPolicy");

      entity.Property(e => e.EntityName).HasMaxLength(50);
      entity.Property(e => e.Prefix)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.Suffix)
              .HasMaxLength(10)
              .IsFixedLength();
    });

    modelBuilder.Entity<ImageTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Employeeid)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.FileName)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Id)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("id");
    });

    modelBuilder.Entity<IncrementEligibleNotifyLog>(entity =>
    {
      entity.Property(e => e.BasicAfterIncrement).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.EmployeeCurrentBasic).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<IncrementGuideLine>(entity =>
    {
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<IncrementRateSettings>(entity =>
    {
      entity.HasKey(e => e.IncrementRateId);

      entity.Property(e => e.GradeWiseIncrementRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.GradeWiseStartingBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtherAllowanceGrossLimit).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtherAllowenceAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<IndexSection>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.IndexSectionId).ValueGeneratedOnAdd();
      entity.Property(e => e.IndexSectionName).HasMaxLength(50);
      entity.Property(e => e.IsActive).HasDefaultValue(1);
    });

    modelBuilder.Entity<IndexSectionDetails>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.IndexSectionDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.IndexSectionDetailsName).HasMaxLength(50);
      entity.Property(e => e.IsActive).HasDefaultValue(1);
      entity.Property(e => e.LinkUrl).HasMaxLength(150);
    });

    modelBuilder.Entity<InstituteInstructor>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Institute_Instructor");

      entity.Property(e => e.Address).HasMaxLength(2000);
      entity.Property(e => e.ConductedType)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasComment("1=Institute,2=Instructor");
      entity.Property(e => e.ContactEmail)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.ContactMobile)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ContactPhone)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Designation).HasMaxLength(250);
      entity.Property(e => e.InstituteOrInstructorId).ValueGeneratedOnAdd();
      entity.Property(e => e.Name).HasMaxLength(250);
    });

    modelBuilder.Entity<InterestRateConfiguration>(entity =>
    {
      entity.HasKey(e => e.PfInterestRateId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Interview>(entity =>
    {
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.FirstInterviewDate).HasColumnType("datetime");
      entity.Property(e => e.InterviewDate).HasColumnType("datetime");
      entity.Property(e => e.NoteApproveDate).HasColumnType("datetime");
      entity.Property(e => e.NoteIssueDate).HasColumnType("datetime");
      entity.Property(e => e.RecuitmentJoiningDateNote).HasColumnType("datetime");
      entity.Property(e => e.SecondInterviewDate).HasColumnType("datetime");
      entity.Property(e => e.SelectedDate).HasColumnType("datetime");
      entity.Property(e => e.ThirdInterviewDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<InterviewHistory>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Interview_History");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.FirstInterviewDate).HasColumnType("datetime");
      entity.Property(e => e.InterviewDate).HasColumnType("datetime");
      entity.Property(e => e.NoteApproveDate).HasColumnType("datetime");
      entity.Property(e => e.NoteIssueDate).HasColumnType("datetime");
      entity.Property(e => e.RecuitmentJoiningDateNote).HasColumnType("datetime");
      entity.Property(e => e.RecuitmentRemarksNote).HasMaxLength(350);
      entity.Property(e => e.SecondInterviewDate).HasColumnType("datetime");
      entity.Property(e => e.SelectedDate).HasColumnType("datetime");
      entity.Property(e => e.ThirdInterviewDate).HasColumnType("datetime");
      entity.Property(e => e.Vanue).HasMaxLength(500);
    });

    modelBuilder.Entity<InterviewMarking>(entity =>
    {
      entity.HasKey(e => e.MarkingId);

      entity.Property(e => e.MarkingTitle)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<InterviewRatingDetails>(entity =>
    {
      entity.HasKey(e => e.InterviewRatingDetailsId).HasName("PK_IntervieRatingDetails");

      entity.Property(e => e.Rating).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<InterviewSelectionApprover>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrRecordId, e.ModuleId, e.SortOrder, e.Type }, "IX_DuplicateInterviewApprover").IsUnique();

      entity.Property(e => e.AssignApproverId).ValueGeneratedOnAdd();
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<InterviewSelectionApproverDetails>(entity =>
    {
      entity.HasKey(e => e.SelectionRemarksId).HasName("PK_SelectionRemarksId");

      entity.Property(e => e.SelectionApprovedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Investment>(entity =>
    {
      entity.Property(e => e.AccountNo).HasMaxLength(250);
      entity.Property(e => e.ActualProfitRate).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InvestAccCode).HasMaxLength(250);
      entity.Property(e => e.InvestmentCode)
              .HasMaxLength(5)
              .IsUnicode(false);
      entity.Property(e => e.PostingDate).HasColumnType("datetime");
      entity.Property(e => e.PrincipalAmt).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.ProfitAccCode).HasMaxLength(250);
      entity.Property(e => e.ProfitAmt).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.SubLedgerAccountCode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.TrusteeBankAccCode).HasMaxLength(250);
      entity.Property(e => e.VoucherNo)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<InvestmentAdjustment>(entity =>
    {
      entity.HasKey(e => e.InvestAdustmentId);

      entity.Property(e => e.PostingDate).HasColumnType("datetime");
      entity.Property(e => e.VoucherNo)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<InvestmentDetails>(entity =>
    {
      entity.Property(e => e.MonthlyInterest).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VoucherNo)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<InvestmentEncash>(entity =>
    {
      entity.Property(e => e.AccProfitAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AcruedProfitAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PostingDate).HasColumnType("datetime");
      entity.Property(e => e.PrincipleAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VarienceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VoucherNo)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<InvestmentInformation>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ActualRateOfPfinterest)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("ActualRateOfPFInterest");
      entity.Property(e => e.ConsidarableTaxableIncome).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InvestmentId).ValueGeneratedOnAdd();
      entity.Property(e => e.LessTaxPfofBasic)
              .HasColumnType("decimal(18, 4)")
              .HasColumnName("LessTaxPFOfBasic");
      entity.Property(e => e.MaxExamptedAmountForTotalIncome).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxExamptedPercentageForTotalIncome).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OldAgeRestriction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OrPfInterest)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("orPfInterest");
    });

    modelBuilder.Entity<InvestmentRenewal>(entity =>
    {
      entity.Property(e => e.RenewDate).HasColumnType("datetime");
      entity.Property(e => e.RenewInterest).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<InvestmentSlabSettings>(entity =>
    {
      entity.HasKey(e => e.InvestmentSlabId);

      entity.Property(e => e.AllowableInvestment).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RangeAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RangePercentage).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SlabTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<InvestmentType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.InvestmentTypeId).ValueGeneratedOnAdd();
      entity.Property(e => e.InvestmentTypeName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<InvoiceInfo>(entity =>
    {
      entity.HasKey(e => e.InvoiceId);

      entity.Property(e => e.Comments).HasColumnType("text");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CustomerCode).HasMaxLength(100);
      entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.InvPrintDate).HasColumnType("datetime");
      entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
      entity.Property(e => e.InvoiceNo).HasMaxLength(100);
      entity.Property(e => e.PurchaseOrderDate).HasColumnType("datetime");
      entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<InvoiceOrderDetails>(entity =>
    {
      entity.HasKey(e => e.InvoiceOrderId);

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<IvmsVisitDetails>(entity =>
    {
      entity.HasKey(e => e.VisitDetailsId).HasName("PK_IVMS_VisitDetails");

      entity.ToTable("IVMS-VisitDetails");

      entity.Property(e => e.AreaAllowed).HasMaxLength(150);
      entity.Property(e => e.PassId).HasMaxLength(15);
      entity.Property(e => e.TimeFrom).HasColumnType("datetime");
      entity.Property(e => e.TimeTo).HasColumnType("datetime");
      entity.Property(e => e.VehicleNo).HasMaxLength(15);
    });

    modelBuilder.Entity<IvmsVisitor>(entity =>
    {
      entity.HasKey(e => e.VisitorId).HasName("PK_IVMS_Visitor");

      entity.ToTable("IVMS-Visitor");

      entity.Property(e => e.Address)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.Country)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Email)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Fax)
              .HasMaxLength(15)
              .IsUnicode(false);
      entity.Property(e => e.Gender)
              .HasMaxLength(10)
              .IsUnicode(false);
      entity.Property(e => e.MobileNo)
              .HasMaxLength(11)
              .IsUnicode(false);
      entity.Property(e => e.PassportNo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Phone)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Photo)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.Purpose)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Status).HasComment("0=Waiting, 1=Approved, 2= Deny");
      entity.Property(e => e.VisaNo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.VisaType)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.VisitorCompany)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.VisitorName)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<JobCode>(entity =>
    {
      entity.HasKey(e => e.JobId);

      entity.Property(e => e.Code).HasMaxLength(50);
      entity.Property(e => e.CognosCode).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
    });

    modelBuilder.Entity<JobCode15012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("JobCode_15_01_2024");

      entity.Property(e => e.Code).HasMaxLength(50);
      entity.Property(e => e.CognosCode).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.JobId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<JobCode20012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("JobCode_20_01_2024");

      entity.Property(e => e.Code).HasMaxLength(50);
      entity.Property(e => e.CognosCode).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.JobId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<JobConfirmationEvaluation>(entity =>
    {
      entity.HasKey(e => e.JobConfirmationEvaluateId);

      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");
      entity.Property(e => e.EffectiveFromMto)
              .HasColumnType("datetime")
              .HasColumnName("EffectiveFromMTO");
      entity.Property(e => e.EffectiveFromNonMng).HasColumnType("datetime");
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
      entity.Property(e => e.FinalApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<JobConfirmationEvaluationHistory>(entity =>
    {
      entity.HasKey(e => e.JobConfirmationEvaluateHistoryId);

      entity.Property(e => e.JobConfirmationEvaluateHistoryId).ValueGeneratedNever();
      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");
      entity.Property(e => e.EffectiveFromMto)
              .HasColumnType("datetime")
              .HasColumnName("EffectiveFromMTO");
      entity.Property(e => e.EffectiveFromNonMng).HasColumnType("datetime");
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
      entity.Property(e => e.FinalApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<JobConfirmationMaster>(entity =>
    {
      entity.ToTable(tb => tb.HasTrigger("tr_JobConfirmationMaster"));

      entity.Property(e => e.DocumentFilePath).HasColumnName("DocumentFIlePath");
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobConfirmationMaster25032023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("JobConfirmationMaster_25_03_2023");

      entity.Property(e => e.DocumentFilePath).HasColumnName("DocumentFIlePath");
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
      entity.Property(e => e.JobConfirmationMasterId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobConfirmationMaster31012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("JobConfirmationMaster_31_01_2024");

      entity.Property(e => e.DocumentFilePath).HasColumnName("DocumentFIlePath");
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
      entity.Property(e => e.JobConfirmationMasterId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobConfirmationMasterHistory>(entity =>
    {
      entity.Property(e => e.JobConfirmationMasterHistoryId).ValueGeneratedNever();
      entity.Property(e => e.DocumentFilePath).HasColumnName("DocumentFIlePath");
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobConfirmationMasterRejectedInfo>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("JobConfirmationMaster_RejectedInfo");

      entity.Property(e => e.DocumentFilePath).HasColumnName("DocumentFIlePath");
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobConfirmationPayrollDetails>(entity =>
    {
      entity.HasKey(e => e.JobConfirmationPayrollDetailId);
    });

    modelBuilder.Entity<JobConfirmationReview>(entity =>
    {
      entity.Property(e => e.EvaluationDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobEndType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.JobEndTypeId).ValueGeneratedOnAdd();
      entity.Property(e => e.JobEndTypeName)
              .HasMaxLength(30)
              .IsUnicode(false);
    });

    modelBuilder.Entity<JobPerformanceDetails>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Description).HasMaxLength(2050);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobPerformanceDetailsLog>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Description).HasMaxLength(2050);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<JobPosition>(entity =>
    {
      entity.Property(e => e.JobPositionName).HasMaxLength(500);
    });

    modelBuilder.Entity<JobRecommendationType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.JobRecommendationTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<JobResponsibility>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Responsibility)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.ResponsibilityId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<JoiningBasicInformation>(entity =>
    {
      entity.HasKey(e => e.JoningBasicId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningBasicAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Kpi>(entity =>
    {
      entity.ToTable("KPI");

      entity.Property(e => e.KpiId).HasColumnName("KPI_ID");
      entity.Property(e => e.KpiName)
              .HasMaxLength(100)
              .HasColumnName("KPI_NAME");
      entity.Property(e => e.SortOrder).HasColumnName("SORT_ORDER");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.TotalMarks).HasColumnName("TOTAL_MARKS");
    });

    modelBuilder.Entity<KpiGrade>(entity =>
    {
      entity.ToTable("KPI_GRADE");

      entity.Property(e => e.KpiGradeId).HasColumnName("KPI_GRADE_ID");
      entity.Property(e => e.KpiGradeMarks)
              .HasDefaultValueSql("((0.0))")
              .HasColumnName("KPI_GRADE_MARKS");
      entity.Property(e => e.KpiGradeName)
              .HasMaxLength(100)
              .HasColumnName("KPI_GRADE_NAME");
      entity.Property(e => e.SortOrder).HasColumnName("SORT_ORDER");
      entity.Property(e => e.Status).HasColumnName("STATUS");
    });

    modelBuilder.Entity<KpiyearConfigWithGrade>(entity =>
    {
      entity.HasKey(e => e.KpiYearGradeMapId);

      entity.ToTable("KPIYearConfigWithGrade");
    });

    modelBuilder.Entity<KpiyearConfiguration>(entity =>
    {
      entity.HasKey(e => e.YearConfigId);

      entity.ToTable("KPIYearConfiguration");

      entity.HasIndex(e => e.EndDate, "NonClusteredIndex_EndDate").IsUnique();

      entity.HasIndex(e => e.StartDate, "NonClusteredIndex_StartDate").IsUnique();
    });

    modelBuilder.Entity<KraAssessmentBehaviour>(entity =>
    {
      entity.HasKey(e => e.KraAssessmentId);

      entity.Property(e => e.Definition).HasMaxLength(1000);
      entity.Property(e => e.Title)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.ValueType).HasComment("1=Common role Behaviour,2=Values Assessment");
    });

    modelBuilder.Entity<KraAssessmentRating>(entity =>
    {
      entity.HasOne(d => d.KraAssessment).WithMany(p => p.KraAssessmentRating)
              .HasForeignKey(d => d.KraAssessmentId)
              .HasConstraintName("FK_KraAssessmentRating_KraAssessmentBehaviour");

      entity.HasOne(d => d.KraCompetencies).WithMany(p => p.KraAssessmentRating)
              .HasForeignKey(d => d.KraCompetenciesId)
              .HasConstraintName("FK_KraAssessmentRating_KraCompetencies");
    });

    modelBuilder.Entity<KraCompetencies>(entity =>
    {
      entity.HasKey(e => e.KraCompetenciesId).HasName("PK_KraPartC");

      entity.Property(e => e.CommentsByEmployee).HasColumnType("text");
      entity.Property(e => e.CommentsByLineManager).HasColumnType("text");
      entity.Property(e => e.LastYearKey).HasColumnType("text");
      entity.Property(e => e.ReviewDate).HasColumnType("datetime");

      entity.HasOne(d => d.KraPerformance).WithMany(p => p.KraCompetencies)
              .HasForeignKey(d => d.KraPerformanceId)
              .HasConstraintName("FK_KraCompetencies_KraPerformance");
    });

    modelBuilder.Entity<KraCompetencyRating>(entity =>
    {
      entity.HasKey(e => e.KraCompetencyId);

      entity.HasIndex(e => new { e.KraCompetenciesId, e.SectionId }, "IX_KraCompetencyRating").IsUnique();

      entity.HasOne(d => d.KraCompetencies).WithMany(p => p.KraCompetencyRating)
              .HasForeignKey(d => d.KraCompetenciesId)
              .HasConstraintName("FK_KraCompetencyRating_KraPerformance");
    });

    modelBuilder.Entity<KraDevelopmentPlan>(entity =>
    {
      entity.HasKey(e => e.DevelopmentPlanId);

      entity.Property(e => e.PlanInitiative).HasMaxLength(1000);
      entity.Property(e => e.PlanWhen).HasColumnType("datetime");

      entity.HasOne(d => d.KraCompetencies).WithMany(p => p.KraDevelopmentPlan)
              .HasForeignKey(d => d.KraCompetenciesId)
              .HasConstraintName("FK_KraDevelopmentPlan_KraCompetencies");
    });

    modelBuilder.Entity<KraOffDevelopmentActivities>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.OffDevActivityId).ValueGeneratedOnAdd();
      entity.Property(e => e.Priority).HasComment("1=High,2=Medium,3=Low");
      entity.Property(e => e.TrainingName).HasMaxLength(500);
      entity.Property(e => e.TrainingSource)
              .HasMaxLength(100)
              .IsUnicode(false);

      entity.HasOne(d => d.KraCompetencies).WithMany()
              .HasForeignKey(d => d.KraCompetenciesId)
              .HasConstraintName("FK_KraDevelopmentActivity_KraDevelopmentActivity");
    });

    modelBuilder.Entity<KraOnDevelopmentActivities>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ActivityHow).HasMaxLength(1000);
      entity.Property(e => e.ActivityWhat).HasMaxLength(500);
      entity.Property(e => e.ActivityWhen).HasColumnType("datetime");
      entity.Property(e => e.OnDevActivityId).ValueGeneratedOnAdd();

      entity.HasOne(d => d.KraCompetencies).WithMany()
              .HasForeignKey(d => d.KraCompetenciesId)
              .HasConstraintName("FK_KraOnDevelopmentActivities_KraCompetencies");
    });

    modelBuilder.Entity<KraPerformance>(entity =>
    {
      entity.Property(e => e.BeforApproveScore).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.TotalAchivementA).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.TotalAchivementB).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.TotalScore).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.TotalWeightPartA).HasColumnType("decimal(18, 4)");
      entity.Property(e => e.TotalWeightPartB).HasColumnType("decimal(18, 4)");
    });

    modelBuilder.Entity<KraPerformanceDetails>(entity =>
    {
      entity.HasKey(e => e.KraPerformanceDetailId);

      entity.Property(e => e.AchivementPoint)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Considaration).HasDefaultValue(1);
      entity.Property(e => e.EligibleFactor)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.KeyArea).HasColumnType("text");
      entity.Property(e => e.KraType).HasComment("1=Part A,2=Part B");
      entity.Property(e => e.MidYearReview).HasColumnType("text");
      entity.Property(e => e.SerialNo)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasDefaultValueSql("((0))");
      entity.Property(e => e.Target).HasColumnType("text");
      entity.Property(e => e.Weight).HasColumnType("decimal(18, 3)");
      entity.Property(e => e.YearEndAchivement).HasColumnType("text");
    });

    modelBuilder.Entity<KraRemarksHistory>(entity =>
    {
      entity.HasKey(e => e.KraRemarksId);

      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.RemarksDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<KraYearAndGradeMap>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.KraYearAndGradeMapId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<KraYearConfigaration>(entity =>
    {
      entity.HasKey(e => e.YearConfigId);

      entity.Property(e => e.ConfigCode).HasMaxLength(5);
      entity.Property(e => e.ConfigName).HasMaxLength(250);
      entity.Property(e => e.MaximumAchivementPoint).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaximumScore).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MinimumAchivementPoint).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PartAweight)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PartAWeight");
      entity.Property(e => e.PartBweight)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PartBWeight");
    });

    modelBuilder.Entity<LateAttendanceThreeTimesNotifyLog>(entity =>
    {
      entity.HasKey(e => e.LateAttendanceId);

      entity.Property(e => e.AdjustmentDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LatePolicy>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.FirstLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FirstLateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FirstLateMin).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LatePolicyName).HasMaxLength(50);
      entity.Property(e => e.SecoundLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecoundLateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SecoundLateMin).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThirdLateMin).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LatePolicyMaping>(entity =>
    {
      entity.HasKey(e => e.LatePolicyMappingId);

      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeaveAdjustment>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.ForTheMonthYear).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveAdjustmentId).ValueGeneratedOnAdd();
      entity.Property(e => e.LeaveIncress).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.RecommanderRemarks)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<LeaveAdjustmentInfo>(entity =>
    {
      entity.HasKey(e => e.LeaveAdjustmentId);

      entity.Property(e => e.ClosingLeaveBalanceNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ClosingLeaveBalanceOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForwardNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForwardOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeductedNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeductedOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoiedNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoiedOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalanceNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalanceOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateType).HasComment("1=Insert, 2=Update, 3=Delete");
    });

    modelBuilder.Entity<LeaveAdjustmentInfoArchieve>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveAdjustmentInfo_Archieve");

      entity.Property(e => e.ClosingLeaveBalanceNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ClosingLeaveBalanceOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveAdjustmentId).ValueGeneratedOnAdd();
      entity.Property(e => e.LeaveBroughtForwardNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForwardOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeductedNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeductedOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoiedNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoiedOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalanceNew).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalanceOld).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeaveApplication>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrrecordId, e.StateId, e.LeaveId, e.LeaveFrom, e.LeaveTo, e.LeaveDays, e.ReferenceId }, "IX_LeaveApplication_1");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteReason)
              .HasMaxLength(400)
              .HasColumnName("Delete_Reason");
      entity.Property(e => e.DeleteTime)
              .HasColumnType("datetime")
              .HasColumnName("Delete_Time");
      entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplication01102023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_01_10_2023");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteReason)
              .HasMaxLength(400)
              .HasColumnName("Delete_Reason");
      entity.Property(e => e.DeleteTime)
              .HasColumnType("datetime")
              .HasColumnName("Delete_Time");
      entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplication04112023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_04_11_2023");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteReason)
              .HasMaxLength(400)
              .HasColumnName("Delete_Reason");
      entity.Property(e => e.DeleteTime)
              .HasColumnType("datetime")
              .HasColumnName("Delete_Time");
      entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplicationArchive2017>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_Archive_2017");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplicationArchive2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_Archive_2018");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplicationArchive2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_Archive_2019");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplicationArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_Archive_2020");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplicationArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_Archive_2021");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteReason)
              .HasMaxLength(400)
              .HasColumnName("Delete_Reason");
      entity.Property(e => e.DeleteTime)
              .HasColumnType("datetime")
              .HasColumnName("Delete_Time");
      entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplicationArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_Archive_2022");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteReason)
              .HasMaxLength(400)
              .HasColumnName("Delete_Reason");
      entity.Property(e => e.DeleteTime)
              .HasColumnType("datetime")
              .HasColumnName("Delete_Time");
      entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveApplicationArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveApplication_Archive_2023");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteReason)
              .HasMaxLength(400)
              .HasColumnName("Delete_Reason");
      entity.Property(e => e.DeleteTime)
              .HasColumnType("datetime")
              .HasColumnName("Delete_Time");
      entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.Entitlement).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveId).ValueGeneratedOnAdd();
      entity.Property(e => e.MedicalCertificatePath).IsUnicode(false);
      entity.Property(e => e.PerformedBy)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.PerformedDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(2000);
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasColumnType("ntext");
    });

    modelBuilder.Entity<LeaveBalance>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrrecordId, e.LeaveType, e.FiscalYearId }, "IX_LeaveBalanceDuplicate").IsUnique();

      entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.ClosingLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeducted).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoied).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
    });

    modelBuilder.Entity<LeaveBalance01102023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveBalance_01_10_2023");

      entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.ClosingLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeducted).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoied).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
    });

    modelBuilder.Entity<LeaveBalance03082022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveBalance_03_08_2022");

      entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.ClosingLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeducted).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoied).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
    });

    modelBuilder.Entity<LeaveBalance04092022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveBalance_04_09_2022");

      entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.ClosingLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeducted).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoied).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
    });

    modelBuilder.Entity<LeaveBalance10072023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveBalance_10_07_2023");

      entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.ClosingLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeducted).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoied).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OpeningLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(200);
    });

    modelBuilder.Entity<LeaveBalance2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveBalance_2019");

      entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
      entity.Property(e => e.ClosingLeaveBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveDeducted).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEnjoied).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveYearFrom).HasColumnType("datetime");
      entity.Property(e => e.LeaveYearTo).HasColumnType("datetime");
      entity.Property(e => e.OpeningLeaveBalance).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<LeaveCoffMap>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.CoffId, e.LeaveId }, "NonClusteredIndex_20231202_095614");

      entity.Property(e => e.CoffId).HasColumnName("COffId");
      entity.Property(e => e.LeaveCoffMapId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveCostCentreMap>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.LeaveCcmapId)
              .ValueGeneratedOnAdd()
              .HasColumnName("LeaveCCMapId");
    });

    modelBuilder.Entity<LeaveDedPolicySbu>(entity =>
    {
      entity.ToTable("LeaveDedPolicySBU");

      entity.Property(e => e.LeaveDedPolicySbuid).HasColumnName("LeaveDedPolicySBUId");
    });

    modelBuilder.Entity<LeaveDeductionInformation>(entity =>
    {
      entity.HasKey(e => e.LeaveDeductionInfoId);

      entity.Property(e => e.AttendanceMonth).HasColumnType("datetime");
      entity.Property(e => e.DeductedLeave).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProcessDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeaveDeductionPolicy>(entity =>
    {
      entity.HasKey(e => e.LeaveDedPolicyId);

      entity.Property(e => e.PolicyName).HasMaxLength(250);
    });

    modelBuilder.Entity<LeaveEncashment>(entity =>
    {
      entity.HasKey(e => e.EncashmentId);

      entity.Property(e => e.AccumulationDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.EncashmentAmountPerDay).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentType).HasComment("1=Encashment, 2=Forwarding");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveType).HasComment("1=Anual, 2=Casual, 3=Medical, 4=C-Off, 5=Without Pay");
      entity.Property(e => e.LeaveYearForm).HasColumnType("datetime");
      entity.Property(e => e.LeaveYearTo).HasColumnType("datetime");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PrevLeaveEncashmentAmt).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<LeaveEncashmentCostCenter>(entity =>
    {
      entity.HasKey(e => e.LeaveEncashmentCostId);
    });

    modelBuilder.Entity<LeaveEncashmentPolicy>(entity =>
    {
      entity.HasKey(e => e.EncashmentPolicyId);
    });

    modelBuilder.Entity<LeaveEncashmentReferenceNo>(entity =>
    {
      entity.HasNoKey();
    });

    modelBuilder.Entity<LeaveEncashmentTemp>(entity =>
    {
      entity.HasKey(e => e.LeaveEncashmentId);

      entity.Property(e => e.CurrentBasic).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.EncashmentAmountPerDay).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PrevLeaveEncashmentAmt).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<LeaveFormAndCompanyMapping>(entity =>
    {
      entity.Property(e => e.LeaveFormForSector).HasMaxLength(50);
    });

    modelBuilder.Entity<LeaveGradeMap>(entity =>
    {
      entity.HasKey(e => e.LeaveDesignationMapId).HasName("PK_LeaveDesignationMap");
    });

    modelBuilder.Entity<LeavePlaned>(entity =>
    {
      entity.HasKey(e => e.LeavePlaneId);

      entity.ToTable(tb =>
              {
              tb.HasTrigger("AutoApprovalAfterLeavePlanSubmit");
              tb.HasTrigger("UpdateAppliedDateAfterInsertLeavePlaned");
              tb.HasTrigger("UpdateApprovedDateAfterLeavePlanedApproval");
            });

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.IsApproved).HasDefaultValue(0);
      entity.Property(e => e.PlaneDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeavePlaned01102023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Leave_Planed_01_10_2023");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.LeavePlaneId).ValueGeneratedOnAdd();
      entity.Property(e => e.PlaneDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeavePlaned02092023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeavePlaned_02_09_2023");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.LeavePlaneId).ValueGeneratedOnAdd();
      entity.Property(e => e.PlaneDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeavePlanedSubmitToDeptHead>(entity =>
    {
      entity.HasKey(e => e.LeavePlanedSubmitId);
    });

    modelBuilder.Entity<LeavePolicy>(entity =>
    {
      entity.HasKey(e => e.LeavePolicyId).HasName("PK_CompanyLeave");

      entity.Property(e => e.EncashmentType).HasComment("0=none;1=Full;2=Half");
      entity.Property(e => e.IsBasicForFsencashment).HasColumnName("IsBasicForFSEncashment");
      entity.Property(e => e.IsLfaapplicable).HasColumnName("IsLFAApplicable");
      entity.Property(e => e.LeavePolicyName)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.LeaveTypeId).HasComment("Casual Leave, Medical Leave, Annual Leave");
      entity.Property(e => e.LfaminimumDays).HasColumnName("LFAMinimumDays");
      entity.Property(e => e.ShortLeaveDuration).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<LeavePolicyDetails>(entity =>
    {
      entity.Property(e => e.Gender).HasComment("1=Male;2=Female; 3 = both");
    });

    modelBuilder.Entity<LeavePolicyException>(entity =>
    {
      entity.HasKey(e => e.ExceptionId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeaveReason>(entity =>
    {
      entity.Property(e => e.Reason)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<LeaveTemp>(entity =>
    {
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(50)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.LeaveType).HasMaxLength(250);
    });

    modelBuilder.Entity<LeaveTemp1>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveTemp$");

      entity.Property(e => e.EmpId).HasMaxLength(255);
      entity.Property(e => e.LeaveType).HasMaxLength(255);
    });

    modelBuilder.Entity<LeaveType>(entity =>
    {
      entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
      entity.Property(e => e.Leavetypecode)
              .HasMaxLength(5)
              .HasColumnName("LEAVETYPECODE");
      entity.Property(e => e.Typename)
              .HasMaxLength(100)
              .HasColumnName("TYPENAME");
    });

    modelBuilder.Entity<LeaveWithoutPay>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrRecordId, e.LeaveWithoutPay1, e.SalaryMonth }, "NOnClasterLeaveWithoutPayIndex");

      entity.Property(e => e.ChannelId).HasComment("1=SystemGenerated,2=Upload");
      entity.Property(e => e.LeaveWithoutId).ValueGeneratedOnAdd();
      entity.Property(e => e.LeaveWithoutPay1)
              .HasColumnType("decimal(18, 1)")
              .HasColumnName("LeaveWithoutPay");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<LeaveYearEndProcess>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndProcessId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveYearEndProcess11012022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveYearEndProcess_11_01_2022");

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndProcessId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveYearEndProcess14012024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveYearEndProcess_14_01_2024");

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndProcessId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveYearEndProcess2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveYearEndProcess_2019");

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndProcessId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveYearEndProcess2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveYearEndProcess_2020");

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<LeaveYearEndProcess2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveYearEndProcess_2021");

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndProcessId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveYearEndProcess2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveYearEndProcess_2022");

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndProcessId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveYearEndProcess2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LeaveYearEndProcess_2023");

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndProcessId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<LeaveYearEndProcessTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EncashmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveAvaill).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveBroughtForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NextYearCarryForward).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NormalLeaveDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PreviousYearOpeningBalance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalLeaveBalanceForNextYear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.YearEndBalance).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<LedgerHead>(entity =>
    {
      entity.Property(e => e.LedgerHeadName).HasMaxLength(50);
      entity.Property(e => e.MappingTable).HasMaxLength(250);
    });

    modelBuilder.Entity<LetterGeneration>(entity =>
    {
      entity.Property(e => e.LevelOf).HasMaxLength(1000);
      entity.Property(e => e.Responsibility).HasMaxLength(1000);
    });

    modelBuilder.Entity<LetterInfoJobConfirmation>(entity =>
    {
      entity.HasKey(e => e.LetterInfoId);

      entity.ToTable("LetterInfo_JobConfirmation");

      entity.Property(e => e.LetterIssueDate).HasColumnType("datetime");
      entity.Property(e => e.ReferenceNo).HasMaxLength(3000);
      entity.Property(e => e.SignatoryDesignation).HasMaxLength(100);
      entity.Property(e => e.SignatoryId).HasMaxLength(50);
      entity.Property(e => e.SignatoryName).HasMaxLength(250);
    });

    modelBuilder.Entity<LetterInfoResignation>(entity =>
    {
      entity.HasKey(e => e.LetterInfoId);

      entity.ToTable("LetterInfo_Resignation");

      entity.Property(e => e.LetterIssueDate).HasColumnType("datetime");
      entity.Property(e => e.ReferenceNo).HasMaxLength(3000);
      entity.Property(e => e.SignatoryDesignation).HasMaxLength(100);
      entity.Property(e => e.SignatoryId).HasMaxLength(50);
      entity.Property(e => e.SignatoryName).HasMaxLength(250);
    });

    modelBuilder.Entity<Lfaapplication>(entity =>
    {
      entity.ToTable("LFAApplication");

      entity.Property(e => e.LfaapplicationId).HasColumnName("LFAApplicationId");
      entity.Property(e => e.LfaapproveBy).HasColumnName("LFAApproveBy");
      entity.Property(e => e.LfaapproveDate).HasColumnName("LFAApproveDate");
      entity.Property(e => e.LfarecommendedBy).HasColumnName("LFARecommendedBy");
      entity.Property(e => e.LfarecommendedDate).HasColumnName("LFARecommendedDate");
      entity.Property(e => e.LfastateId).HasColumnName("LFAStateId");
      entity.Property(e => e.LfasubmitDate).HasColumnName("LFASubmitDate");
      entity.Property(e => e.LfatotalAmount)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("LFATotalAmount");
      entity.Property(e => e.TextFileGenerateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<LfaapplicationDetails>(entity =>
    {
      entity.HasKey(e => e.LfaappDetailsId);

      entity.ToTable("LFAApplicationDetails");

      entity.Property(e => e.LfaappDetailsId).HasColumnName("LFAAppDetailsId");
      entity.Property(e => e.Ctcid).HasColumnName("CTCId");
      entity.Property(e => e.Ctcvalue)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CTCValue");
      entity.Property(e => e.LfaapplicationId).HasColumnName("LFAApplicationId");
    });

    modelBuilder.Entity<LoanEarlySettlement>(entity =>
    {
      entity.HasKey(e => e.LoanSettlementId);

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.VoucherNo).HasMaxLength(50);
    });

    modelBuilder.Entity<LoanInformation>(entity =>
    {
      entity.HasKey(e => e.LoanId);

      entity.HasIndex(e => new { e.LoanId, e.CompanyId, e.HrRecordId, e.BranchId }, "IX_LoanInformation");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ConsideredInterestRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ConsideredLoanAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DisburshmentDate).HasColumnType("datetime");
      entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InstalmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RecoveryStartDate).HasColumnType("datetime");
      entity.Property(e => e.RejectDate).HasColumnType("datetime");
      entity.Property(e => e.ScheduleDate).HasColumnType("datetime");
      entity.Property(e => e.TotalPaid).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VoucherNo).HasMaxLength(100);

      entity.HasOne(d => d.LoanType).WithMany(p => p.LoanInformation)
              .HasForeignKey(d => d.LoanTypeId)
              .HasConstraintName("FK_LoanInformation_LoanType");

      entity.HasOne(d => d.Status).WithMany(p => p.LoanInformation)
              .HasForeignKey(d => d.StatusId)
              .HasConstraintName("FK_LoanInformation_WFState");
    });

    modelBuilder.Entity<LoanInformationAudit>(entity =>
    {
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ConsideredInterestRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ConsideredLoanAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DisburshmentDate).HasColumnType("datetime");
      entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InstalmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RecoveryStartDate).HasColumnType("datetime");
      entity.Property(e => e.RejectDate).HasColumnType("datetime");
      entity.Property(e => e.ScheduleDate).HasColumnType("datetime");
      entity.Property(e => e.TotalPaid).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VoucherNo).HasMaxLength(100);
    });

    modelBuilder.Entity<LoanPurpose>(entity =>
    {
      entity.Property(e => e.PurposeName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<LoanSchedule>(entity =>
    {
      entity.Property(e => e.EmiAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmiDate).HasColumnType("datetime");
      entity.Property(e => e.InterestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IsPaid).HasComment("1 = Paid else Unpaid");
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.PrincipalAmount).HasColumnType("decimal(18, 2)");

      entity.HasOne(d => d.Loan).WithMany(p => p.LoanSchedule)
              .HasForeignKey(d => d.LoanId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_LoanSchedule_LoanInformation");

      entity.HasOne(d => d.Status).WithMany(p => p.LoanSchedule)
              .HasForeignKey(d => d.StatusId)
              .HasConstraintName("FK_LoanSchedule_WFState");
    });

    modelBuilder.Entity<LoanScheduleAudit>(entity =>
    {
      entity.Property(e => e.EmiAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmiDate).HasColumnType("datetime");
      entity.Property(e => e.InterestAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.PrincipalAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<LoanType>(entity =>
    {
      entity.Property(e => e.DefaultInterstRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmiAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.EmiAmountPer).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.IsPfloan)
              .HasComment("1= PF Loan else 0")
              .HasColumnName("IsPFLoan");
      entity.Property(e => e.LoanTypeName).HasMaxLength(250);
      entity.Property(e => e.MaximumLoanAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TermsAndCondition)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<LwpDeductionSettings>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DeductionPer).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<McPolicy>(entity =>
    {
      entity.Property(e => e.InternetCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IsInternetCeilingUl).HasColumnName("IsInternetCeilingUL");
      entity.Property(e => e.IsIsdCeilingUl).HasColumnName("IsIsdCeilingUL");
      entity.Property(e => e.IsSmsCeilingUl).HasColumnName("IsSmsCeilingUL");
      entity.Property(e => e.IsTotalCeilingUl).HasColumnName("IsTotalCeilingUL");
      entity.Property(e => e.IsVasCeilingUl).HasColumnName("IsVasCeilingUL");
      entity.Property(e => e.IsdCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.McPolicyName).HasMaxLength(50);
      entity.Property(e => e.SmsCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VasCeiling).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<Menu>(entity =>
    {
      entity.Property(e => e.MenuId).HasColumnName("MenuID");
      entity.Property(e => e.MenuName).HasMaxLength(50);
      entity.Property(e => e.MenuPath).HasMaxLength(200);
      entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
      entity.Property(e => e.Sororder).HasColumnName("SORORDER");
      entity.Property(e => e.Todo).HasColumnName("TODO");
    });

    modelBuilder.Entity<Message>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => e.MessageId, "IX_Message_MessageId");

      entity.Property(e => e.ArchiveTime).HasColumnType("datetime");
      entity.Property(e => e.DeliveredTime).HasColumnType("datetime");
      entity.Property(e => e.MessageDetails).IsUnicode(false);
      entity.Property(e => e.MessageId).ValueGeneratedOnAdd();
      entity.Property(e => e.MessageSubject).HasMaxLength(500);
      entity.Property(e => e.MessagingDate).HasColumnType("datetime");
      entity.Property(e => e.RedirectLink)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Message2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Message_2023");

      entity.Property(e => e.ArchiveTime).HasColumnType("datetime");
      entity.Property(e => e.DeliveredTime).HasColumnType("datetime");
      entity.Property(e => e.MessageDetails).IsUnicode(false);
      entity.Property(e => e.MessageId).ValueGeneratedOnAdd();
      entity.Property(e => e.MessageSubject).HasMaxLength(500);
      entity.Property(e => e.MessagingDate).HasColumnType("datetime");
      entity.Property(e => e.RedirectLink)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<MessageDetails>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmailAddress)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.MessageDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.ReadTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<MessageEmployeeDetails>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.MessageEmpDtlId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MobUpload>(entity =>
    {
      entity.HasKey(e => e.UpId);

      entity.Property(e => e.Empid).HasMaxLength(50);
      entity.Property(e => e.MoNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<MobileBillingTmp>(entity =>
    {
      entity.HasKey(e => e.MobileBillingId);

      entity.ToTable("MobileBilling_tmp");

      entity.Property(e => e.AdjustedDate).HasColumnType("datetime");
      entity.Property(e => e.AdjustmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InternetAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IsdAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MobileNo)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.NwdAmount)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.SmsAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VasAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<MobileBillingUploadtmp>(entity =>
    {
      entity.HasKey(e => e.MobileBillingTempId);

      entity.ToTable("MobileBilling_Uploadtmp");

      entity.Property(e => e.BillingMonth)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.InternetAmount)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.IsdAmount)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.MobileNo)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.SalaryDate)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.SmsAmount)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VasAmount)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<MobileCeiling>(entity =>
    {
      entity.HasKey(e => e.MobCeilingId);

      entity.Property(e => e.ContactNo).HasMaxLength(50);
      entity.Property(e => e.InternetCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IsInternetCeilingUl).HasColumnName("IsInternetCeilingUL");
      entity.Property(e => e.IsIsdCeilingUl).HasColumnName("IsIsdCeilingUL");
      entity.Property(e => e.IsSmsCeilingUl).HasColumnName("IsSmsCeilingUL");
      entity.Property(e => e.IsTotalCeilingUl).HasColumnName("IsTotalCeilingUL");
      entity.Property(e => e.IsVasCeilingUl).HasColumnName("IsVasCeilingUL");
      entity.Property(e => e.IsdCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SimIssueDate).HasColumnType("datetime");
      entity.Property(e => e.SmsCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalCeiling).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VasCeiling).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<MobileCeilingTemp>(entity =>
    {
      entity.HasKey(e => e.MobCeilingId);

      entity.ToTable("MobileCeiling_Temp");

      entity.Property(e => e.ContactNo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.InternetCeiling)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.IsdCeiling)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SimIssueDate).HasColumnType("datetime");
      entity.Property(e => e.SmsCeiling)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.TotalCeiling)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.VasCeiling)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<MobileSimVendor>(entity =>
    {
      entity.HasKey(e => e.SimVendorId);

      entity.Property(e => e.VendorName).HasMaxLength(250);
    });

    modelBuilder.Entity<MobileVendorLedger>(entity =>
    {
      entity.HasKey(e => e.VendorLedgerId);

      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DebitAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LedgerReferenceNumber).HasMaxLength(250);
      entity.Property(e => e.MobileVendorLedgerDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Module>(entity =>
    {
      entity.Property(e => e.ModuleName).HasMaxLength(50);
    });

    modelBuilder.Entity<Mongla24082022DivDep>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Mongla_24_08_2022_DIV_DEP");

      entity.Property(e => e.Department).HasMaxLength(50);
      entity.Property(e => e.Designation).HasMaxLength(50);
      entity.Property(e => e.Division).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<MovementLog>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => e.MovementType, "NONCLUSTERED_Type");

      entity.HasIndex(e => e.UserId, "NonClusteredIndex_20200810_132248");

      entity.HasIndex(e => e.MovementDate, "NonClusteredIndex_20200810_133628");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogArchive2017>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovementLog_Archive_2017");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogArchive2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovementLog_Archive_2018");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogArchive2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovementLog_Archive_2019");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovementLog_Archive_2020");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovementLog_Archive_2021");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovementLog_Archive_2022");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovementLog_Archive_2023");

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.MovementId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementLogTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ActualMovement).HasColumnType("datetime");
      entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedReturnTime).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.InTime).HasMaxLength(50);
      entity.Property(e => e.MovementDate).HasColumnType("datetime");
      entity.Property(e => e.OutTime).HasMaxLength(50);
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovementPolicy>(entity =>
    {
      entity.Property(e => e.PolicyName).HasMaxLength(280);
    });

    modelBuilder.Entity<MovementPolicyDetails>(entity =>
    {
      entity.HasKey(e => e.DetailsId);

      entity.Property(e => e.Duration).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<MovementPolicyWithSbulist>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_MovementPolicyWithSBUList_1");

      entity.ToTable("MovementPolicyWithSBUList");
    });

    modelBuilder.Entity<MovementType>(entity =>
    {
      entity.Property(e => e.MovementTypeCode).HasMaxLength(50);
      entity.Property(e => e.MovementTypeName).HasMaxLength(288);
    });

    modelBuilder.Entity<MyTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<NatureOfActions>(entity =>
    {
      entity.HasKey(e => e.NatureOfActionId);

      entity.Property(e => e.NatureOfActionName).HasMaxLength(500);
    });

    modelBuilder.Entity<News>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
      entity.Property(e => e.NewsDetails).HasMaxLength(2000);
      entity.Property(e => e.NewsId).ValueGeneratedOnAdd();
      entity.Property(e => e.NewsTitle).HasMaxLength(2000);
      entity.Property(e => e.PublishDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<NewsCategory>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.NewsCategoryCode).HasMaxLength(50);
      entity.Property(e => e.NewsCategoryDescription).HasMaxLength(50);
      entity.Property(e => e.NewsCategoryId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<NightPunchConfigaration>(entity =>
    {
      entity.HasKey(e => e.NightPunchConfigId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.PunchTimeFrom).HasMaxLength(50);
      entity.Property(e => e.PunchTimeTo).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<NigthReconcilation>(entity =>
    {
      entity.HasKey(e => e.RemoveId);

      entity.Property(e => e.AttendanceDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Nominee>(entity =>
    {
      entity.Property(e => e.Address)
              .HasMaxLength(1000)
              .HasColumnName("ADDRESS");
      entity.Property(e => e.ContactNumber).HasMaxLength(50);
      entity.Property(e => e.Countryvisited)
              .HasMaxLength(1000)
              .HasColumnName("COUNTRYVISITED");
      entity.Property(e => e.Dateofbirth).HasColumnName("DATEOFBIRTH");
      entity.Property(e => e.Diseases)
              .HasMaxLength(1000)
              .HasColumnName("DISEASES");
      entity.Property(e => e.Fathername)
              .HasMaxLength(100)
              .HasColumnName("FATHERNAME");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsActive)
              .HasDefaultValue(1)
              .HasComment("1=Active,0=Inactive");
      entity.Property(e => e.Mothername)
              .HasMaxLength(100)
              .HasColumnName("MOTHERNAME");
      entity.Property(e => e.Name).HasMaxLength(500);
      entity.Property(e => e.Nationalid)
              .HasMaxLength(100)
              .HasColumnName("NATIONALID");
      entity.Property(e => e.Ofsahre)
              .HasMaxLength(50)
              .HasColumnName("OFSAHRE");
      entity.Property(e => e.Passport)
              .HasMaxLength(100)
              .HasColumnName("PASSPORT");
      entity.Property(e => e.Photo)
              .HasMaxLength(1000)
              .HasColumnName("PHOTO");
      entity.Property(e => e.TmpId).HasMaxLength(50);

      entity.HasOne(d => d.Occupation).WithMany(p => p.Nominee)
              .HasForeignKey(d => d.OccupationId)
              .HasConstraintName("FK_Nominee_Occupation");
    });

    modelBuilder.Entity<NomineeDetails>(entity =>
    {
      entity.Property(e => e.BenefitName).HasMaxLength(50);
      entity.Property(e => e.BenefitType).HasComment("1=Hospitalization;2=Group Life Insurance;3=PF;4=Gratuity;");
      entity.Property(e => e.NomineeShare).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<NomineeTemp>(entity =>
    {
      entity.HasKey(e => e.NomineTempId);

      entity.Property(e => e.Address)
              .HasMaxLength(1000)
              .HasColumnName("ADDRESS");
      entity.Property(e => e.Countryvisited)
              .HasMaxLength(1000)
              .HasColumnName("COUNTRYVISITED");
      entity.Property(e => e.Dateofbirth).HasColumnName("DATEOFBIRTH");
      entity.Property(e => e.Diseases)
              .HasMaxLength(1000)
              .HasColumnName("DISEASES");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Fathername)
              .HasMaxLength(100)
              .HasColumnName("FATHERNAME");
      entity.Property(e => e.IsActive).HasMaxLength(50);
      entity.Property(e => e.Mothername)
              .HasMaxLength(100)
              .HasColumnName("MOTHERNAME");
      entity.Property(e => e.Name).HasMaxLength(500);
      entity.Property(e => e.Nationalid)
              .HasMaxLength(100)
              .HasColumnName("NATIONALID");
      entity.Property(e => e.Occupation).HasMaxLength(50);
      entity.Property(e => e.Ofsahre)
              .HasMaxLength(50)
              .HasColumnName("OFSAHRE");
      entity.Property(e => e.Passport)
              .HasMaxLength(100)
              .HasColumnName("PASSPORT");
      entity.Property(e => e.Photo)
              .HasMaxLength(1000)
              .HasColumnName("PHOTO");
      entity.Property(e => e.RelationShipName).HasMaxLength(50);
    });

    modelBuilder.Entity<Notice>(entity =>
    {
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
      entity.Property(e => e.NoticeDetails).HasMaxLength(2000);
      entity.Property(e => e.NoticeTitle).HasMaxLength(1000);
      entity.Property(e => e.Publishdate).HasColumnType("datetime");
    });

    modelBuilder.Entity<NoticeCategory>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.NoticeCategoryCode).HasMaxLength(50);
      entity.Property(e => e.NoticeCategoryDescription).HasMaxLength(50);
      entity.Property(e => e.NoticeCategoryId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<NoticeEmployee>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.NoticeEmployeeId).ValueGeneratedOnAdd();
      entity.Property(e => e.ViewStatus).HasDefaultValue(0);
    });

    modelBuilder.Entity<NoticePay>(entity =>
    {
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DeleteDate).HasColumnType("datetime");
      entity.Property(e => e.NoticeReceiveDate).HasColumnType("datetime");
      entity.Property(e => e.TotalBasic).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<NotificationEmailType>(entity =>
    {
      entity.HasKey(e => e.EmailNotificationTypeId);

      entity.HasIndex(e => e.EmailNotificationTypeName, "IX_NotificationEmailType").IsUnique();

      entity.Property(e => e.EmailNotificationTypeName).HasMaxLength(50);
      entity.Property(e => e.ParamDefination)
              .HasMaxLength(1050)
              .IsUnicode(false);
    });

    modelBuilder.Entity<NotificationSource>(entity =>
    {
      entity.HasIndex(e => e.NotificationSourceId, "IX_NotificationSource").IsUnique();

      entity.Property(e => e.BaseUrl)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.SourceTitle)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<NotificationTypeAndPeerGroup>(entity =>
    {
      entity.HasKey(e => e.PeerGroupId);
    });

    modelBuilder.Entity<Notifications>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.NotificationDate).HasColumnType("datetime");
      entity.Property(e => e.NotificationId).ValueGeneratedOnAdd();
      entity.Property(e => e.NotificationRedirect)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.NotificationTitle).HasMaxLength(250);
      entity.Property(e => e.ReadTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<Occupation>(entity =>
    {
      entity.Property(e => e.OccCode).HasMaxLength(50);
      entity.Property(e => e.OccupationName).HasMaxLength(50);
    });

    modelBuilder.Entity<OfficeTime>(entity =>
    {
      entity.Property(e => e.OfficeTimeId).HasColumnName("OfficeTimeID");
      entity.Property(e => e.FriBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FriFrom).HasMaxLength(50);
      entity.Property(e => e.FriTo).HasMaxLength(50);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.MonBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MonFrom).HasMaxLength(50);
      entity.Property(e => e.MonTo).HasMaxLength(50);
      entity.Property(e => e.SatBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SatFrom).HasMaxLength(50);
      entity.Property(e => e.SatTo).HasMaxLength(50);
      entity.Property(e => e.SunBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SunFrom).HasMaxLength(50);
      entity.Property(e => e.SunTo).HasMaxLength(50);
      entity.Property(e => e.ThuBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThuFrom).HasMaxLength(50);
      entity.Property(e => e.ThuTo).HasMaxLength(50);
      entity.Property(e => e.TueBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TueFrom).HasMaxLength(50);
      entity.Property(e => e.TueTo).HasMaxLength(50);
      entity.Property(e => e.WedBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.WedFrom).HasMaxLength(50);
      entity.Property(e => e.WedTo).HasMaxLength(50);

      entity.HasOne(d => d.Shift).WithMany(p => p.OfficeTime)
              .HasForeignKey(d => d.ShiftId)
              .HasConstraintName("FK_OfficeTime_Shift");
    });

    modelBuilder.Entity<OfficeTimeHistory>(entity =>
    {
      entity.Property(e => e.OfficeTimeHistoryId).HasColumnName("OfficeTimeHistoryID");
      entity.Property(e => e.FriBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FriFrom).HasMaxLength(50);
      entity.Property(e => e.FriTo).HasMaxLength(50);
      entity.Property(e => e.HistoryEffectEndDate).HasColumnType("datetime");
      entity.Property(e => e.HistoryEffectStartDate).HasColumnType("datetime");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.MonBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MonFrom).HasMaxLength(50);
      entity.Property(e => e.MonTo).HasMaxLength(50);
      entity.Property(e => e.OfficeTimeId).HasColumnName("OfficeTimeID");
      entity.Property(e => e.SatBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SatFrom).HasMaxLength(50);
      entity.Property(e => e.SatTo).HasMaxLength(50);
      entity.Property(e => e.SunBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SunFrom).HasMaxLength(50);
      entity.Property(e => e.SunTo).HasMaxLength(50);
      entity.Property(e => e.ThuBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ThuFrom).HasMaxLength(50);
      entity.Property(e => e.ThuTo).HasMaxLength(50);
      entity.Property(e => e.TueBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TueFrom).HasMaxLength(50);
      entity.Property(e => e.TueTo).HasMaxLength(50);
      entity.Property(e => e.WedBuiltInOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.WedFrom).HasMaxLength(50);
      entity.Property(e => e.WedTo).HasMaxLength(50);
    });

    modelBuilder.Entity<OldSalary>(entity =>
    {
      entity.HasKey(e => e.SalaryId).HasName("PK_Salary");

      entity.Property(e => e.Advance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Ait).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Arear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CoffEncash)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("COffEncash");
      entity.Property(e => e.Css).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Cssemployer).HasColumnName("CSSEMPLOYER");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Insurence).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.LateLeave).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LeaveEncash).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtherAddition).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtherDeduction).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Overtime).HasColumnName("OVERTIME");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Payrollid).HasColumnName("PAYROLLID");
      entity.Property(e => e.SalaryMonth).HasColumnType("smalldatetime");
      entity.Property(e => e.Telephone).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Withoutpay).HasColumnName("WITHOUTPAY");
    });

    modelBuilder.Entity<Oldsalarytemp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OLDSALARYTEMP");

      entity.Property(e => e.Advance).HasColumnName("ADVANCE");
      entity.Property(e => e.Ait).HasColumnName("AIT");
      entity.Property(e => e.Approvedate)
              .HasColumnType("datetime")
              .HasColumnName("APPROVEDATE");
      entity.Property(e => e.Approverid).HasColumnName("APPROVERID");
      entity.Property(e => e.Arear).HasColumnName("AREAR");
      entity.Property(e => e.Coffencash).HasColumnName("COFFENCASH");
      entity.Property(e => e.Css).HasColumnName("CSS");
      entity.Property(e => e.Cssemployer).HasColumnName("CSSEMPLOYER");
      entity.Property(e => e.Generatedate)
              .HasColumnType("datetime")
              .HasColumnName("GENERATEDATE");
      entity.Property(e => e.Grosspay).HasColumnName("GROSSPAY");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Insurence).HasColumnName("INSURENCE");
      entity.Property(e => e.Isclear).HasColumnName("ISCLEAR");
      entity.Property(e => e.Lastupdatedate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.Lateleave).HasColumnName("LATELEAVE");
      entity.Property(e => e.Leavededucted).HasColumnName("LEAVEDEDUCTED");
      entity.Property(e => e.Leaveencash).HasColumnName("LEAVEENCASH");
      entity.Property(e => e.Messageid).HasColumnName("MESSAGEID");
      entity.Property(e => e.Netpay).HasColumnName("NETPAY");
      entity.Property(e => e.Otheraddition).HasColumnName("OTHERADDITION");
      entity.Property(e => e.Otherdeduction).HasColumnName("OTHERDEDUCTION");
      entity.Property(e => e.Overtime).HasColumnName("OVERTIME");
      entity.Property(e => e.Paymentmode)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("PAYMENTMODE");
      entity.Property(e => e.Payrollid).HasColumnName("PAYROLLID");
      entity.Property(e => e.Salarymonth)
              .HasColumnType("datetime")
              .HasColumnName("SALARYMONTH");
      entity.Property(e => e.Stateid).HasColumnName("STATEID");
      entity.Property(e => e.Telephone).HasColumnName("TELEPHONE");
      entity.Property(e => e.Userid).HasColumnName("USERID");
      entity.Property(e => e.Withoutpay).HasColumnName("WITHOUTPAY");
    });

    modelBuilder.Entity<OmitLateLog>(entity =>
    {
      entity.Property(e => e.Date).HasColumnType("datetime");
      entity.Property(e => e.Status)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Type)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<OnSiteClientConveyance>(entity =>
    {
      entity.HasKey(e => e.OnsiteConveyanceId);

      entity.Property(e => e.ApplicantAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproverRemarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RecomaderRemarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RecommandedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TransportDescription)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<OnsiteClient>(entity =>
    {
      entity.HasIndex(e => new { e.UserId, e.Status, e.AppliedDate }, "IX_OnsiteClient1");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.AttachmentPath).HasMaxLength(200);
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OnsiteClientArchive2017>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OnsiteClient_Archive_2017");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
      entity.Property(e => e.UserId).HasMaxLength(50);
    });

    modelBuilder.Entity<OnsiteClientArchive2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OnsiteClient_Archive_2018");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.OnsiteClientId).ValueGeneratedOnAdd();
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
      entity.Property(e => e.UserId).HasMaxLength(50);
    });

    modelBuilder.Entity<OnsiteClientArchive2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OnsiteClient_Archive_2019");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.OnsiteClientId).ValueGeneratedOnAdd();
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OnsiteClientArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OnsiteClient_Archive_2020");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OnsiteClientArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OnsiteClient_Archive_2021");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.OnsiteClientId).ValueGeneratedOnAdd();
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OnsiteClientArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OnsiteClient_Archive_2022");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.AttachmentPath).HasMaxLength(200);
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.OnsiteClientId).ValueGeneratedOnAdd();
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OnsiteClientArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OnsiteClient_Archive_2023");

      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.AttachmentPath).HasMaxLength(200);
      entity.Property(e => e.ClientCode).HasMaxLength(100);
      entity.Property(e => e.ClientName).HasMaxLength(100);
      entity.Property(e => e.ConvenceAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DayNo).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.OnsiteClientId).ValueGeneratedOnAdd();
      entity.Property(e => e.ProjectCode).HasMaxLength(50);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OrganizerInfo>(entity =>
    {
      entity.HasKey(e => e.OrganizerId);

      entity.Property(e => e.OrganizerCode).HasMaxLength(50);
      entity.Property(e => e.OrganizerName).HasMaxLength(500);
    });

    modelBuilder.Entity<OtAllocation>(entity =>
    {
      entity.ToTable(tb => tb.HasTrigger("AfterInsertOtAllocation"));

      entity.Property(e => e.AverageOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
      entity.Property(e => e.Remarks).IsUnicode(false);
      entity.Property(e => e.StateChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtAllocation2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocation_2018");

      entity.Property(e => e.AverageOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.StateChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtAllocation2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocation_2019");

      entity.Property(e => e.AverageOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.StateChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtAllocation2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocation_2020");

      entity.Property(e => e.AverageOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtAllocationId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.StateChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtAllocation2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocation_2021");

      entity.Property(e => e.AverageOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtAllocationId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
      entity.Property(e => e.Remarks).IsUnicode(false);
      entity.Property(e => e.StateChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtAllocation2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocation_2022");

      entity.Property(e => e.AverageOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtAllocationId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
      entity.Property(e => e.Remarks).IsUnicode(false);
      entity.Property(e => e.StateChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtAllocation2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocation_2023");

      entity.Property(e => e.AverageOtHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtAllocationId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
      entity.Property(e => e.Remarks).IsUnicode(false);
      entity.Property(e => e.StateChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtAllocationDetails>(entity =>
    {
      entity.HasIndex(e => e.OtAllocationId, "NonClusteredIndex_20200902_121248");

      entity.HasIndex(e => new { e.HrRecordId, e.OtFromDate }, "NonClusteredIndex_20200902_124147");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAllocationDetails2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocationDetails_2018");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAllocationDetails2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocationDetails_2019");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtAllocationDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAllocationDetails2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocationDetails_2020");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtAllocationDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAllocationDetails2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocationDetails_2021");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtAllocationDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAllocationDetails2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocationDetails_2022");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtAllocationDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAllocationDetails2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocationDetails_2023");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtAllocationDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAllocationDetails27April2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OtAllocationDetails_27_April_2022");

      entity.Property(e => e.OtAllocatedHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtAllocationDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.OtTimeFrom).HasPrecision(0);
      entity.Property(e => e.OtTimeTo).HasPrecision(0);
      entity.Property(e => e.OttoDate).HasColumnName("OTToDate");
    });

    modelBuilder.Entity<OtAmountSetUpInfo>(entity =>
    {
      entity.HasKey(e => e.OtAmountSetUpId);

      entity.Property(e => e.InsertDate).HasColumnType("datetime");
      entity.Property(e => e.OtAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.OtAmountTypeName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtCompleteListMay2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OT_Complete_List_MAY_2022");
    });

    modelBuilder.Entity<OtCutofMonth>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OtFromDate).HasColumnType("datetime");
      entity.Property(e => e.OtToDate).HasColumnType("datetime");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtLimit>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtLimitDetails>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Otconfiguration>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OTConfiguration");

      entity.Property(e => e.OtconfigurationId)
              .ValueGeneratedOnAdd()
              .HasColumnName("OTConfigurationId");
      entity.Property(e => e.OtconfigureDate)
              .HasColumnType("datetime")
              .HasColumnName("OTConfigureDate");
      entity.Property(e => e.OtmaxHour)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTMaxHour");
      entity.Property(e => e.OtminHour)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTMinHour");
      entity.Property(e => e.Otrate)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTRate");
    });

    modelBuilder.Entity<OtgradeSbu>(entity =>
    {
      entity.ToTable("OTGradeSbu");

      entity.Property(e => e.OtgradeSbuId).HasColumnName("OTGradeSbuId");
    });

    modelBuilder.Entity<OtherPayment>(entity =>
    {
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BonusPolicyName).HasMaxLength(250);
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FestivalDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(250);
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentMonth).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasMaxLength(250);
      entity.Property(e => e.StampCharge).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<OtherPaymentCostCentre>(entity =>
    {
      entity.HasKey(e => e.OtherPaymentCostcentreHisId);

      entity.Property(e => e.PaymentMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtherPaymentReferenceNo>(entity =>
    {
      entity.HasNoKey();
    });

    modelBuilder.Entity<OtherPaymentTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BonusPolicyName).HasMaxLength(250);
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FestivalDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentMonth).HasColumnType("datetime");
      entity.Property(e => e.StampCharge).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<OtherPaymentUploadTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CtcAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<OtherSchedule>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AddedDate).HasMaxLength(250);
      entity.Property(e => e.OtherScheduleDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasMaxLength(250);
    });

    modelBuilder.Entity<Otsettings>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OTSettings");

      entity.Property(e => e.OtmaxHour)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTMaxHour");
      entity.Property(e => e.OtminHour)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTMinHour");
      entity.Property(e => e.OtstartHour)
              .HasMaxLength(10)
              .HasColumnName("OTStartHour");
      entity.Property(e => e.OttotalHour)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTTotalHour");
      entity.Property(e => e.OttypeId).HasColumnName("OTTypeId");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OtsettingsBg>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OTSettingsBG");

      entity.Property(e => e.BreakUpOt)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("BreakUpOT");
      entity.Property(e => e.OtPilocyCode)
              .HasMaxLength(50)
              .IsFixedLength();
      entity.Property(e => e.OtminHour)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTMinHour");
      entity.Property(e => e.OtpolicyName).HasColumnName("OTPolicyName");
      entity.Property(e => e.OttypeId).HasColumnName("OTTypeId");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<OtsettingsMapDesignation>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OTSettingsMapDesignation");

      entity.Property(e => e.OtsettingsMapDesignationId)
              .ValueGeneratedOnAdd()
              .HasColumnName("OTSettingsMapDesignationId");
    });

    modelBuilder.Entity<Otslab>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OTSlab");

      entity.Property(e => e.CalculateMin).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.OtfromMin)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTFromMin");
      entity.Property(e => e.OtslabId)
              .ValueGeneratedOnAdd()
              .HasColumnName("OTSlabId");
      entity.Property(e => e.OttoMin)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("OTToMin");
    });

    modelBuilder.Entity<Outlet>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ContactNo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ContactPerson)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.GeoLocation)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.OutletAddress).HasMaxLength(50);
      entity.Property(e => e.OutletCode)
              .HasMaxLength(20)
              .IsUnicode(false);
      entity.Property(e => e.OutletId).ValueGeneratedOnAdd();
      entity.Property(e => e.OutletName).HasMaxLength(250);
      entity.Property(e => e.RsmregionId).HasColumnName("RSMRegionId");
    });

    modelBuilder.Entity<OutletVisitingSchedule>(entity =>
    {
      entity.HasKey(e => e.OutletVisitingScheduleId).HasName("PK_OutletVisitingSchedule_1");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OutletVisitingScheduleDetails>(entity =>
    {
      entity.HasKey(e => e.OutletVisitingScheduleDetailsId).HasName("PK_OutletVisitingSchedule");

      entity.Property(e => e.Description).HasMaxLength(350);
      entity.Property(e => e.EndTimezone).HasMaxLength(50);
      entity.Property(e => e.RecurrenceException).HasMaxLength(150);
      entity.Property(e => e.RecurrenceRule).HasMaxLength(50);
      entity.Property(e => e.StartTimezone).HasMaxLength(50);
      entity.Property(e => e.Title).HasMaxLength(350);
      entity.Property(e => e.VisitEndDateTime).HasColumnType("datetime");
      entity.Property(e => e.VisitStartDateTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTime>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => e.Otmonth, "IX_OverTime").IsDescending();

      entity.HasIndex(e => e.Otmonth, "Index_OT_Month");

      entity.HasIndex(e => e.OtfromDate, "OverTime_NoClusIndex");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTime10082022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OverTime_10_08_2022");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeApprovalHistory>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.OverTimeApprovalHistoryId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeArchive2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OverTime_Archive_2018");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeArchive2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OverTime_Archive_2019");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OverTime_Archive_2020");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OverTime_Archive_2021");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OverTime_Archive_2022");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("OverTime_Archive_2023");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.Otmonth)
              .HasColumnType("datetime")
              .HasColumnName("OTMonth");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OverTimeId).ValueGeneratedOnAdd();
      entity.Property(e => e.PaidDate).HasColumnType("datetime");
      entity.Property(e => e.RecommandDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeCtcAdjustment>(entity =>
    {
      entity.HasKey(e => e.OverTimeAdjustmentId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DeleteDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeMonthEndRecommendedNotification>(entity =>
    {
      entity.HasKey(e => e.OtmonthEndRecNotifyId);

      entity.Property(e => e.OtmonthEndRecNotifyId).HasColumnName("OTMonthEndRecNotifyId");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.OverTimeMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimePaidDetails>(entity =>
    {
      entity.HasKey(e => e.OverTimeProcessDetailsId);

      entity.Property(e => e.ActualOtHour).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.CtcActualValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OtRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeProcessDetails>(entity =>
    {
      entity.Property(e => e.ActualOtHour).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.CtcActualValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Otrate)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTRate");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OverTimeTaxInfo>(entity =>
    {
      entity.HasKey(e => e.OttaxId);

      entity.Property(e => e.OttaxId).HasColumnName("OTTaxId");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<OverTimeType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.OttypeId).HasColumnName("OTTypeId");
      entity.Property(e => e.OttypeName)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("OTTypeName");
    });

    modelBuilder.Entity<OvertimeDeduction>(entity =>
    {
      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.RejectDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<OvertimePaid>(entity =>
    {
      entity.HasKey(e => e.OvertimeProcessId);

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.RemarksForEdit).HasMaxLength(100);
      entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<OvertimePaymentCostCentre>(entity =>
    {
      entity.HasKey(e => e.OvertimePaymentCostCentreHisId);

      entity.Property(e => e.OverTimeMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<OvertimeProcess>(entity =>
    {
      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.RemarksForEdit).HasMaxLength(100);
      entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<OvertimeTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmpId)
              .HasMaxLength(50)
              .HasColumnName("EmpID");
      entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
    });

    modelBuilder.Entity<PaScoreConfig>(entity =>
    {
      entity.HasKey(e => e.PaScoreConfigId).HasName("PK_PAScoreConfig");
    });

    modelBuilder.Entity<ParallelApprover>(entity =>
    {
      entity.HasKey(e => e.AssignApproverId);

      entity.HasIndex(e => new { e.HrRecordId, e.ApproverId, e.ModuleId }, "IX_SameParallelApprover").IsUnique();

      entity.HasIndex(e => new { e.ApproverId, e.ModuleId, e.Type }, "IndexParallelApprover");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<PartialPayment>(entity =>
    {
      entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentPercentage).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<PasswordHistory>(entity =>
    {
      entity.HasKey(e => e.HistoryId);

      entity.Property(e => e.OldPassword).HasMaxLength(50);
      entity.Property(e => e.PasswordChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Payband>(entity =>
    {
      entity.Property(e => e.PaybandName).HasMaxLength(50);
    });

    modelBuilder.Entity<Payroll>(entity =>
    {
      entity.Property(e => e.Activedate)
              .HasColumnType("datetime")
              .HasColumnName("ACTIVEDATE");
      entity.Property(e => e.Approvedate)
              .HasColumnType("datetime")
              .HasColumnName("APPROVEDATE");
      entity.Property(e => e.Authorizeby).HasColumnName("AUTHORIZEBY");
      entity.Property(e => e.BasicofGross).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CssfundEmployee)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CSSFundEmployee");
      entity.Property(e => e.CssfundEmployer)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CSSFundEmployer");
      entity.Property(e => e.CurrentGrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FestivalBonus).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Gradeid).HasColumnName("GRADEID");
      entity.Property(e => e.HospitalInsurance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HouseRent).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.MedicalAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MobileAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Nextreviewdate)
              .HasColumnType("datetime")
              .HasColumnName("NEXTREVIEWDATE");
      entity.Property(e => e.Otapplicable).HasColumnName("OTAPPLICABLE");
      entity.Property(e => e.OtherAllowance).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Otrate)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTRATE");
      entity.Property(e => e.Payrolltype).HasColumnName("PAYROLLTYPE");
      entity.Property(e => e.PerformanceBonus).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PfopeningBalance)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PFOpeningBalance");
      entity.Property(e => e.ProfitSharing).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProjectBonus).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Rate)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("RATE");
      entity.Property(e => e.Stateid).HasColumnName("STATEID");
      entity.Property(e => e.TinNumber).HasMaxLength(50);
      entity.Property(e => e.Wagestype).HasColumnName("WAGESTYPE");
      entity.Property(e => e.Workinghour)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("WORKINGHOUR");
    });

    modelBuilder.Entity<PayrollAccessRestriction>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<PayrollAdjustment>(entity =>
    {
      entity.HasKey(e => e.PayrollAdjustmentId).HasName("PK_PayrollAdjustmentMaster");

      entity.HasIndex(e => new { e.FromMonth, e.ToMonth, e.NetTaxPayable }, "NonClasterPayrollAdjustmentIndex");

      entity.HasIndex(e => new { e.FromMonth, e.ToMonth }, "NonClasterePayrollAdjustmentIndex");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.CompanyTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FromMonth).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ToMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<PayrollAdjustmentDetails>(entity =>
    {
      entity.HasIndex(e => new { e.CtcId, e.ValidDateFrom, e.ValidDateTo, e.PayrollAdjustmentId }, "IX_CtcElemnts").IsUnique();

      entity.HasIndex(e => new { e.ValidDateFrom, e.ValidDateTo }, "NonclasterPayrollAdjustmentDetailsIndex");

      entity.Property(e => e.AccountNo).HasMaxLength(50);
      entity.Property(e => e.Remarks).HasMaxLength(250);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.ValidDateFrom).HasColumnType("datetime");
      entity.Property(e => e.ValidDateTo).HasColumnType("datetime");
    });

    modelBuilder.Entity<PayrollBlockInfo>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable(tb =>
              {
              tb.HasTrigger("UpdateBlockSubmitDateAfterPayrollBlockSetup");
              tb.HasTrigger("UpdateUnBlockSubmitDateAfterPayrollUnBlockSetup");
            });

      entity.Property(e => e.BlockDate).HasColumnType("datetime");
      entity.Property(e => e.BlockRemark).HasMaxLength(300);
      entity.Property(e => e.BlockStatus).HasComment("1=Block,0=Unblock");
      entity.Property(e => e.BlockSubmitDate).HasColumnType("datetime");
      entity.Property(e => e.PayrollBlockId).ValueGeneratedOnAdd();
      entity.Property(e => e.UnBlockRemark).HasMaxLength(300);
      entity.Property(e => e.UnBlockSubmitDate).HasColumnType("datetime");
      entity.Property(e => e.UnblockDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<PayrollCycleSetup>(entity =>
    {
      entity.HasKey(e => e.PayrollCycleId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<PayrollException>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<PayrollHistory>(entity =>
    {
      entity.Property(e => e.Activedate)
              .HasColumnType("datetime")
              .HasColumnName("ACTIVEDATE");
      entity.Property(e => e.Approvedate)
              .HasColumnType("datetime")
              .HasColumnName("APPROVEDATE");
      entity.Property(e => e.Authorizeby).HasColumnName("AUTHORIZEBY");
      entity.Property(e => e.Basicofgross)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("BASICOFGROSS");
      entity.Property(e => e.Cssfundemployee)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CSSFUNDEMPLOYEE");
      entity.Property(e => e.Cssfundemployer)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CSSFUNDEMPLOYER");
      entity.Property(e => e.Currentgrosspay)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("CURRENTGROSSPAY");
      entity.Property(e => e.Festivalbonus)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("FESTIVALBONUS");
      entity.Property(e => e.Gradeid).HasColumnName("GRADEID");
      entity.Property(e => e.Hospitalinsurance)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("HOSPITALINSURANCE");
      entity.Property(e => e.Houserent)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("HOUSERENT");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Lastupdatedate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.Medicalallowance)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("MEDICALALLOWANCE");
      entity.Property(e => e.Mobileallowance)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("MOBILEALLOWANCE");
      entity.Property(e => e.Nextreviewdate)
              .HasColumnType("datetime")
              .HasColumnName("NEXTREVIEWDATE");
      entity.Property(e => e.Otapplicable).HasColumnName("OTAPPLICABLE");
      entity.Property(e => e.Otherallowance)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTHERALLOWANCE");
      entity.Property(e => e.Otrate)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTRATE");
      entity.Property(e => e.Payrollid).HasColumnName("PAYROLLID");
      entity.Property(e => e.Payrolltype).HasColumnName("PAYROLLTYPE");
      entity.Property(e => e.Performancebonus)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PERFORMANCEBONUS");
      entity.Property(e => e.Pfopeningbalance)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PFOPENINGBALANCE");
      entity.Property(e => e.Profitsharing)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PROFITSHARING");
      entity.Property(e => e.Projectbonus)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("PROJECTBONUS");
      entity.Property(e => e.Rate)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("RATE");
      entity.Property(e => e.Stateid).HasColumnName("STATEID");
      entity.Property(e => e.Tinnumber)
              .HasMaxLength(50)
              .HasColumnName("TINNUMBER");
      entity.Property(e => e.Wagestype).HasColumnName("WAGESTYPE");
      entity.Property(e => e.Workinghour)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("WORKINGHOUR");
    });

    modelBuilder.Entity<PayrollIntegrationSetup>(entity =>
    {
      entity.Property(e => e.TadadeductionCtc).HasColumnName("TADADeductionCtc");
    });

    modelBuilder.Entity<PerformanceFactors>(entity =>
    {
      entity.HasKey(e => e.PerformanceFactorsId).HasName("PK_PerformanceFactory");

      entity.Property(e => e.PfattributesName)
              .HasMaxLength(150)
              .HasColumnName("PFAttributesName");
    });

    modelBuilder.Entity<PerformanceFactorsRating>(entity =>
    {
      entity.HasKey(e => e.FactorsRatingId).HasName("PK_PerformanceFactoryRating");
    });

    modelBuilder.Entity<PerformanceFactory>(entity =>
    {
      entity.HasKey(e => e.PerformanceFactoryId).HasName("PK_[PerformanceFactory]");

      entity.Property(e => e.PfattributesName)
              .HasMaxLength(150)
              .HasColumnName("PFAttributesName");
    });

    modelBuilder.Entity<PerformanceFactoryRating>(entity =>
    {
      entity.HasKey(e => e.FactoryRatingId).HasName("PK_PerformanceFactoryRating1");

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<PerformanceFactoryRatingLog>(entity =>
    {
      entity.HasKey(e => e.FactoryRatingId);

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<PerformancePayroll>(entity =>
    {
      entity.HasIndex(e => new { e.PerformancePayrollId, e.PerformanceReviewId, e.HrRecordId, e.EvaluationType, e.EffectiveDate }, "NonClusteredIndex_20230114_111558");

      entity.Property(e => e.NewLocationId)
              .HasMaxLength(10)
              .IsFixedLength();
    });

    modelBuilder.Entity<PerformancePayrollDetails>(entity =>
    {
      entity.HasKey(e => e.PerformancePayrollDetailId);
    });

    modelBuilder.Entity<PerformanceReview>(entity =>
    {
      entity.Property(e => e.AnticipatedConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.CurrentBasicSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentTakeHomeSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.JoiningDate).HasColumnType("datetime");
      entity.Property(e => e.LengthOfService).HasMaxLength(50);
      entity.Property(e => e.ReasonForCurrentReview).HasMaxLength(50);
      entity.Property(e => e.ReviewDate).HasColumnType("datetime");
      entity.Property(e => e.RsmregionId).HasColumnName("RSMRegionId");
    });

    modelBuilder.Entity<PerformanceReviewAttribute>(entity =>
    {
      entity.HasKey(e => e.PrattributeId);

      entity.Property(e => e.PrattributeId).HasColumnName("PRAttributeId");
      entity.Property(e => e.PrattributeName)
              .HasMaxLength(50)
              .HasColumnName("PRAttributeName");
    });

    modelBuilder.Entity<PerformanceReviewDetailDraft>(entity =>
    {
      entity.HasNoKey();
    });

    modelBuilder.Entity<PerformanceReviewLog>(entity =>
    {
      entity.Property(e => e.PerformanceReviewLogId).ValueGeneratedNever();
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.CurrentBasicSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CurrentTakeHomeSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LengthOfService).HasMaxLength(50);
      entity.Property(e => e.PerformanceReviewId).ValueGeneratedOnAdd();
      entity.Property(e => e.ReasonForCurrentReview).HasMaxLength(50);
      entity.Property(e => e.ReviewDate).HasColumnType("datetime");
      entity.Property(e => e.RsmregionId).HasColumnName("RSMRegionId");
      entity.Property(e => e.SupervisorId).HasMaxLength(50);
    });

    modelBuilder.Entity<PerformanceReviewMaster>(entity =>
    {
      entity.HasKey(e => e.PerformanceReviewId);

      entity.ToTable(tb => tb.HasTrigger("tr_PMSRV_MASTER_DH_EVAL"));

      entity.HasIndex(e => new { e.PerformanceReviewId, e.TotalEvaluationMarkByDh, e.HrRecordId, e.IsReviewApprovedByHrHead, e.YearConfigId }, "NonClusteredIndex_20230114_110547");

      entity.Property(e => e.DocumentFilePath)
              .HasMaxLength(1000)
              .IsUnicode(false)
              .HasColumnName("DocumentFIlePath");
      entity.Property(e => e.TotalEvaluationMarkByDh).HasColumnName("TotalEvaluationMarkByDH");
      entity.Property(e => e.TotalEvaluationMarkByDhdate).HasColumnName("TotalEvaluationMarkByDHDate");
      entity.Property(e => e.TotalEvaluationMarkByLm).HasColumnName("TotalEvaluationMarkByLM");
      entity.Property(e => e.TotalEvaluationMarkByLmdate).HasColumnName("TotalEvaluationMarkByLMDate");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<PerformanceReviewMasterDraft>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DocumentFilePath)
              .HasMaxLength(1000)
              .IsUnicode(false)
              .HasColumnName("DocumentFIlePath");
      entity.Property(e => e.TotalEvaluationMarkByDh).HasColumnName("TotalEvaluationMarkByDH");
      entity.Property(e => e.TotalEvaluationMarkByDhdate).HasColumnName("TotalEvaluationMarkByDHDate");
      entity.Property(e => e.TotalEvaluationMarkByLm).HasColumnName("TotalEvaluationMarkByLM");
      entity.Property(e => e.TotalEvaluationMarkByLmdate).HasColumnName("TotalEvaluationMarkByLMDate");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<PfFundEligibleAmount>(entity =>
    {
      entity.Property(e => e.PfFundEligibleAmountName).HasMaxLength(200);
    });

    modelBuilder.Entity<PfIgnore>(entity =>
    {
      entity.ToTable("PF_Ignore");

      entity.Property(e => e.PfignoreId).HasColumnName("PFIgnoreId");
      entity.Property(e => e.Remarks).HasMaxLength(100);
    });

    modelBuilder.Entity<PfMembership>(entity =>
    {
      entity.ToTable("PF_Membership");

      entity.HasIndex(e => new { e.HrRecordId, e.SalaryStartMonth, e.Status }, "NonClasterPF_MembershipIndex");

      entity.HasIndex(e => new { e.Status, e.SalaryStartMonth }, "NonClasterrPF_MembershipIndex");

      entity.Property(e => e.PfmembershipId).HasColumnName("PFMembershipId");
      entity.Property(e => e.AttachedDocument).HasMaxLength(300);
      entity.Property(e => e.PfdeductionPercentage)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("PFDeductionPercentage");
    });

    modelBuilder.Entity<Pfrecovery>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("PFRecovery");

      entity.Property(e => e.MakeDate).HasColumnType("datetime");
      entity.Property(e => e.PfRecoveryId).ValueGeneratedOnAdd();
      entity.Property(e => e.VoucherNo)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<PhoneBillDeductionTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EffectiveMonth).HasColumnType("datetime");
      entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
      entity.Property(e => e.EmployeePf)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.MbillAmount)
              .HasColumnType("money")
              .HasColumnName("MBillAmount");
      entity.Property(e => e.MbillMonth)
              .HasColumnType("datetime")
              .HasColumnName("MBillMonth");
      entity.Property(e => e.Mdeduction)
              .HasColumnType("money")
              .HasColumnName("MDeduction");
      entity.Property(e => e.MlimitAmount)
              .HasColumnType("money")
              .HasColumnName("MLimitAmount");
      entity.Property(e => e.MphoneNumber)
              .HasMaxLength(12)
              .IsUnicode(false)
              .HasColumnName("MPhoneNumber");
      entity.Property(e => e.PostingDate).HasColumnType("datetime");
      entity.Property(e => e.TbillAmount)
              .HasColumnType("money")
              .HasColumnName("TBillAmount");
      entity.Property(e => e.TbillMonth)
              .HasColumnType("datetime")
              .HasColumnName("TBillMonth");
      entity.Property(e => e.Tdeduction)
              .HasColumnType("money")
              .HasColumnName("TDeduction");
      entity.Property(e => e.TlimitAmount)
              .HasColumnType("money")
              .HasColumnName("TLimitAmount");
      entity.Property(e => e.TphoneNumber)
              .HasMaxLength(12)
              .IsUnicode(false)
              .HasColumnName("TPhoneNumber");
      entity.Property(e => e.TranId)
              .HasColumnType("numeric(18, 0)")
              .HasColumnName("TranID");
      entity.Property(e => e.VoiceMail).HasColumnType("money");
    });

    modelBuilder.Entity<PmsInstructionConfig>(entity =>
    {
      entity.HasKey(e => e.InstructionId);

      entity.Property(e => e.InstructionId).ValueGeneratedOnAdd();
      entity.Property(e => e.InstructionTitle)
              .HasMaxLength(250)
              .IsUnicode(false);

      entity.HasOne(d => d.Instruction).WithOne(p => p.PmsInstructionConfig)
              .HasForeignKey<PmsInstructionConfig>(d => d.InstructionId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_PmsInstructionConfig_PmsTabConfig");
    });

    modelBuilder.Entity<PmsTabConfig>(entity =>
    {
      entity.HasKey(e => e.PmsConfigId);

      entity.Property(e => e.TabName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Title)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<PmsTabTitles>(entity =>
    {
      entity.HasKey(e => e.PmsTabTitleId);

      entity.Property(e => e.Title)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<PostatusType>(entity =>
    {
      entity.HasKey(e => e.StatusId).HasName("PK_POStatusName");

      entity.ToTable("POStatusType");

      entity.Property(e => e.StatusName).HasMaxLength(50);
    });

    modelBuilder.Entity<PostingType>(entity =>
    {
      entity.HasKey(e => e.PostingTypeId).HasName("PK_PromotionType");

      entity.Property(e => e.IsConsiderForPms).HasColumnName("IsConsiderForPMS");
      entity.Property(e => e.PostingTypeName).HasMaxLength(100);
    });

    modelBuilder.Entity<PreferredName22012023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Preferred_Name_22_01_2023");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.ShortName).HasMaxLength(50);
    });

    modelBuilder.Entity<ProfitLossDetails>(entity =>
    {
      entity.Property(e => e.ExpenseAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncomeAmt).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<ProfitLossMaster>(entity =>
    {
      entity.Property(e => e.DistributedPl).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PlVoucherNo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.ProvisionForExp).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProvisionForTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalPlAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UndistributedPl).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VoucharNo)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Project>(entity =>
    {
      entity.Property(e => e.Projectid).HasColumnName("PROJECTID");
      entity.Property(e => e.Clientcode)
              .HasMaxLength(100)
              .HasColumnName("CLIENTCODE");
      entity.Property(e => e.Clientname)
              .HasMaxLength(100)
              .HasColumnName("CLIENTNAME");
      entity.Property(e => e.Companyid).HasColumnName("COMPANYID");
      entity.Property(e => e.Createddate)
              .HasColumnType("datetime")
              .HasColumnName("CREATEDDATE");
      entity.Property(e => e.Lastupdatedate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.Owneremployeeid).HasColumnName("OWNEREMPLOYEEID");
      entity.Property(e => e.Projectcode)
              .HasMaxLength(50)
              .HasColumnName("PROJECTCODE");
      entity.Property(e => e.Projectdescription).HasColumnName("PROJECTDESCRIPTION");
      entity.Property(e => e.Projectname)
              .HasMaxLength(100)
              .HasColumnName("PROJECTNAME");
      entity.Property(e => e.Projectownerdepartmentid).HasColumnName("PROJECTOWNERDEPARTMENTID");
      entity.Property(e => e.Repositoryname)
              .HasMaxLength(500)
              .HasColumnName("REPOSITORYNAME");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.Userid).HasColumnName("USERID");
    });

    modelBuilder.Entity<ProjectAssignment>(entity =>
    {
      entity.HasKey(e => e.AssignmentId);

      entity.Property(e => e.AssignDate).HasColumnType("datetime");
      entity.Property(e => e.Assigndepartmentid).HasColumnName("ASSIGNDEPARTMENTID");
      entity.Property(e => e.CompletionDate).HasColumnType("datetime");
      entity.Property(e => e.TargetDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ProjectTask>(entity =>
    {
      entity.HasKey(e => e.TaskId);

      entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
      entity.Property(e => e.TaskName).HasMaxLength(100);
    });

    modelBuilder.Entity<PromotionGuideLine>(entity =>
    {
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Psolocation>(entity =>
    {
      entity.ToTable("PSOLocation");

      entity.Property(e => e.PsolocationId).HasColumnName("PSOLocationId");
      entity.Property(e => e.DsmlocationCode)
              .HasMaxLength(500)
              .HasColumnName("DSMLocationCode");
      entity.Property(e => e.PsolocationCode)
              .HasMaxLength(500)
              .HasColumnName("PSOLocationCode");
      entity.Property(e => e.PsolocationName)
              .HasMaxLength(500)
              .HasColumnName("PSOLocationName");
    });

    modelBuilder.Entity<PunishmentDetails>(entity =>
    {
      entity.Property(e => e.AffectTime).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<PunishmentInfo>(entity =>
    {
      entity.HasKey(e => e.PunishmentId).HasName("PK_PunishmentInfo1");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.PunishmentAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PunishmentDate).HasColumnType("datetime");
      entity.Property(e => e.PunishmentDays).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PunishmentPercentage).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Remarks).HasMaxLength(500);
    });

    modelBuilder.Entity<PunishmentSettings>(entity =>
    {
      entity.HasKey(e => e.PunishmentSettingId);

      entity.Property(e => e.PunishmentAccountHead).HasMaxLength(50);
      entity.Property(e => e.PunishmentWithAccountHead).HasMaxLength(50);
    });

    modelBuilder.Entity<PurchaseOrderDetails>(entity =>
    {
      entity.HasKey(e => e.PurchaseOrderId).HasName("PK_PurchaseOrder");

      entity.Property(e => e.DeliveryTime).HasMaxLength(50);
      entity.Property(e => e.PoNumber).HasMaxLength(100);
      entity.Property(e => e.PoRemarks).HasMaxLength(50);
      entity.Property(e => e.Podate)
              .HasColumnType("datetime")
              .HasColumnName("PODate");
      entity.Property(e => e.PostatusId).HasColumnName("POStatusId");
      entity.Property(e => e.Povalue)
              .HasMaxLength(50)
              .HasColumnName("POValue");
      entity.Property(e => e.SubClientId).HasMaxLength(100);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<QuestionCategory>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.QuestionCategoryCode).HasMaxLength(50);
      entity.Property(e => e.QuestionCategoryDescription).HasMaxLength(50);
      entity.Property(e => e.QuestionCategoryId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<RcruitmentJobVacancy>(entity =>
    {
      entity.HasKey(e => e.JobVacancyId);

      entity.ToTable("Rcruitment_JobVacancy ");

      entity.Property(e => e.AddProvidedDate).HasColumnType("datetime");
      entity.Property(e => e.AdditionalRequirment).HasColumnType("text");
      entity.Property(e => e.AgeOnDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Classification)
              .HasMaxLength(250)
              .HasColumnName("classification");
      entity.Property(e => e.CvcollectedDate)
              .HasColumnType("datetime")
              .HasColumnName("CVCollectedDate");
      entity.Property(e => e.DesiredEmploymetDate).HasColumnType("datetime");
      entity.Property(e => e.EducationalRequirment).HasColumnType("text");
      entity.Property(e => e.ExperienceRequirment).HasColumnType("text");
      entity.Property(e => e.FinishDate).HasColumnType("datetime");
      entity.Property(e => e.JobAnnounceDate).HasColumnType("datetime");
      entity.Property(e => e.JobAnnounceExpireDate).HasColumnType("datetime");
      entity.Property(e => e.JobProfileAttacement).HasColumnType("text");
      entity.Property(e => e.JobResponsibility).HasColumnType("text");
      entity.Property(e => e.JobTitle)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.LanguagesRequirement).HasMaxLength(250);
      entity.Property(e => e.OtherBenefits).HasMaxLength(500);
      entity.Property(e => e.PostedJobReferenceNo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Reason).HasMaxLength(150);
      entity.Property(e => e.RecomendDate).HasColumnType("datetime");
      entity.Property(e => e.ReferenceId).HasMaxLength(250);
      entity.Property(e => e.Remarks).HasMaxLength(500);
      entity.Property(e => e.RequestedBySbu).HasColumnName("RequestedBySBU");
      entity.Property(e => e.RequestedDate).HasColumnType("datetime");
      entity.Property(e => e.RequirmentForThisPoistion).HasMaxLength(500);
      entity.Property(e => e.RequisitionReference).HasMaxLength(500);
      entity.Property(e => e.ServiceArea).HasMaxLength(250);
      entity.Property(e => e.Status)
              .HasDefaultValue(0)
              .HasComment("Status 0=Unpulish,1=Publish");
    });

    modelBuilder.Entity<RcruitmentRequisitionForm>(entity =>
    {
      entity.HasKey(e => e.JobVacancyId);

      entity.ToTable("Rcruitment_RequisitionForm ");

      entity.Property(e => e.AdditionalRequirment).HasColumnType("text");
      entity.Property(e => e.AnnualPackageCharge).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Classification).HasMaxLength(250);
      entity.Property(e => e.DesiredEmploymetDate).HasColumnType("datetime");
      entity.Property(e => e.EducationalRequirment).HasColumnType("text");
      entity.Property(e => e.ExperienceRequirment).HasColumnType("text");
      entity.Property(e => e.JobAnnounceDate).HasColumnType("datetime");
      entity.Property(e => e.JobAnnounceExpireDate).HasColumnType("datetime");
      entity.Property(e => e.JobProfileAttacement).HasColumnType("text");
      entity.Property(e => e.JobResponsibility).HasColumnType("text");
      entity.Property(e => e.JobTitle)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.LanguagesRequirement).HasColumnType("text");
      entity.Property(e => e.NewRecruitmentRemarks).HasMaxLength(250);
      entity.Property(e => e.ProfessionalCategory).HasMaxLength(250);
      entity.Property(e => e.Reason).HasMaxLength(100);
      entity.Property(e => e.RecomendDate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasMaxLength(500);
      entity.Property(e => e.ReplacedPersonDetail).HasMaxLength(250);
      entity.Property(e => e.ReplacementDueToAbsenceRemarks).HasMaxLength(250);
      entity.Property(e => e.ReplacementDueToTransferRemarks).HasMaxLength(250);
      entity.Property(e => e.ReplacementReasonOtherDueToDeparture).HasMaxLength(250);
      entity.Property(e => e.RequestedBySbu).HasColumnName("RequestedBySBU");
      entity.Property(e => e.RequestedDate).HasColumnType("datetime");
      entity.Property(e => e.ReviewDate).HasColumnType("datetime");
      entity.Property(e => e.TermContractValidFrom).HasColumnType("datetime");
      entity.Property(e => e.TermContractValidTo).HasColumnType("datetime");
    });

    modelBuilder.Entity<ReasonOfReview>(entity =>
    {
      entity.HasKey(e => e.RemarksId).HasName("PK_ManpowerPlanningReviewRemarks");

      entity.Property(e => e.ActionType).HasMaxLength(500);
      entity.Property(e => e.RemarksDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecApplicantTraining>(entity =>
    {
      entity.HasKey(e => e.TrainingId).HasName("PK_Training");

      entity.ToTable("Rec_ApplicantTraining");

      entity.Property(e => e.Duration).HasMaxLength(50);
      entity.Property(e => e.Trainer).HasMaxLength(1000);
      entity.Property(e => e.TrainingTypeName).HasMaxLength(1000);
    });

    modelBuilder.Entity<RecCandidateJoining>(entity =>
    {
      entity.HasKey(e => e.CandidateJoiningId);

      entity.ToTable("Rec_CandidateJoining");

      entity.Property(e => e.AppointedDate).HasColumnType("datetime");
      entity.Property(e => e.AppointmentCopyTo).HasMaxLength(500);
      entity.Property(e => e.AppointmentLetterSentDate).HasColumnType("datetime");
      entity.Property(e => e.AppointmentSignatoryEmpId).HasMaxLength(25);
      entity.Property(e => e.AppointmentSignatoryEmpName).HasMaxLength(250);
      entity.Property(e => e.CircularGeneratedDate).HasColumnType("datetime");
      entity.Property(e => e.CircularMailSentDate).HasColumnType("datetime");
      entity.Property(e => e.IdGenerateDate).HasColumnType("datetime");
      entity.Property(e => e.JoinedDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningCancelDate).HasColumnType("datetime");
      entity.Property(e => e.JoiningLetterRefNo).HasMaxLength(250);
      entity.Property(e => e.JoiningLetterSentDate).HasColumnType("datetime");
      entity.Property(e => e.NotifieDate).HasColumnType("datetime");
      entity.Property(e => e.TemporaryFromDate).HasColumnType("datetime");
      entity.Property(e => e.TemporaryToDate).HasColumnType("datetime");
      entity.Property(e => e.TraineeReumenarationAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<RecCandidateSalary>(entity =>
    {
      entity.HasKey(e => e.SalaryInfoId);

      entity.ToTable("Rec_CandidateSalary");

      entity.Property(e => e.CtcAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<RecCompetencies>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Rec_Competencies");

      entity.Property(e => e.CompetencyName).HasMaxLength(50);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<RecCompitencyArea>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Rec_CompitencyArea");

      entity.Property(e => e.CompitencyAreaName).HasMaxLength(100);
      entity.Property(e => e.Description).HasMaxLength(500);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
      entity.Property(e => e.MaxMark).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MaxMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MinMarks).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<RecEducationalVerification>(entity =>
    {
      entity.HasKey(e => e.EducationVerificationId);

      entity.ToTable("Rec_EducationalVerification");

      entity.Property(e => e.CertificateTypeName).HasMaxLength(250);
      entity.Property(e => e.Others).HasMaxLength(250);
    });

    modelBuilder.Entity<RecEmailBody>(entity =>
    {
      entity.HasKey(e => e.EmailBodyId);

      entity.ToTable("Rec_EmailBody");

      entity.Property(e => e.EmailBody).IsUnicode(false);
      entity.Property(e => e.EmailBodyName).HasMaxLength(250);
    });

    modelBuilder.Entity<RecEmployeeCategory>(entity =>
    {
      entity.HasKey(e => e.EmployeeCategoryId).HasName("PK_EmployeeCategory");

      entity.ToTable("REC_EmployeeCategory");
    });

    modelBuilder.Entity<RecEmployeeReplacementInfo>(entity =>
    {
      entity.HasKey(e => e.NewReplacementId);

      entity.ToTable("Rec_EmployeeReplacementInfo");
    });

    modelBuilder.Entity<RecEmploymentHistoryCheck>(entity =>
    {
      entity.HasKey(e => e.EmploymentHistoryCheckId);

      entity.ToTable("REC_EmploymentHistoryCheck");

      entity.Property(e => e.UpdateDate).HasMaxLength(50);
    });

    modelBuilder.Entity<RecExperienceVerification>(entity =>
    {
      entity.HasKey(e => e.ExperienceVerificationId);

      entity.ToTable("Rec_ExperienceVerification");

      entity.Property(e => e.CommentsTypeName).HasMaxLength(250);
      entity.Property(e => e.Designation).HasMaxLength(250);
      entity.Property(e => e.FinancialClearanceTypeName).HasMaxLength(250);
      entity.Property(e => e.HonestyTypeName).HasMaxLength(250);
      entity.Property(e => e.MobileNo).HasMaxLength(250);
      entity.Property(e => e.NameofReferee).HasMaxLength(250);
      entity.Property(e => e.Organization).HasMaxLength(250);
      entity.Property(e => e.ProfessionalismTypeName).HasMaxLength(250);
    });

    modelBuilder.Entity<RecInterviewRatingDetails>(entity =>
    {
      entity.HasKey(e => e.InterviewRatingDetailsId);

      entity.ToTable("Rec_InterviewRatingDetails");

      entity.Property(e => e.Rating).HasMaxLength(10);
      entity.Property(e => e.Remarks).HasMaxLength(500);
    });

    modelBuilder.Entity<RecInterviewRatingMaster>(entity =>
    {
      entity.ToTable("Rec_InterviewRatingMaster");

      entity.Property(e => e.FileRef).HasMaxLength(500);
      entity.Property(e => e.InterviewRatingDateTime).HasColumnType("datetime");
      entity.Property(e => e.RatingFrom).HasMaxLength(50);
      entity.Property(e => e.Remarks).HasMaxLength(250);
      entity.Property(e => e.SavedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecInterviewRatingMasterHistory>(entity =>
    {
      entity.HasKey(e => e.RecInterviewRatingMasterId).HasName("PK_Rec_InterviewRatingMaster_1");

      entity.ToTable("Rec_InterviewRatingMaster_history");

      entity.Property(e => e.AssesmentComment).HasMaxLength(500);
      entity.Property(e => e.InterviewRatingDateTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecInvitationType>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Rec_InvitationType");

      entity.Property(e => e.InvitationName).HasMaxLength(250);
    });

    modelBuilder.Entity<RecJobDescription>(entity =>
    {
      entity.HasKey(e => e.JobDescriptionId);

      entity.ToTable("Rec_JobDescription");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.AgeMaximum).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.AgeMinimum).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.ExperienceMaximum).HasMaxLength(250);
      entity.Property(e => e.ExperienceMinimum).HasMaxLength(250);
      entity.Property(e => e.JobResponsibilitiesA).HasColumnName("JobResponsibilities_a");
      entity.Property(e => e.JobResponsibilitiesB).HasColumnName("JobResponsibilities_b");
      entity.Property(e => e.JobResponsibilitiesC).HasColumnName("JobResponsibilities_c");
      entity.Property(e => e.JobResponsibilitiesD).HasColumnName("JobResponsibilities_d");
      entity.Property(e => e.JobResponsibilitiesE).HasColumnName("JobResponsibilities_e");
      entity.Property(e => e.JobResponsibilitiesF).HasColumnName("JobResponsibilities_f");
      entity.Property(e => e.JobResponsibilitiesG).HasColumnName("JobResponsibilities_g");
      entity.Property(e => e.JobResponsibilitiesH).HasColumnName("JobResponsibilities_h");
      entity.Property(e => e.JobResponsibilitiesI).HasColumnName("JobResponsibilities_i");
      entity.Property(e => e.JobResponsibilitiesJ).HasColumnName("JobResponsibilities_j");
      entity.Property(e => e.PurposeOfJob).HasMaxLength(500);
      entity.Property(e => e.ReasonOfApproved).HasMaxLength(500);
      entity.Property(e => e.Remarks).HasMaxLength(250);
      entity.Property(e => e.RequiredSkillA).HasColumnName("RequiredSkill_a");
      entity.Property(e => e.RequiredSkillB).HasColumnName("RequiredSkill_b");
      entity.Property(e => e.RequiredSkillC).HasColumnName("RequiredSkill_c");
      entity.Property(e => e.RequiredSkillD).HasColumnName("RequiredSkill_d");
      entity.Property(e => e.RequiredSkillE).HasColumnName("RequiredSkill_e");
      entity.Property(e => e.RequiredSkillF).HasColumnName("RequiredSkill_f");
      entity.Property(e => e.SalaryMaximum).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryMinimum).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecJobDescriptionSetup>(entity =>
    {
      entity.HasKey(e => e.JobDescriptionSetupId);

      entity.ToTable("Rec_JobDescriptionSetup");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.JobResponsibilitiesA).HasColumnName("JobResponsibilities_a");
      entity.Property(e => e.RequiredSkillA).HasColumnName("RequiredSkill_a");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecJobIdSelectionDetails>(entity =>
    {
      entity.HasKey(e => e.JobIdSelectionDetailsId);

      entity.ToTable("Rec_JobIdSelectionDetails");

      entity.Property(e => e.RejectedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecJobIdSelectionMaster>(entity =>
    {
      entity.HasKey(e => e.JobIdSelectionMasterId);

      entity.ToTable("Rec_JobIdSelectionMaster");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.ManpowerRequistionReportFilePath).HasMaxLength(250);
      entity.Property(e => e.RefCode).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecJobVacancy>(entity =>
    {
      entity.HasKey(e => e.RecJobVacancyId).HasName("PK_Rec_JobVacancy_1");

      entity.ToTable("Rec_JobVacancy");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CvsortingCommitteOfficeCircularPath)
              .HasMaxLength(250)
              .HasColumnName("CVSortingCommitteOfficeCircularPath");
      entity.Property(e => e.CvsortingCopyTo)
              .HasMaxLength(250)
              .HasColumnName("CVSortingCopyTo");
      entity.Property(e => e.CvsortingDesgExtra)
              .HasMaxLength(250)
              .HasColumnName("CVSortingDesgExtra");
      entity.Property(e => e.CvsortingMode).HasColumnName("CVSortingMode");
      entity.Property(e => e.CvsortingModeInfo)
              .HasMaxLength(500)
              .HasColumnName("CVSortingModeInfo");
      entity.Property(e => e.CvsortingModeVenue)
              .HasMaxLength(250)
              .HasColumnName("CVSortingModeVenue");
      entity.Property(e => e.CvsortingRefNo)
              .HasMaxLength(250)
              .HasColumnName("CVSortingRefNo");
      entity.Property(e => e.CvsortingSignatoryId)
              .HasMaxLength(250)
              .HasColumnName("CVSortingSignatoryId");
      entity.Property(e => e.DeadlineCvsorting).HasColumnName("DeadlineCVSorting");
      entity.Property(e => e.EmploymentStatus).HasMaxLength(250);
      entity.Property(e => e.InterviewModeInfo).HasMaxLength(500);
      entity.Property(e => e.InterviewModeVenue).HasMaxLength(250);
      entity.Property(e => e.InterviewPanelCopyTo).HasMaxLength(750);
      entity.Property(e => e.InterviewerDesgExtra).HasMaxLength(250);
      entity.Property(e => e.InterviewerSignatoryId).HasMaxLength(250);
      entity.Property(e => e.InterviewerVanue).HasMaxLength(250);
      entity.Property(e => e.JobExpireDate).HasColumnType("datetime");
      entity.Property(e => e.JobPublishDate).HasColumnType("datetime");
      entity.Property(e => e.JobVacancyReportPath).HasMaxLength(250);
      entity.Property(e => e.LastDeadlineDate).HasColumnType("datetime");
      entity.Property(e => e.OtherAdvertisement).HasMaxLength(350);
      entity.Property(e => e.PostedJobReferenceNo).HasMaxLength(50);
      entity.Property(e => e.ReplacementReason).HasMaxLength(250);
      entity.Property(e => e.SalaryMax).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.SalaryMin).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TalentAssesmentOfficeCircularPath).HasMaxLength(250);
      entity.Property(e => e.TalentAssesmentRefNo).HasMaxLength(250);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecPresentationMarks>(entity =>
    {
      entity.HasKey(e => e.PresentationMarksId);

      entity.ToTable("Rec_PresentationMarks");

      entity.Property(e => e.PresentationMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SavedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecPresentationMarksHistory>(entity =>
    {
      entity.HasKey(e => e.PresentationMarksId).HasName("PK_Rec_PresentationMarks_1");

      entity.ToTable("Rec_PresentationMarks_history");

      entity.Property(e => e.PresentationMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SavedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecReferenceCheck>(entity =>
    {
      entity.HasKey(e => e.ReferenceCheckId);

      entity.ToTable("Rec_ReferenceCheck");
    });

    modelBuilder.Entity<RecRequisitionHeldInfo>(entity =>
    {
      entity.HasKey(e => e.HoldId);

      entity.ToTable("Rec_RequisitionHeldInfo");

      entity.Property(e => e.HoldDate).HasColumnType("datetime");
      entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
      entity.Property(e => e.SavedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecSalaryMatchingInformation>(entity =>
    {
      entity.HasKey(e => e.SalaryMatchId);

      entity.ToTable("Rec_SalaryMatchingInformation");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ChangeInMonthlyCtc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ChangeInMonthlyGross).HasColumnType("decimal(18, 3)");
      entity.Property(e => e.Comments).HasMaxLength(250);
      entity.Property(e => e.CurrentMonthlyGross).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpectedSalary)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("ExpectedSAlary");
      entity.Property(e => e.ItsAvgSalary).HasColumnName("ItsAvgSAlary");
      entity.Property(e => e.ItsAvrExp).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ItsMaxExp).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ItsMinExp).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.JoiningBonus).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NegotiatedMonthlyGross).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProposedMonthlyGross).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryDescription).HasMaxLength(250);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecSecInterviewRating>(entity =>
    {
      entity.HasKey(e => e.RceSecondIntRatingId);

      entity.ToTable("Rec_SecInterviewRating");

      entity.Property(e => e.FileRef).HasMaxLength(500);
      entity.Property(e => e.RatingFrom).HasMaxLength(500);
      entity.Property(e => e.SaveDate).HasColumnType("datetime");
      entity.Property(e => e.SecInterviewRatingDateTime).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecSecondInterview>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Rec_SecondInterview");

      entity.Property(e => e.SecondInterviewDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecThirdInterviewRating>(entity =>
    {
      entity.HasKey(e => e.RceThirdIntRatingId);

      entity.ToTable("Rec_ThirdInterviewRating");

      entity.Property(e => e.FileRef).HasMaxLength(500);
      entity.Property(e => e.RatingFrom).HasMaxLength(500);
      entity.Property(e => e.SaveDate).HasColumnType("datetime");
      entity.Property(e => e.ThirdInterviewRatingDateTime).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecWrittenMarks>(entity =>
    {
      entity.HasKey(e => e.WrittenMarksId);

      entity.ToTable("Rec_WrittenMarks");

      entity.Property(e => e.EducationMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.RollNumber).HasMaxLength(250);
      entity.Property(e => e.SavedDate).HasColumnType("datetime");
      entity.Property(e => e.WrittenMarks).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<RecWrittenMarksHistory>(entity =>
    {
      entity.HasKey(e => e.WrittenMarksId).HasName("PK_Rec_WrittenMarks_1");

      entity.ToTable("Rec_WrittenMarks_history");

      entity.Property(e => e.SavedDate).HasColumnType("datetime");
      entity.Property(e => e.WrittenMarks).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<RecruitmentApplicantEducation>(entity =>
    {
      entity.HasKey(e => e.ApplicantEducationId);

      entity.ToTable("Recruitment_ApplicantEducation");

      entity.Property(e => e.Board)
              .HasMaxLength(250)
              .HasColumnName("BOARD");
      entity.Property(e => e.Certificate).HasMaxLength(250);
      entity.Property(e => e.Certificatetypeid).HasColumnName("CERTIFICATETYPEID");
      entity.Property(e => e.ExamOrDegreeTitle).HasMaxLength(250);
      entity.Property(e => e.Institute).HasMaxLength(250);
      entity.Property(e => e.MajorOrGroup).HasMaxLength(250);
      entity.Property(e => e.OtherInstitute).HasMaxLength(250);
      entity.Property(e => e.Result).HasMaxLength(50);
      entity.Property(e => e.Yearofcompletion).HasMaxLength(50);
    });

    modelBuilder.Entity<RecruitmentApplicantEmploymentHistory>(entity =>
    {
      entity.HasKey(e => e.ApplicantEmploymentHistoryId);

      entity.ToTable("Recruitment_ApplicantEmploymentHistory");

      entity.Property(e => e.CompanyName).HasMaxLength(500);
      entity.Property(e => e.DepartmentName).HasMaxLength(500);
      entity.Property(e => e.Designation).HasMaxLength(500);
      entity.Property(e => e.ExperienceYear).HasMaxLength(500);
      entity.Property(e => e.FromDate).HasMaxLength(50);
      entity.Property(e => e.ToDate).HasMaxLength(50);
    });

    modelBuilder.Entity<RecruitmentApplicantReferance>(entity =>
    {
      entity.HasKey(e => e.ApplicantReferanceId);

      entity.ToTable("Recruitment_ApplicantReferance");

      entity.Property(e => e.RefAddress).HasMaxLength(500);
      entity.Property(e => e.RefEmail).HasMaxLength(100);
      entity.Property(e => e.RefMobile).HasMaxLength(50);
      entity.Property(e => e.RefOccupation).HasMaxLength(1000);
      entity.Property(e => e.Relation).HasMaxLength(100);
    });

    modelBuilder.Entity<RecruitmentApplicantSkill>(entity =>
    {
      entity.HasKey(e => e.ApplicantSkillId);

      entity.ToTable("Recruitment_ApplicantSkill");

      entity.Property(e => e.OtherSkill).HasMaxLength(250);
      entity.Property(e => e.SkillId).HasColumnName("skillId");
    });

    modelBuilder.Entity<RecruitmentEligibleType>(entity =>
    {
      entity.HasKey(e => e.EligibleTypeId);

      entity.Property(e => e.EligibleTypeName).HasMaxLength(250);
    });

    modelBuilder.Entity<RecruitmentSource>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.RecruitmentSourceCode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.RecruitmentSourceId).ValueGeneratedOnAdd();
      entity.Property(e => e.RecruitmentSourceName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RecruitmentStandardForm>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Recruitment_StandardForm");

      entity.Property(e => e.DowloadPath)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.Extention)
              .HasMaxLength(10)
              .IsUnicode(false);
      entity.Property(e => e.FileName)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.FormId).ValueGeneratedOnAdd();
      entity.Property(e => e.FormTitle)
              .HasMaxLength(1000)
              .IsUnicode(false);
    });

    modelBuilder.Entity<RecruitmentType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.RecruitmentTypeCode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.RecruitmentTypeId).ValueGeneratedOnAdd();
      entity.Property(e => e.RecruitmentTypeName)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RefRequisitionSn>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RefRequisitionSN");

      entity.Property(e => e.Sl).HasColumnName("SL");
    });

    modelBuilder.Entity<ReferenceType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ReferenceTypeName)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<RegionDivisionMap>(entity =>
    {
      entity.HasNoKey();
    });

    modelBuilder.Entity<RelationShip>(entity =>
    {
      entity.Property(e => e.RelationShipCode).HasMaxLength(50);
      entity.Property(e => e.RelationShipName).HasMaxLength(50);
    });

    modelBuilder.Entity<Religion>(entity =>
    {
      entity.Property(e => e.RelegionCode).HasMaxLength(50);
      entity.Property(e => e.ReligionName).HasMaxLength(50);
    });

    modelBuilder.Entity<ReligionCtcMapping>(entity =>
    {
      entity.HasKey(e => e.MappingId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.InactiveDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RemunerationSuspension>(entity =>
    {
      entity.HasKey(e => e.RemunerationSusId);
    });

    modelBuilder.Entity<ReportBuilder>(entity =>
    {
      entity.HasKey(e => e.ReportHeaderId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OrderByColumn)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.ReportHeader).HasMaxLength(250);
      entity.Property(e => e.ReportTitle).HasMaxLength(250);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ReportSignatoryMapping>(entity =>
    {
      entity.HasKey(e => e.SignatoryMappingId);

      entity.Property(e => e.Description).HasMaxLength(500);
      entity.Property(e => e.SignatoryDesignationName).HasMaxLength(500);
    });

    modelBuilder.Entity<ReportToApproverTemp>(entity =>
    {
      entity.HasKey(e => e.ReportToApproverId);

      entity.ToTable("ReportToApproverTEMP");

      entity.Property(e => e.Approver).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.ReportTo).HasMaxLength(50);
    });

    modelBuilder.Entity<ReqruitmentEducationalInstitute>(entity =>
    {
      entity.HasKey(e => e.InstituteId);

      entity.ToTable("Reqruitment_EducationalInstitute");

      entity.Property(e => e.InstituteName).HasMaxLength(250);
    });

    modelBuilder.Entity<ResignationAcceptanceLetterType>(entity =>
    {
      entity.HasKey(e => e.AccepetanceTypeId).HasName("PK_ResignationAccepetanceLetterType");

      entity.Property(e => e.AccepetanceType).HasMaxLength(50);
    });

    modelBuilder.Entity<ResignationApplication>(entity =>
    {
      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Reason).HasMaxLength(550);
      entity.Property(e => e.ResignationDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ResignationMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ResignationMappingId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<ResignationRemarks>(entity =>
    {
      entity.HasKey(e => e.ResignationId);

      entity.Property(e => e.ResignationRemarksName)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<RevDistributionDetails>(entity =>
    {
      entity.HasKey(e => e.RevDistributionId);

      entity.Property(e => e.IndividualCpf)
              .HasColumnType("decimal(18, 5)")
              .HasColumnName("IndividualCPf");
      entity.Property(e => e.IndividualIncome).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.IndividualPf).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.IndividualTotal).HasColumnType("decimal(18, 5)");
    });

    modelBuilder.Entity<RevDistributionMaster>(entity =>
    {
      entity.Property(e => e.EmployeeActive).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.EmployeeInActive).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.EmployeeRevenue).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.EmployerActive).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.EmployerInActive).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.EmployerRevenue).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.RevRatioActiveOwn).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.TotalContribution).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.TotalProduct).HasColumnType("decimal(18, 5)");
      entity.Property(e => e.TotalRevenue).HasColumnType("decimal(18, 5)");
    });

    modelBuilder.Entity<Reward>(entity =>
    {
      entity.ToTable("REWARD");

      entity.Property(e => e.Rewardid).HasColumnName("REWARDID");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Natureofreward)
              .HasMaxLength(500)
              .HasColumnName("NATUREOFREWARD");
      entity.Property(e => e.Rewarddate)
              .HasColumnType("datetime")
              .HasColumnName("REWARDDATE");
      entity.Property(e => e.Rewarddescription)
              .HasMaxLength(1000)
              .HasColumnName("REWARDDESCRIPTION");
      entity.Property(e => e.Uploadfile)
              .HasMaxLength(1000)
              .HasColumnName("UPLOADFILE");
    });

    modelBuilder.Entity<RewardDistribution>(entity =>
    {
      entity.Property(e => e.DistributionDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RewardEligibility>(entity =>
    {
      entity.Property(e => e.EligibilityDescription)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<RewardGenerate>(entity =>
    {
      entity.Property(e => e.NatureOfReward).HasMaxLength(50);
      entity.Property(e => e.RewardGenerateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RewardGenerateDetails>(entity =>
    {
      entity.HasKey(e => e.RewardGenDetailsId);

      entity.Property(e => e.ActualServiceLength)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<RewardPenaltyGuideLine>(entity =>
    {
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RollingGroup>(entity =>
    {
      entity.HasKey(e => e.RollGroupId);

      entity.Property(e => e.RollGroupName)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<RollingGroupAndPolicyMap>(entity =>
    {
      entity.HasKey(e => e.RollGroupMapId);

      entity.HasOne(d => d.RollGroup).WithMany(p => p.RollingGroupAndPolicyMap)
              .HasForeignKey(d => d.RollGroupId)
              .HasConstraintName("FK_RollingGroupAndPolicyMap_RollingGroup");

      entity.HasOne(d => d.RollingPolicyDetails).WithMany(p => p.RollingGroupAndPolicyMap)
              .HasForeignKey(d => d.RollingPolicyDetailsId)
              .HasConstraintName("FK_RollingGroupAndPolicyMap_ShiftRollingPolicyDetails");

      entity.HasOne(d => d.ShiftRollingPolicy).WithMany(p => p.RollingGroupAndPolicyMap)
              .HasForeignKey(d => d.ShiftRollingPolicyId)
              .HasConstraintName("FK_RollingGroupAndPolicyMap_ShiftRollingPolicy");
    });

    modelBuilder.Entity<RollingGroupDetails>(entity =>
    {
      entity.HasKey(e => e.RollGroupDetailsId);

      entity.HasOne(d => d.RollGroup).WithMany(p => p.RollingGroupDetails)
              .HasForeignKey(d => d.RollGroupId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_RollingGroupDetails_RollingGroup");
    });

    modelBuilder.Entity<Roster>(entity =>
    {
      entity.ToTable("ROSTER");

      entity.HasIndex(e => e.RosterMonth, "IX_ROSTER").IsDescending();

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.RosterMonth).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetails>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrRecordId, e.ShiftId, e.DateValue }, "IX_RosterDetails");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");

      entity.HasOne(d => d.Roster).WithMany()
              .HasForeignKey(d => d.RosterId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_RosterDetails_ROSTER");
    });

    modelBuilder.Entity<RosterDetails18112023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_18_11_2023");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetails27062024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_27_06_2024");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetails29082022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_29_08_2022");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsApproval>(entity =>
    {
      entity.HasKey(e => e.RosterDetailsApprovalId).HasName("PK_RosterDetailsApprovel");

      entity.Property(e => e.DateValue).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsArchive2017>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_Archive_2017");

      entity.Property(e => e.DateValue).HasColumnType("datetime");
      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsArchive2018>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_Archive_2018");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsArchive2019>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_Archive_2019");

      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsArchive2020>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_Archive_2020");

      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsArchive2021>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_Archive_2021");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsArchive2022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_Archive_2022");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDetailsArchive2023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("RosterDetails_Archive_2023");

      entity.Property(e => e.RosterDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterDraftDetails>(entity =>
    {
      entity.ToTable(tb => tb.HasTrigger("tr_RosterDraftDetails"));
    });

    modelBuilder.Entity<RosterDraftDetailsForRl>(entity =>
    {
      entity.HasKey(e => e.RosterDraftDetailsId);

      entity.ToTable("RosterDraftDetails_For_RL");
    });

    modelBuilder.Entity<RosterDraftMaster>(entity =>
    {
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.FromDate).HasColumnType("datetime");
      entity.Property(e => e.IsDo).HasColumnName("isDo");
      entity.Property(e => e.IsNextDayOff).HasColumnName("isNextDayOff");
      entity.Property(e => e.NewShiftId).HasColumnName("newShiftId");
      entity.Property(e => e.RejectDate).HasColumnType("datetime");
      entity.Property(e => e.ToDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterForwardableEmployee>(entity =>
    {
      entity.HasKey(e => e.RosterFwdEmpId);
    });

    modelBuilder.Entity<RosterLock>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.LocakFrom).HasColumnType("datetime");
      entity.Property(e => e.LocakTo).HasColumnType("datetime");
      entity.Property(e => e.LockDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<RosterRollingGroupTemp>(entity =>
    {
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.IsValid).HasMaxLength(250);
      entity.Property(e => e.RollingGroupName).HasMaxLength(250);
      entity.Property(e => e.Status).HasMaxLength(250);
    });

    modelBuilder.Entity<RosterRunMonth>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => e.RosterRunMonth1, "IX_RosterRunMonth").IsDescending();

      entity.Property(e => e.RosterRunMonth1)
              .HasColumnType("datetime")
              .HasColumnName("RosterRunMonth");
      entity.Property(e => e.RosterRunMonthId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Rsmregion>(entity =>
    {
      entity.HasKey(e => e.RsmregionCode);

      entity.ToTable("RSMRegion");

      entity.Property(e => e.RsmregionCode)
              .HasMaxLength(50)
              .HasColumnName("RSMRegionCode");
      entity.Property(e => e.RsmmanagerHrRecordId).HasColumnName("RSMManagerHrRecordId");
      entity.Property(e => e.RsmregionId)
              .ValueGeneratedOnAdd()
              .HasColumnName("RSMRegionId");
      entity.Property(e => e.RsmregionName)
              .HasMaxLength(500)
              .HasColumnName("RSMRegionName");
    });

    modelBuilder.Entity<RsmregionManager>(entity =>
    {
      entity.ToTable("RSMRegionManager");

      entity.Property(e => e.RsmregionManagerId).HasColumnName("RSMRegionManagerId");
      entity.Property(e => e.RsmregionId).HasColumnName("RSMRegionId");
    });

    modelBuilder.Entity<Salary>(entity =>
    {
      entity.HasKey(e => e.SalaryId).HasName("PK_Salary_1");

      entity.HasIndex(e => new { e.SalaryMonth, e.StateId }, "NonClusteredIndex_20200329_172137");

      entity.HasIndex(e => new { e.HrRecordId, e.SalaryMonth }, "NonClusteredIndex_2023122305");

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BankPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.CashPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.InstructionReferenceNo).HasMaxLength(250);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.SalaryRemarks).HasMaxLength(500);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<Salary20230719>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Salary_2023_07_19");

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BankPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.CashPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.InstructionReferenceNo).HasMaxLength(250);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SalaryId).ValueGeneratedOnAdd();
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.SalaryRemarks).HasMaxLength(500);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryBk22072023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Salary_BK_22_07_2023");

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BankPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.CashPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.InstructionReferenceNo).HasMaxLength(250);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SalaryId).ValueGeneratedOnAdd();
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.SalaryRemarks).HasMaxLength(500);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryByAdmin>(entity =>
    {
      entity.HasKey(e => e.SalaryId).HasName("PK_Salary_11");

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.SalaryRemarks).HasMaxLength(500);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryClose>(entity =>
    {
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<SalaryCloseCompanyMap>(entity =>
    {
      entity.HasKey(e => e.SalaryCloseCompanyMapId).HasName("PK_SalaryCloseCompanyMapping");
    });

    modelBuilder.Entity<SalaryCostCentre>(entity =>
    {
      entity.HasKey(e => e.SalaryCostCetreHistoryId);

      entity.HasIndex(e => new { e.HrRecordId, e.CostCentreId }, "NonClusteredIndex_20200329_172200");

      entity.HasIndex(e => e.SalaryMonth, "NonClusteredIndex_20231225_172234");

      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<SalaryDetails>(entity =>
    {
      entity.HasIndex(e => new { e.HrRecordId, e.CtcId, e.CtcValue }, "NonClusteredIndex_20200401_173652");

      entity.HasIndex(e => new { e.SalaryMonth, e.SalaryType }, "Non_Cluster_Index_For_Salary_Details");

      entity.Property(e => e.Arear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcActualValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LwpAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<SalaryDetailsByAdmin>(entity =>
    {
      entity.HasKey(e => e.SalaryDetailsId).HasName("PK_SalaryDetails1");

      entity.Property(e => e.CtcActualValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<SalaryDetailsProcess>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.SalaryMonth, e.StatusId, e.IsAdditional, e.SalaryType }, "NonClasterSalaryDetailsProcessIndex");

      entity.HasIndex(e => new { e.HrRecordId, e.CtcId, e.CtcValue, e.SalaryMonth, e.SalaryType }, "NonClusteredIndex_20201017_122613");

      entity.Property(e => e.Arear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcActualValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LwpAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<SalaryDetailsProcessAudit>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Arear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcActualValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LwpAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<SalaryDetailsTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Arear).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcActualValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LwpAmt).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<SalaryIncrement>(entity =>
    {
      entity.Property(e => e.CostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncrementApproveDate).HasColumnType("datetime");
      entity.Property(e => e.IncrementEffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.IncrementGeneratedDate).HasColumnType("datetime");
      entity.Property(e => e.IncrementMonth).HasColumnType("datetime");
      entity.Property(e => e.MaxClaimedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NetTaxPayable).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewCostToCompany).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.NewGrossPayOnInc).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SpecialAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Status).HasDefaultValue(1);
      entity.Property(e => e.TaxProvidedByCompanyPer).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TaxProvidedByEmployee).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryIncrementDetails>(entity =>
    {
      entity.Property(e => e.CtcValue).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.IncrementCtcValue).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryProcess>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrRecordId, e.SalaryMonth }, "NonClasterSalaryProcessIndex");

      entity.HasIndex(e => new { e.SalaryMonth, e.StateId, e.CompanyId, e.SalaryType }, "NonClastereSalaryProcessIndex");

      entity.HasIndex(e => new { e.SalaryMonth, e.SalaryType }, "NonClastereSalaryProcessIndex_678967");

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BankPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.CashPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.InstructionReferenceNo).HasMaxLength(250);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SalaryId).ValueGeneratedOnAdd();
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.SalaryRemarks).HasMaxLength(500);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryProcess09062024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("SalaryProcess_09_06_2024");

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BankPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.CashPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.InstructionReferenceNo).HasMaxLength(250);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SalaryId).ValueGeneratedOnAdd();
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.SalaryRemarks).HasMaxLength(500);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryProcessAudit>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BankPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.CashPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.InstructionAccountNo).HasMaxLength(50);
      entity.Property(e => e.InstructionReferenceNo).HasMaxLength(250);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentMode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.SalaryRemarks).HasMaxLength(500);
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryRefereneNo>(entity =>
    {
      entity.HasNoKey();
    });

    modelBuilder.Entity<SalaryRemark>(entity =>
    {
      entity.HasKey(e => e.SalaryRemarksId);
    });

    modelBuilder.Entity<SalaryTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ActualBasic).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AdjustmentTax).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.BankAccountNo).HasMaxLength(250);
      entity.Property(e => e.BankPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.CashPaymentDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.GenerateDate).HasColumnType("datetime");
      entity.Property(e => e.GrossPay).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.NetPayout).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<SalaryUploadTemplate>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AbsentAmt).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Arear).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Basic).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.BodyGuardAllow).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.ChairmanAllow).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Consolidated).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Conveyance).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.FoodAllow).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.FoodDed).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Gross).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.HairCut).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.HouseRent).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.LoanInstall).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Medical).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.MobileDed).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.MotorCycleDed).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.NetPay).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.OtherDed).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Pfded)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("PFDed");
      entity.Property(e => e.SalaryAdv).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Special).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TaxReturn).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TransportDed).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.VipdutyAllow)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("VIPDutyAllow");
      entity.Property(e => e.Washing).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<Section>(entity =>
    {
      entity.Property(e => e.SectionCode).HasMaxLength(50);
      entity.Property(e => e.SectionName).HasMaxLength(500);
    });

    modelBuilder.Entity<SectorAAppraisalProScale03072022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Sector_A_Appraisal_Pro_Scale_03_07_2022");

      entity.Property(e => e.Designation).HasMaxLength(150);
      entity.Property(e => e.EmployeeId).HasMaxLength(100);
      entity.Property(e => e.Name).HasMaxLength(150);
      entity.Property(e => e.PayGradeScale).HasMaxLength(150);
    });

    modelBuilder.Entity<SectorAMonglaScale07112022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Sector_A_Mongla_Scale_07_11_2022");

      entity.Property(e => e.Designation).HasMaxLength(150);
      entity.Property(e => e.EmployeeId).HasMaxLength(100);
      entity.Property(e => e.Name).HasMaxLength(150);
      entity.Property(e => e.PayGradeScale).HasMaxLength(150);
    });

    modelBuilder.Entity<SelfKpi>(entity =>
    {
      entity.ToTable("SelfKPI");
    });

    modelBuilder.Entity<SentEmail>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Attachment).HasColumnType("ntext");
      entity.Property(e => e.Body).IsUnicode(false);
      entity.Property(e => e.EmailFrom).HasMaxLength(350);
      entity.Property(e => e.MailCreateDate).HasColumnType("datetime");
      entity.Property(e => e.MailSendDate).HasColumnType("datetime");
      entity.Property(e => e.MenuId).HasColumnName("menuId");
      entity.Property(e => e.SentEmailId).ValueGeneratedOnAdd();
      entity.Property(e => e.Subject).HasMaxLength(1500);
    });

    modelBuilder.Entity<Sheet1>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Sheet1$");

      entity.Property(e => e.CardNo).HasColumnName("Card No");
      entity.Property(e => e.EmployeeName)
              .HasMaxLength(255)
              .HasColumnName("Employee Name");
      entity.Property(e => e.Pf).HasColumnName("PF");
      entity.Property(e => e.Sl).HasColumnName("SL");
    });

    modelBuilder.Entity<ShifRollingPolicyMap>(entity =>
    {
      entity.HasKey(e => e.ShiftRollingMapId);

      entity.HasOne(d => d.ShiftRollingPolicy).WithMany(p => p.ShifRollingPolicyMap)
              .HasForeignKey(d => d.ShiftRollingPolicyId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_ShifRollingPolicyMap_ShiftRollingPolicy");
    });

    modelBuilder.Entity<Shift>(entity =>
    {
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.Allowedlate).HasColumnName("ALLOWEDLATE");
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Dayoffenable).HasColumnName("DAYOFFENABLE");
      entity.Property(e => e.Graceloggofftime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("GRACELOGGOFFTIME");
      entity.Property(e => e.Gracetimein).HasColumnName("GRACETIMEIN");
      entity.Property(e => e.Gracetimeout).HasColumnName("GRACETIMEOUT");
      entity.Property(e => e.Isdefault).HasColumnName("ISDEFAULT");
      entity.Property(e => e.Lateallowed)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("LATEALLOWED");
      entity.Property(e => e.Movealltothisshift).HasColumnName("MOVEALLTOTHISSHIFT");
      entity.Property(e => e.Nextdaylogingracetime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("NEXTDAYLOGINGRACETIME");
      entity.Property(e => e.Officehourdescription)
              .HasMaxLength(2000)
              .HasColumnName("OFFICEHOURDESCRIPTION");
      entity.Property(e => e.Shiftcode)
              .HasMaxLength(50)
              .HasColumnName("SHIFTCODE");
      entity.Property(e => e.Shiftdescription)
              .HasMaxLength(2000)
              .HasColumnName("SHIFTDESCRIPTION");
      entity.Property(e => e.Shiftname)
              .HasMaxLength(100)
              .HasColumnName("SHIFTNAME");
      entity.Property(e => e.Shiftstatus).HasColumnName("SHIFTSTATUS");
      entity.Property(e => e.Shifttype).HasColumnName("SHIFTTYPE");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.WeakName).HasMaxLength(50);
    });

    modelBuilder.Entity<Shift03042022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Shift_03_04_2022");

      entity.Property(e => e.Allowedlate).HasColumnName("ALLOWEDLATE");
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Dayoffenable).HasColumnName("DAYOFFENABLE");
      entity.Property(e => e.Graceloggofftime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("GRACELOGGOFFTIME");
      entity.Property(e => e.Gracetimein).HasColumnName("GRACETIMEIN");
      entity.Property(e => e.Gracetimeout).HasColumnName("GRACETIMEOUT");
      entity.Property(e => e.Isdefault).HasColumnName("ISDEFAULT");
      entity.Property(e => e.Lateallowed)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("LATEALLOWED");
      entity.Property(e => e.Movealltothisshift).HasColumnName("MOVEALLTOTHISSHIFT");
      entity.Property(e => e.Nextdaylogingracetime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("NEXTDAYLOGINGRACETIME");
      entity.Property(e => e.Officehourdescription)
              .HasMaxLength(2000)
              .HasColumnName("OFFICEHOURDESCRIPTION");
      entity.Property(e => e.Shiftcode)
              .HasMaxLength(50)
              .HasColumnName("SHIFTCODE");
      entity.Property(e => e.Shiftdescription)
              .HasMaxLength(2000)
              .HasColumnName("SHIFTDESCRIPTION");
      entity.Property(e => e.Shiftid)
              .ValueGeneratedOnAdd()
              .HasColumnName("SHIFTID");
      entity.Property(e => e.Shiftname)
              .HasMaxLength(100)
              .HasColumnName("SHIFTNAME");
      entity.Property(e => e.Shiftstatus).HasColumnName("SHIFTSTATUS");
      entity.Property(e => e.Shifttype).HasColumnName("SHIFTTYPE");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.WeakName).HasMaxLength(50);
    });

    modelBuilder.Entity<Shift25032023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Shift_25_03_2023");

      entity.Property(e => e.Allowedlate).HasColumnName("ALLOWEDLATE");
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Dayoffenable).HasColumnName("DAYOFFENABLE");
      entity.Property(e => e.Graceloggofftime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("GRACELOGGOFFTIME");
      entity.Property(e => e.Gracetimein).HasColumnName("GRACETIMEIN");
      entity.Property(e => e.Gracetimeout).HasColumnName("GRACETIMEOUT");
      entity.Property(e => e.Isdefault).HasColumnName("ISDEFAULT");
      entity.Property(e => e.Lateallowed)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("LATEALLOWED");
      entity.Property(e => e.Movealltothisshift).HasColumnName("MOVEALLTOTHISSHIFT");
      entity.Property(e => e.Nextdaylogingracetime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("NEXTDAYLOGINGRACETIME");
      entity.Property(e => e.Officehourdescription)
              .HasMaxLength(2000)
              .HasColumnName("OFFICEHOURDESCRIPTION");
      entity.Property(e => e.Shiftcode)
              .HasMaxLength(50)
              .HasColumnName("SHIFTCODE");
      entity.Property(e => e.Shiftdescription)
              .HasMaxLength(2000)
              .HasColumnName("SHIFTDESCRIPTION");
      entity.Property(e => e.Shiftid)
              .ValueGeneratedOnAdd()
              .HasColumnName("SHIFTID");
      entity.Property(e => e.Shiftname)
              .HasMaxLength(100)
              .HasColumnName("SHIFTNAME");
      entity.Property(e => e.Shiftstatus).HasColumnName("SHIFTSTATUS");
      entity.Property(e => e.Shifttype).HasColumnName("SHIFTTYPE");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.WeakName).HasMaxLength(50);
    });

    modelBuilder.Entity<ShiftBk12032024Rmdn>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Shift_BK_12_03_2024_RMDN");

      entity.Property(e => e.Allowedlate).HasColumnName("ALLOWEDLATE");
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Dayoffenable).HasColumnName("DAYOFFENABLE");
      entity.Property(e => e.Graceloggofftime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("GRACELOGGOFFTIME");
      entity.Property(e => e.Gracetimein).HasColumnName("GRACETIMEIN");
      entity.Property(e => e.Gracetimeout).HasColumnName("GRACETIMEOUT");
      entity.Property(e => e.Isdefault).HasColumnName("ISDEFAULT");
      entity.Property(e => e.Lateallowed)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("LATEALLOWED");
      entity.Property(e => e.Movealltothisshift).HasColumnName("MOVEALLTOTHISSHIFT");
      entity.Property(e => e.Nextdaylogingracetime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("NEXTDAYLOGINGRACETIME");
      entity.Property(e => e.Officehourdescription)
              .HasMaxLength(2000)
              .HasColumnName("OFFICEHOURDESCRIPTION");
      entity.Property(e => e.Shiftcode)
              .HasMaxLength(50)
              .HasColumnName("SHIFTCODE");
      entity.Property(e => e.Shiftdescription)
              .HasMaxLength(2000)
              .HasColumnName("SHIFTDESCRIPTION");
      entity.Property(e => e.Shiftid)
              .ValueGeneratedOnAdd()
              .HasColumnName("SHIFTID");
      entity.Property(e => e.Shiftname)
              .HasMaxLength(100)
              .HasColumnName("SHIFTNAME");
      entity.Property(e => e.Shiftstatus).HasColumnName("SHIFTSTATUS");
      entity.Property(e => e.Shifttype).HasColumnName("SHIFTTYPE");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.WeakName).HasMaxLength(50);
    });

    modelBuilder.Entity<ShiftHistory>(entity =>
    {
      entity.Property(e => e.Shifthistoryid).HasColumnName("SHIFTHISTORYID");
      entity.Property(e => e.Allowedlate).HasColumnName("ALLOWEDLATE");
      entity.Property(e => e.BreakupDuration).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Dayoffenable).HasColumnName("DAYOFFENABLE");
      entity.Property(e => e.Graceloggofftime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("GRACELOGGOFFTIME");
      entity.Property(e => e.Gracetimein).HasColumnName("GRACETIMEIN");
      entity.Property(e => e.Gracetimeout).HasColumnName("GRACETIMEOUT");
      entity.Property(e => e.HistoryEffectEndDate).HasColumnType("datetime");
      entity.Property(e => e.HistoryEffectStartDate).HasColumnType("datetime");
      entity.Property(e => e.Isdefault).HasColumnName("ISDEFAULT");
      entity.Property(e => e.Lateallowed)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("LATEALLOWED");
      entity.Property(e => e.Movealltothisshift).HasColumnName("MOVEALLTOTHISSHIFT");
      entity.Property(e => e.Nextdaylogingracetime)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("NEXTDAYLOGINGRACETIME");
      entity.Property(e => e.Officehourdescription)
              .HasMaxLength(2000)
              .HasColumnName("OFFICEHOURDESCRIPTION");
      entity.Property(e => e.Shiftcode)
              .HasMaxLength(50)
              .HasColumnName("SHIFTCODE");
      entity.Property(e => e.Shiftdescription)
              .HasMaxLength(2000)
              .HasColumnName("SHIFTDESCRIPTION");
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.Shiftname)
              .HasMaxLength(100)
              .HasColumnName("SHIFTNAME");
      entity.Property(e => e.Shiftstatus).HasColumnName("SHIFTSTATUS");
      entity.Property(e => e.Shifttype).HasColumnName("SHIFTTYPE");
      entity.Property(e => e.ShortLeaveHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
      entity.Property(e => e.WeakName).HasMaxLength(50);
    });

    modelBuilder.Entity<ShiftMapping>(entity =>
    {
      entity.HasKey(e => e.ShiftMapId);

      entity.Property(e => e.LunchHour).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ReferenceType).HasComment("");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ShiftRollingPolicy>(entity =>
    {
      entity.Property(e => e.DayOffType).HasComment("1=Weekly,2=Fortnightly,3=Monthly");
      entity.Property(e => e.RollingDuration).HasMaxLength(50);
      entity.Property(e => e.RollingPolicyName).HasMaxLength(50);
      entity.Property(e => e.ShiftReplaceByDo)
              .HasDefaultValue(false)
              .HasColumnName("ShiftReplaceByDO");
    });

    modelBuilder.Entity<ShiftRollingPolicyDetails>(entity =>
    {
      entity.HasKey(e => e.RollingPolicyDetailsId);

      entity.HasOne(d => d.ShiftRollingPolicy).WithMany(p => p.ShiftRollingPolicyDetails)
              .HasForeignKey(d => d.ShiftRollingPolicyId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_ShiftRollingPolicyDetails_ShiftRollingPolicy");
    });

    modelBuilder.Entity<SiclMngEmp29012023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("SiclMNG_Emp_29_01_2023");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<SiclSecAEnhanced08082022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("SICL_SecA_Enhanced_08_08_2022");

      entity.Property(e => e.EmpId).HasMaxLength(50);
      entity.Property(e => e.NewGross).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.NewSp)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("NewSP");
    });

    modelBuilder.Entity<SimNumberSettings>(entity =>
    {
      entity.HasKey(e => e.PhoneSettingsId).HasName("PK_PhoneNoSettings");

      entity.Property(e => e.PhoneNumber)
              .HasMaxLength(20)
              .IsUnicode(false);
    });

    modelBuilder.Entity<SliderPicture>(entity =>
    {
      entity.Property(e => e.PictureTite)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.SliderPicturePath)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Smsrecieved>(entity =>
    {
      entity.ToTable("SMSRecieved");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.FromMobileNumber).HasMaxLength(25);
      entity.Property(e => e.RecievedDate).HasColumnType("datetime");
      entity.Property(e => e.SimNumber).HasMaxLength(25);
      entity.Property(e => e.Smsindex).HasColumnName("SMSIndex");
      entity.Property(e => e.Smstext)
              .HasMaxLength(1000)
              .HasColumnName("SMSText");
      entity.Property(e => e.SystemDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Smssent>(entity =>
    {
      entity.ToTable("SMSSent");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.DeliveryDateTime).HasColumnType("datetime");
      entity.Property(e => e.MobileNumber).HasMaxLength(25);
      entity.Property(e => e.RequestDateTime).HasColumnType("datetime");
      entity.Property(e => e.SimNumber).HasMaxLength(25);
      entity.Property(e => e.Smstext)
              .HasMaxLength(1000)
              .HasColumnName("SMSText");
    });

    modelBuilder.Entity<Sqanswer>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("SQAnswer");

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.SortOrder)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SqanswerId)
              .ValueGeneratedOnAdd()
              .HasColumnName("SQAnswerId");
    });

    modelBuilder.Entity<SubKpi>(entity =>
    {
      entity.ToTable("SUB_KPI");

      entity.Property(e => e.SubKpiId).HasColumnName("SUB_KPI_ID");
      entity.Property(e => e.ParentKpiId).HasColumnName("PARENT_KPI_ID");
      entity.Property(e => e.SortOrder).HasColumnName("SORT_ORDER");
      entity.Property(e => e.Status)
              .HasDefaultValue(0)
              .HasColumnName("STATUS");
      entity.Property(e => e.SubKpiName)
              .HasMaxLength(100)
              .HasColumnName("SUB_KPI_NAME");
      entity.Property(e => e.SubKpiTotalMarksOfParrent).HasColumnName("SUB_KPI_TOTAL_MARKS_OF_PARRENT");
    });

    modelBuilder.Entity<SubjectOfAccountDetails>(entity =>
    {
      entity.HasKey(e => e.SubjectOfAccountsDetailsId);

      entity.Property(e => e.AccountName).HasMaxLength(250);
      entity.Property(e => e.AccountNo).HasMaxLength(250);
    });

    modelBuilder.Entity<SubjectOfAccounts>(entity =>
    {
      entity.HasKey(e => e.SubjectOfAccountId);

      entity.Property(e => e.MaximumLoanRate).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SubjectOfAccountName).HasMaxLength(500);
    });

    modelBuilder.Entity<SubjectOfAccountsHead>(entity =>
    {
      entity.HasKey(e => e.SubjectOfAccountHeadId);
    });

    modelBuilder.Entity<SubsidaryAccountMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SubLedgerAccountCode)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.SubsidaryAccountMappingId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<SugarClient>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Id)
              .HasMaxLength(108)
              .HasColumnName("id");
      entity.Property(e => e.Name)
              .HasMaxLength(150)
              .HasColumnName("name");
    });

    modelBuilder.Entity<SupportTransactionLog>(entity =>
    {
      entity.HasKey(e => e.TransactionLogId);

      entity.HasIndex(e => new { e.TransactionLogId, e.TransactionDate, e.TransactionTypeId }, "NonClusteredIndex_20210202_140927");

      entity.Property(e => e.Remarks)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.Request)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.Response)
              .HasMaxLength(2000)
              .IsUnicode(false);
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionType)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<SupportTransactionLog17102023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("SupportTransactionLog_17_10_2023");

      entity.Property(e => e.Remarks)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.Request)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.Response)
              .HasMaxLength(2000)
              .IsUnicode(false);
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionLogId).ValueGeneratedOnAdd();
      entity.Property(e => e.TransactionType)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<SupportTransactionLog31102023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("SupportTransactionLog_31_10_2023");

      entity.Property(e => e.Remarks)
              .HasMaxLength(1000)
              .IsUnicode(false);
      entity.Property(e => e.Request)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.Response)
              .HasMaxLength(2000)
              .IsUnicode(false);
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionLogId).ValueGeneratedOnAdd();
      entity.Property(e => e.TransactionType)
              .HasMaxLength(100)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Survey>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.HitCount).HasDefaultValue(0);
      entity.Property(e => e.IsAnnonymous).HasComment("0=false,1=True");
      entity.Property(e => e.IsPublished).HasComment("0=Draft/No, 1=Published/Yes, 2 = Cancel");
      entity.Property(e => e.PublishedDate).HasColumnType("datetime");
      entity.Property(e => e.SurveyId).ValueGeneratedOnAdd();
      entity.Property(e => e.SurveyTitle).HasMaxLength(50);
    });

    modelBuilder.Entity<SurveyAnswer>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.SortOrder).HasDefaultValue(0);
      entity.Property(e => e.SqanswerId).HasColumnName("SQAnswerId");
      entity.Property(e => e.SurveyAnswerId).ValueGeneratedOnAdd();
      entity.Property(e => e.SurveyAnswerStatus)
              .HasDefaultValue(-1)
              .HasComment("-1=Defauls/Null,0=Save As Draft, 1=Publish");
    });

    modelBuilder.Entity<SurveyCategory>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SurveyCategoryCode).HasMaxLength(50);
      entity.Property(e => e.SurveyCategoryDescription).HasMaxLength(50);
      entity.Property(e => e.SurveyCategoryId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<SurveyDetails>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SurveyDetailsId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<SurveyEmployee>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.SurveyEmployeeId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<SurveyQuestion>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AnswerType).HasComment("1=Single,2=Multiple");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.SurveyQuestionId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<SuspensionDetails>(entity =>
    {
      entity.HasKey(e => e.SuspensionId);
    });

    modelBuilder.Entity<SystemSettings>(entity =>
    {
      entity.HasKey(e => e.SettingsId);

      entity.Property(e => e.CasualWorkerAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.CustomStatusForNoOutPunch).HasMaxLength(250);
      entity.Property(e => e.IsOtcalculateOnHolidayWekend).HasColumnName("IsOTCalculateOnHolidayWekend");
      entity.Property(e => e.IsWebLoginEnable)
              .HasDefaultValue(0)
              .HasComment("0=Disable,1=Enable");
      entity.Property(e => e.Language).HasMaxLength(50);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.OdbcClientList).HasComment("0=Native SQL, 1=ODBC");
      entity.Property(e => e.PassResetBy).HasComment("1=SysAdmin, 2=User");
      entity.Property(e => e.PassType).HasComment("1=Alphanumeric, 2=Alphabetic, 3=Numeric");
      entity.Property(e => e.ResetPass).HasMaxLength(100);
      entity.Property(e => e.Theme).HasMaxLength(50);
    });

    modelBuilder.Entity<TaskType>(entity =>
    {
      entity.ToTable("TASK_TYPE");

      entity.Property(e => e.TaskTypeId).HasColumnName("TASK_TYPE_ID");
      entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
      entity.Property(e => e.IsMatricsActive).HasColumnName("IS_MATRICS_ACTIVE");
      entity.Property(e => e.IsRevisionActive).HasColumnName("IS_REVISION_ACTIVE");
      entity.Property(e => e.TaskTypeName)
              .HasMaxLength(50)
              .HasColumnName("TASK_TYPE_NAME");
    });

    modelBuilder.Entity<TaxBfbilMaySal22072023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Tax_BFBIL_MAY_SAL_22_07_2023");

      entity.Property(e => e.EmployeeId)
              .HasMaxLength(50)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<TaxInvestmentLetterReferenceNo>(entity =>
    {
      entity.HasNoKey();
    });

    modelBuilder.Entity<TaxSlabSettings>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.SlabId).ValueGeneratedOnAdd();
      entity.Property(e => e.SlabTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<TaxYear>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TaxYearId).ValueGeneratedOnAdd();
      entity.Property(e => e.TaxYearName).HasMaxLength(50);
    });

    modelBuilder.Entity<TempActualBasic>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_ActualBasic");

      entity.Property(e => e.EmpId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<TempApplicantInformation>(entity =>
    {
      entity.HasKey(e => e.TempApplicantId);

      entity.ToTable("Temp_ApplicantInformation");

      entity.Property(e => e.ApplicantAddress).HasMaxLength(1000);
      entity.Property(e => e.ApplicantName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.ApplicantPhotoPath).HasMaxLength(100);
      entity.Property(e => e.ApplicantPresentAddress).HasMaxLength(1000);
      entity.Property(e => e.ApplicantSource).HasMaxLength(200);
      entity.Property(e => e.ApplicantSurname).HasMaxLength(100);
      entity.Property(e => e.AttachmentCv)
              .HasMaxLength(500)
              .HasColumnName("AttachmentCV");
      entity.Property(e => e.CurrentCompany).HasMaxLength(400);
      entity.Property(e => e.CurrentDesignation).HasMaxLength(200);
      entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
      entity.Property(e => e.EducationalQualification).HasMaxLength(400);
      entity.Property(e => e.ExpectedSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExperienceYear).HasMaxLength(50);
      entity.Property(e => e.FatherName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.Gender).HasMaxLength(100);
      entity.Property(e => e.Institute).HasMaxLength(300);
      entity.Property(e => e.MobileNumber)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.MotherName)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.NationalId)
              .HasMaxLength(100)
              .IsUnicode(false)
              .HasColumnName("NationalID");
      entity.Property(e => e.PersonalEmail)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.PresentCompanyExperienceYear)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.PresentSalary).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ProfessionalQualification).HasMaxLength(300);
      entity.Property(e => e.Qualification).HasMaxLength(300);
      entity.Property(e => e.Reference1).HasMaxLength(200);
      entity.Property(e => e.Reference2).HasMaxLength(200);
      entity.Property(e => e.ReferenceType).HasMaxLength(100);
      entity.Property(e => e.Result).HasMaxLength(100);
      entity.Property(e => e.TotalExperienceYear).HasMaxLength(50);
    });

    modelBuilder.Entity<TempApplicantInterviewMarks>(entity =>
    {
      entity.HasKey(e => e.AppInterviewMarksUploadId);

      entity.ToTable("Temp_ApplicantInterviewMarks");

      entity.Property(e => e.InterviewDate).HasColumnType("datetime");
      entity.Property(e => e.InterviewMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Name).HasMaxLength(200);
    });

    modelBuilder.Entity<TempApplicantWrittenMarks>(entity =>
    {
      entity.HasKey(e => e.ApplicantMarksUploadId);

      entity.ToTable("Temp_ApplicantWrittenMarks");

      entity.Property(e => e.Name).HasMaxLength(100);
      entity.Property(e => e.WrittenDate).HasColumnType("datetime");
      entity.Property(e => e.WrittenMarks).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<TempAppraisalData>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DepartmentName).HasMaxLength(150);
      entity.Property(e => e.Description).HasMaxLength(2000);
      entity.Property(e => e.DesignationName).HasMaxLength(150);
    });

    modelBuilder.Entity<TempApproverRecommender>(entity =>
    {
      entity.HasKey(e => e.ApproverRecommenderId);

      entity.Property(e => e.Approver1)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Approver2)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Approver3)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Approver4)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Approver5)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Approver6)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Approver7)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Approver8)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.EmpId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Hr1)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("HR1");
      entity.Property(e => e.Hr2)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("HR2");
      entity.Property(e => e.Hr3)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("HR3");
      entity.Property(e => e.Hr4)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("HR4");
      entity.Property(e => e.Hr5)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("HR5");
      entity.Property(e => e.ParallelApp1)
              .HasMaxLength(50)
              .HasColumnName("Parallel_App1");
      entity.Property(e => e.ParallelApp2)
              .HasMaxLength(50)
              .HasColumnName("Parallel_App2");
      entity.Property(e => e.ParallelRec1)
              .HasMaxLength(50)
              .HasColumnName("Parallel_Rec1");
      entity.Property(e => e.ParallelRec2)
              .HasMaxLength(50)
              .HasColumnName("Parallel_Rec2");
      entity.Property(e => e.Recommender1)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Recommender2)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Recommender3)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TempAttendanceMonthEnd>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<TempEmployee>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_Employee");

      entity.Property(e => e.AdditionalInfo).HasMaxLength(50);
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.Birthidentification)
              .HasMaxLength(100)
              .HasColumnName("BIRTHIDENTIFICATION");
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.District).HasMaxLength(50);
      entity.Property(e => e.FatherName).HasMaxLength(500);
      entity.Property(e => e.FullName).HasMaxLength(500);
      entity.Property(e => e.Gender).HasMaxLength(50);
      entity.Property(e => e.Height)
              .HasMaxLength(50)
              .HasColumnName("HEIGHT");
      entity.Property(e => e.Hobby)
              .HasMaxLength(2000)
              .HasColumnName("HOBBY");
      entity.Property(e => e.HomePhone).HasMaxLength(50);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Identificationmark)
              .HasMaxLength(1000)
              .HasColumnName("IDENTIFICATIONMARK");
      entity.Property(e => e.Investmentamount).HasColumnName("INVESTMENTAMOUNT");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.Meritialstatus)
              .HasMaxLength(50)
              .HasColumnName("MERITIALSTATUS");
      entity.Property(e => e.MobileNo).HasMaxLength(500);
      entity.Property(e => e.MobileNo1)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.MotherName).HasMaxLength(500);
      entity.Property(e => e.NationalId)
              .HasMaxLength(250)
              .HasColumnName("NationalID");
      entity.Property(e => e.Nationality).HasMaxLength(50);
      entity.Property(e => e.OriginalBirthDay).HasMaxLength(50);
      entity.Property(e => e.PassportNo).HasMaxLength(250);
      entity.Property(e => e.Passportexpiredate).HasColumnName("PASSPORTEXPIREDATE");
      entity.Property(e => e.Passportissuedate).HasColumnName("PASSPORTISSUEDATE");
      entity.Property(e => e.PermanentAddressDistrict).HasMaxLength(50);
      entity.Property(e => e.PermanentAddressThana).HasMaxLength(50);
      entity.Property(e => e.PermanentPostCode).HasMaxLength(50);
      entity.Property(e => e.PersonalEmail).HasMaxLength(250);
      entity.Property(e => e.PlaceofBirth).HasMaxLength(50);
      entity.Property(e => e.Placeofpassportissue)
              .HasMaxLength(50)
              .HasColumnName("PLACEOFPASSPORTISSUE");
      entity.Property(e => e.PresentPostCode).HasMaxLength(50);
      entity.Property(e => e.Profilepicture)
              .HasMaxLength(2000)
              .HasColumnName("PROFILEPICTURE");
      entity.Property(e => e.Refempid)
              .HasMaxLength(50)
              .HasColumnName("REFEMPID");
      entity.Property(e => e.ReligionId).HasMaxLength(50);
      entity.Property(e => e.ShortName).HasMaxLength(50);
      entity.Property(e => e.Signature)
              .HasMaxLength(2000)
              .HasColumnName("SIGNATURE");
      entity.Property(e => e.SpouseName).HasMaxLength(500);
      entity.Property(e => e.Taxexamption).HasColumnName("TAXEXAMPTION");
      entity.Property(e => e.Thana).HasMaxLength(50);
      entity.Property(e => e.Weight)
              .HasMaxLength(50)
              .HasColumnName("WEIGHT");
    });

    modelBuilder.Entity<TempEmployeeLeaveUpload>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("TempEmployeeLeaveUpload$");

      entity.Property(e => e.EmployeeId)
              .HasMaxLength(250)
              .IsUnicode(false)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.F7).HasMaxLength(255);
      entity.Property(e => e.F8).HasMaxLength(255);
      entity.Property(e => e.LeaveEnjoyed)
              .HasMaxLength(255)
              .HasColumnName(" LeaveEnjoyed ");
      entity.Property(e => e.LeaveType)
              .HasMaxLength(255)
              .HasColumnName(" LeaveType ");
      entity.Property(e => e.NameOmit)
              .HasMaxLength(255)
              .HasColumnName("Name  (Omit) ");
    });

    modelBuilder.Entity<TempEmployeeWiseOvertime>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_EmployeeWiseOvertime");

      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeId).HasMaxLength(150);
      entity.Property(e => e.Otamount)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("OTAmount");
    });

    modelBuilder.Entity<TempEmployment>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_Employment");

      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid)
              .HasMaxLength(50)
              .HasColumnName("BRANCHID");
      entity.Property(e => e.CompanyId).HasMaxLength(50);
      entity.Property(e => e.ContactAddress)
              .HasMaxLength(300)
              .IsUnicode(false);
      entity.Property(e => e.DepartmentId).HasMaxLength(50);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.DivisionId).HasMaxLength(50);
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.EmployeeType).HasMaxLength(50);
      entity.Property(e => e.Experience)
              .HasMaxLength(2000)
              .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FacilityId).HasMaxLength(50);
      entity.Property(e => e.FuncId)
              .HasMaxLength(50)
              .HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
              .HasMaxLength(100)
              .HasColumnName("GPFNO");
      entity.Property(e => e.GradeId).HasMaxLength(50);
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.JobEndTypeId).HasMaxLength(50);
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.PostingType).HasMaxLength(50);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SalaryLocation).HasMaxLength(50);
      entity.Property(e => e.SectionId).HasMaxLength(50);
      entity.Property(e => e.Shiftid)
              .HasMaxLength(50)
              .HasColumnName("SHIFTID");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<TempExcelData>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CompanyName)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Department)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Designation)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.DesignationInHrToday)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("Designation In HR TODAY");
      entity.Property(e => e.EmailAddress)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("Email Address");
      entity.Property(e => e.Location)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Mobile)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Module)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Name)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.NameInHrToday)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("Name In HR TODAY");
      entity.Property(e => e.PfId)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("PF/ID");
      entity.Property(e => e.Plant)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Role)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Sn)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("SN");
    });

    modelBuilder.Entity<TempFieldForce>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_FieldForce");

      entity.Property(e => e.Dsmcode)
              .HasMaxLength(50)
              .HasColumnName("DSMCode");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Psocode)
              .HasMaxLength(100)
              .HasColumnName("PSOCode");
      entity.Property(e => e.Psolocation)
              .HasMaxLength(100)
              .HasColumnName("PSOLocation");
      entity.Property(e => e.RegionManagerId).HasMaxLength(200);
      entity.Property(e => e.RegionName).HasMaxLength(200);
      entity.Property(e => e.Rsmcode)
              .HasMaxLength(100)
              .HasColumnName("RSMCode");
    });

    modelBuilder.Entity<TempFuncDesign29012023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("tempFuncDesign_29_01_2023");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.FunctionalDesignation).HasMaxLength(250);
    });

    modelBuilder.Entity<TempGroupPermissionHrms>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_GroupPermission_HRMS");

      entity.Property(e => e.Groupid).HasColumnName("GROUPID");
      entity.Property(e => e.Parentpermission).HasColumnName("PARENTPERMISSION");
      entity.Property(e => e.PermissionId).ValueGeneratedOnAdd();
      entity.Property(e => e.Permissiontablename)
              .HasMaxLength(50)
              .HasColumnName("PERMISSIONTABLENAME");
      entity.Property(e => e.Referenceid).HasColumnName("REFERENCEID");
    });

    modelBuilder.Entity<TempLeaveUpload30052022>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_LeaveUpload_30_05_2022");

      entity.Property(e => e.EmpId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
      entity.Property(e => e.LeaveDate)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.LeaveTypeCode)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TempLeaveWithoutPay>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.LeaveWithoutPay).HasColumnType("decimal(18, 1)");
    });

    modelBuilder.Entity<TempMenuHrms>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_Menu_HRMS");

      entity.Property(e => e.MenuId)
              .ValueGeneratedOnAdd()
              .HasColumnName("MenuID");
      entity.Property(e => e.MenuName).HasMaxLength(50);
      entity.Property(e => e.MenuPath).HasMaxLength(200);
      entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
      entity.Property(e => e.Sororder).HasColumnName("SORORDER");
      entity.Property(e => e.Todo).HasColumnName("TODO");
    });

    modelBuilder.Entity<TempMissingConfimDate29012023>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("tempMissingConfimDate_29_01_2023");

      entity.Property(e => e.ConfirmationDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TempMultiApprover>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Temp_MultiApprover");

      entity.Property(e => e.ApproverEmpId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.RecommenderEmpId)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TempOverTime>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.OtfromDate)
              .HasColumnType("datetime")
              .HasColumnName("OTFromDate");
      entity.Property(e => e.OttoDate)
              .HasColumnType("datetime")
              .HasColumnName("OTToDate");
      entity.Property(e => e.OverTimeHour).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<TempOverTimeCtcAdjustment>(entity =>
    {
      entity.HasKey(e => e.OverTimeAdjustmentId);

      entity.Property(e => e.CtcAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<TempPfincrementSicl29072024>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("TempPFIncrementSICL_29_07_2024");

      entity.Property(e => e.EmployeeId)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.Pf)
              .HasColumnType("decimal(18, 0)")
              .HasColumnName("PF");
    });

    modelBuilder.Entity<TempRecommendation>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.PerformanceReviewId, e.YearConfigId }, "NonClusteredIndex_20230114_112113");
    });

    modelBuilder.Entity<TempRosterUploadFinal>(entity =>
    {
      entity.HasKey(e => e.RosterTempId);

      entity.Property(e => e.D1).HasMaxLength(50);
      entity.Property(e => e.D10).HasMaxLength(50);
      entity.Property(e => e.D11).HasMaxLength(50);
      entity.Property(e => e.D12).HasMaxLength(50);
      entity.Property(e => e.D13).HasMaxLength(50);
      entity.Property(e => e.D14).HasMaxLength(50);
      entity.Property(e => e.D15).HasMaxLength(50);
      entity.Property(e => e.D16).HasMaxLength(50);
      entity.Property(e => e.D17).HasMaxLength(50);
      entity.Property(e => e.D18).HasMaxLength(50);
      entity.Property(e => e.D19).HasMaxLength(50);
      entity.Property(e => e.D2).HasMaxLength(50);
      entity.Property(e => e.D20).HasMaxLength(50);
      entity.Property(e => e.D21).HasMaxLength(50);
      entity.Property(e => e.D22).HasMaxLength(50);
      entity.Property(e => e.D23).HasMaxLength(50);
      entity.Property(e => e.D24).HasMaxLength(50);
      entity.Property(e => e.D25).HasMaxLength(50);
      entity.Property(e => e.D26).HasMaxLength(50);
      entity.Property(e => e.D27).HasMaxLength(50);
      entity.Property(e => e.D28).HasMaxLength(50);
      entity.Property(e => e.D29).HasMaxLength(50);
      entity.Property(e => e.D3).HasMaxLength(50);
      entity.Property(e => e.D30).HasMaxLength(50);
      entity.Property(e => e.D31).HasMaxLength(50);
      entity.Property(e => e.D4).HasMaxLength(50);
      entity.Property(e => e.D5).HasMaxLength(50);
      entity.Property(e => e.D6).HasMaxLength(50);
      entity.Property(e => e.D7).HasMaxLength(50);
      entity.Property(e => e.D8).HasMaxLength(50);
      entity.Property(e => e.D9).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
    });

    modelBuilder.Entity<Tempmessage>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("TEMPMESSAGE");

      entity.Property(e => e.Fromuserid).HasColumnName("FROMUSERID");
      entity.Property(e => e.Isarchive).HasColumnName("ISARCHIVE");
      entity.Property(e => e.Isread).HasColumnName("ISREAD");
      entity.Property(e => e.Messagedetails)
              .HasMaxLength(2000)
              .HasColumnName("MESSAGEDETAILS");
      entity.Property(e => e.Messagingdate)
              .HasColumnType("datetime")
              .HasColumnName("MESSAGINGDATE");
      entity.Property(e => e.Touserid).HasColumnName("TOUSERID");
    });

    modelBuilder.Entity<TempsalaryNov>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("TEMPSALARY_NOV");

      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.GeneratedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MismatchedAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.OriginalAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
    });

    modelBuilder.Entity<Thana>(entity =>
    {
      entity.Property(e => e.ThanaCode).HasMaxLength(50);
      entity.Property(e => e.ThanaName).HasMaxLength(100);
      entity.Property(e => e.ThanaNameBn)
              .HasMaxLength(100)
              .HasColumnName("ThanaName_bn");
    });

    modelBuilder.Entity<Timesheet>(entity =>
    {
      entity.ToTable("TIMESHEET");

      entity.Property(e => e.Timesheetid).HasColumnName("TIMESHEETID");
      entity.Property(e => e.ApproveDate)
              .HasColumnType("datetime")
              .HasColumnName("APPROVE_DATE");
      entity.Property(e => e.ApproveRhRrecordid).HasColumnName("APPROVE_RH_RRECORDID");
      entity.Property(e => e.BillStatus).HasColumnName("BILL_STATUS");
      entity.Property(e => e.BillableLogHour).HasColumnName("BILLABLE_LOG_HOUR");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Isapprove).HasColumnName("ISAPPROVE");
      entity.Property(e => e.LogEntryDate)
              .HasColumnType("datetime")
              .HasColumnName("LOG_ENTRY_DATE");
      entity.Property(e => e.NoBillableLogHour).HasColumnName("NO_BILLABLE_LOG_HOUR");
      entity.Property(e => e.Projectid).HasColumnName("PROJECTID");
      entity.Property(e => e.Taskid).HasColumnName("TASKID");
      entity.Property(e => e.WorkedLogHour).HasColumnName("WORKED_LOG_HOUR");
      entity.Property(e => e.WorkingLogDate)
              .HasColumnType("datetime")
              .HasColumnName("WORKING_LOG_DATE");
    });

    modelBuilder.Entity<TmpBusUpload>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Company)
              .HasMaxLength(350)
              .IsUnicode(false);
      entity.Property(e => e.Department)
              .HasMaxLength(350)
              .IsUnicode(false);
      entity.Property(e => e.Division)
              .HasMaxLength(350)
              .IsUnicode(false);
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(350)
              .IsUnicode(false)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.EmployeeName)
              .HasMaxLength(350)
              .IsUnicode(false);
      entity.Property(e => e.Route)
              .HasMaxLength(350)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TmpTaxUpload>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Emp)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Tax)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("tax");
    });

    modelBuilder.Entity<ToDelete>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.BloodGroup).HasMaxLength(255);
      entity.Property(e => e.DateofBirth).HasColumnType("datetime");
      entity.Property(e => e.DateofMarriage).HasColumnType("datetime");
      entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
      entity.Property(e => e.FatherName).HasMaxLength(255);
      entity.Property(e => e.FullName).HasMaxLength(255);
      entity.Property(e => e.Gender).HasMaxLength(255);
      entity.Property(e => e.Height)
              .HasMaxLength(255)
              .HasColumnName("HEIGHT");
      entity.Property(e => e.Hobby)
              .HasMaxLength(255)
              .HasColumnName("HOBBY");
      entity.Property(e => e.HomePhone).HasMaxLength(255);
      entity.Property(e => e.Identificationmark)
              .HasMaxLength(255)
              .HasColumnName("IDENTIFICATIONMARK");
      entity.Property(e => e.IsAutistic).HasMaxLength(255);
      entity.Property(e => e.Meritialstatus)
              .HasMaxLength(255)
              .HasColumnName("MERITIALSTATUS");
      entity.Property(e => e.MobileNo).HasMaxLength(255);
      entity.Property(e => e.MotherName).HasMaxLength(255);
      entity.Property(e => e.NationalId)
              .HasMaxLength(255)
              .HasColumnName("NationalID");
      entity.Property(e => e.Nationality).HasMaxLength(255);
      entity.Property(e => e.PassportNo).HasMaxLength(255);
      entity.Property(e => e.Passportexpiredate)
              .HasMaxLength(255)
              .HasColumnName("PASSPORTEXPIREDATE");
      entity.Property(e => e.Passportissuedate)
              .HasMaxLength(255)
              .HasColumnName("PASSPORTISSUEDATE");
      entity.Property(e => e.PermanentAddress).HasMaxLength(255);
      entity.Property(e => e.Placeofpassportissue)
              .HasMaxLength(255)
              .HasColumnName("PLACEOFPASSPORTISSUE");
      entity.Property(e => e.PresentAddress).HasMaxLength(255);
      entity.Property(e => e.Profilepicture)
              .HasMaxLength(255)
              .HasColumnName("PROFILEPICTURE");
      entity.Property(e => e.Religion).HasMaxLength(255);
      entity.Property(e => e.ShortName).HasMaxLength(255);
      entity.Property(e => e.Signature)
              .HasMaxLength(255)
              .HasColumnName("SIGNATURE");
      entity.Property(e => e.SpouseName).HasMaxLength(255);
      entity.Property(e => e.Weight)
              .HasMaxLength(255)
              .HasColumnName("WEIGHT");
    });

    modelBuilder.Entity<TrConductorsAndPlanningMap>(entity =>
    {
      entity.HasKey(e => e.ConductorsAndPlanningMapId).HasName("PK_TrainingConductors");

      entity.ToTable("Tr_ConductorsAndPlanningMap");
    });

    modelBuilder.Entity<TrFinancerAndPlanningMap>(entity =>
    {
      entity.HasKey(e => e.FinancerAndPlanningMapId);

      entity.ToTable("Tr_FinancerAndPlanningMap");

      entity.Property(e => e.Percentage).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<TrOrganizerAndPlanningMap>(entity =>
    {
      entity.HasKey(e => e.OrganizerAndPlanningMapId);

      entity.ToTable("Tr_OrganizerAndPlanningMap");
    });

    modelBuilder.Entity<TrainingBooking>(entity =>
    {
      entity.Property(e => e.AppliedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.InvitedDate).HasColumnType("datetime");
      entity.Property(e => e.RecomendedDate).HasColumnType("datetime");
      entity.Property(e => e.RejectedDate).HasColumnType("datetime");
      entity.Property(e => e.StatusName).HasMaxLength(50);
    });

    modelBuilder.Entity<TrainingCertificateUpload>(entity =>
    {
      entity.HasKey(e => e.TrainingCertificateId);

      entity.Property(e => e.EmployeeName).HasMaxLength(250);
      entity.Property(e => e.UploadFilePath).HasMaxLength(500);
    });

    modelBuilder.Entity<TrainingCost>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.CostAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.ExpenditureHead).HasMaxLength(100);
      entity.Property(e => e.TrainingCostId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<TrainingForcast>(entity =>
    {
      entity.Property(e => e.CourseOutline).HasMaxLength(500);
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.Duration).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.LearnObjective).HasColumnType("text");
      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.StandardCost).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<TrainingHistory>(entity =>
    {
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
    });

    modelBuilder.Entity<TrainingHistoryTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
      entity.Property(e => e.TrainingHistoryTempId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<TrainingInfo>(entity =>
    {
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.CourseOutline).HasMaxLength(500);
      entity.Property(e => e.Duration).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.LearnObjective).HasColumnType("text");
      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.StandardCost).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TrainingCode).HasMaxLength(150);
      entity.Property(e => e.TrainingName).HasMaxLength(1000);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TrainingMarks>(entity =>
    {
      entity.HasKey(e => e.TrainingMarkId).HasName("PK_TraingMarks");
    });

    modelBuilder.Entity<TrainingMarksDetails>(entity =>
    {
      entity.Property(e => e.Mark).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<TrainingMarksTemp>(entity =>
    {
      entity.HasKey(e => e.TrainingMarksId);

      entity.ToTable("TrainingMarks_Temp");

      entity.Property(e => e.EmployeeId).HasMaxLength(250);
      entity.Property(e => e.Marks).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<TrainingNameForForcastInfo>(entity =>
    {
      entity.HasKey(e => e.TrainingNameIdForForcast);

      entity.Property(e => e.TrainingNameForForcast).HasMaxLength(250);
    });

    modelBuilder.Entity<TrainingParticipantAttendance>(entity =>
    {
      entity.HasKey(e => e.ParticipantAttendanceId);

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.IsPresent).HasMaxLength(50);
    });

    modelBuilder.Entity<TrainingPlanning>(entity =>
    {
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.Duration).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TrainingRequisitParticipant>(entity =>
    {
      entity.Property(e => e.SelectionStatus).HasMaxLength(50);
    });

    modelBuilder.Entity<TrainingRequisitionForm>(entity =>
    {
      entity.HasKey(e => e.TrainingRequisitionId);

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.Duration).HasColumnType("numeric(18, 2)");
      entity.Property(e => e.RecommendedDate).HasColumnType("datetime");
      entity.Property(e => e.RequestedDate).HasColumnType("datetime");
      entity.Property(e => e.StateChangedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TrainingScheduleEvents>(entity =>
    {
      entity.HasKey(e => e.TrainingScheduleCalendarId).HasName("PK_TrainingScheduleCalendar");

      entity.Property(e => e.Description).HasMaxLength(250);
      entity.Property(e => e.EndTimezone).HasMaxLength(50);
      entity.Property(e => e.RecurrenceException).HasMaxLength(150);
      entity.Property(e => e.RecurrenceRule).HasMaxLength(150);
      entity.Property(e => e.ScheduleCreateDate).HasColumnType("datetime");
      entity.Property(e => e.ScheduleEndTime).HasColumnType("datetime");
      entity.Property(e => e.ScheduleStartTime).HasColumnType("datetime");
      entity.Property(e => e.StartTimezone).HasMaxLength(50);
      entity.Property(e => e.Title).HasMaxLength(150);
    });

    modelBuilder.Entity<TrainingType>(entity =>
    {
      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.TrainingTypeCode).HasMaxLength(50);
      entity.Property(e => e.TrainingTypeName).HasMaxLength(150);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TraningSchedule>(entity =>
    {
      entity.HasKey(e => e.TrainingScheduleId);

      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
      entity.Property(e => e.Place).HasMaxLength(150);
      entity.Property(e => e.Sbu).HasColumnName("SBU");
      entity.Property(e => e.ScheduleStatus).HasMaxLength(50);
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TotalCourseFee).HasColumnType("decimal(18, 0)");
    });

    modelBuilder.Entity<TransectionType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.TransectionTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<TransferPromotion>(entity =>
    {
      entity.HasIndex(e => e.EffectiveDate, "TransferPromotion_3456");

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EvalutionDate).HasColumnType("datetime");
      entity.Property(e => e.NewEmpId).HasMaxLength(50);
      entity.Property(e => e.TransferLetterRaisedDate)
              .HasColumnType("datetime")
              .HasColumnName("transferLetterRaisedDate");

      entity.HasOne(d => d.PostingType).WithMany(p => p.TransferPromotion)
              .HasForeignKey(d => d.PostingTypeId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_TransferPromotion_PostingType");
    });

    modelBuilder.Entity<TransferPromotionDelete>(entity =>
    {
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.DeletedDate).HasColumnType("datetime");
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EvalutionDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<TransferPromotionTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Approver).HasMaxLength(50);
      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.Branch)
              .HasMaxLength(50)
              .HasColumnName("BRANCH");
      entity.Property(e => e.Company).HasMaxLength(50);
      entity.Property(e => e.ConfirmationDate).HasColumnType("datetime");
      entity.Property(e => e.Department).HasMaxLength(500);
      entity.Property(e => e.Designation)
              .HasMaxLength(500)
              .HasColumnName("DESIGNATION");
      entity.Property(e => e.Division).HasMaxLength(500);
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.EmployeeType).HasMaxLength(50);
      entity.Property(e => e.Facility).HasMaxLength(500);
      entity.Property(e => e.FunctionJobRole)
              .HasMaxLength(500)
              .HasColumnName("Function_JobRole");
      entity.Property(e => e.Grade).HasMaxLength(50);
      entity.Property(e => e.IsFieldForce).HasMaxLength(50);
      entity.Property(e => e.IsOteligible)
              .HasMaxLength(50)
              .HasColumnName("IsOTEligible");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.ReportTo).HasMaxLength(50);
      entity.Property(e => e.RepostingType).HasMaxLength(50);
      entity.Property(e => e.Section).HasMaxLength(500);
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TransferPromotionTempId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<TrusteeBoardSignatory>(entity =>
    {
      entity.HasKey(e => e.TrusteeBoardId);

      entity.Property(e => e.ContactPerson)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.SignatoryName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.SignatoryTitle)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<UnauthorizedAbsenceDetails>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.LetterRefNo).HasMaxLength(150);
      entity.Property(e => e.SignatoriesCompany).HasMaxLength(250);
      entity.Property(e => e.SignatoriesDesignation).HasMaxLength(250);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Users>(entity =>
    {
      entity.HasKey(e => e.UserId).HasName("PK_User");

      entity.HasIndex(e => e.EmployeeId, "IX_Users");

      entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeId).HasComment("EmployeeId As HrRecordId");
      entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.LoginId).HasMaxLength(50);
      entity.Property(e => e.Password).HasMaxLength(100);
      entity.Property(e => e.Theme)
              .HasMaxLength(100)
              .HasColumnName("THEME");
      entity.Property(e => e.UserName).HasMaxLength(500);
    });

    modelBuilder.Entity<Userstemp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("USERSTEMP");

      entity.Property(e => e.Companyid).HasColumnName("COMPANYID");
      entity.Property(e => e.Createddate)
              .HasColumnType("datetime")
              .HasColumnName("CREATEDDATE");
      entity.Property(e => e.Employeeid).HasColumnName("EMPLOYEEID");
      entity.Property(e => e.Failedloginno).HasColumnName("FAILEDLOGINNO");
      entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
      entity.Property(e => e.Isexpired).HasColumnName("ISEXPIRED");
      entity.Property(e => e.Lastlogindate)
              .HasColumnType("datetime")
              .HasColumnName("LASTLOGINDATE");
      entity.Property(e => e.Lastupdatedate)
              .HasColumnType("datetime")
              .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.Loginid)
              .HasMaxLength(50)
              .HasColumnName("LOGINID");
      entity.Property(e => e.Password)
              .HasMaxLength(100)
              .HasColumnName("PASSWORD");
      entity.Property(e => e.Theme)
              .HasMaxLength(100)
              .HasColumnName("THEME");
      entity.Property(e => e.Username)
              .HasMaxLength(50)
              .HasColumnName("USERNAME");
    });

    modelBuilder.Entity<Venue>(entity =>
    {
      entity.Property(e => e.VenueCode).HasMaxLength(50);
      entity.Property(e => e.VenueName).HasMaxLength(500);
    });

    modelBuilder.Entity<VerificationType>(entity =>
    {
      entity.Property(e => e.TypeName).HasMaxLength(100);
    });

    modelBuilder.Entity<VhAssignCarToManager>(entity =>
    {
      entity.HasKey(e => e.AssignCarToManagerId);

      entity.ToTable("VH_AssignCarToManager");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AssignCarToManagerDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.RegistrationNumber)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhAssignDriver>(entity =>
    {
      entity.HasKey(e => e.AssignId);

      entity.ToTable("VH_AssignDriver");

      entity.Property(e => e.AssignDateFrom)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.AssignDateTo)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhAssignVhToDivision>(entity =>
    {
      entity.HasKey(e => e.AssignToDivisionId);

      entity.ToTable("VH_AssignVhToDivision");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.AssignDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhAssignVhToLocation>(entity =>
    {
      entity.HasKey(e => e.AssignId);

      entity.ToTable("VH_AssignVhToLocation");
    });

    modelBuilder.Entity<VhColor>(entity =>
    {
      entity.HasKey(e => e.ColorId);

      entity.ToTable("VH_Color");

      entity.Property(e => e.ColorName)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhDivision>(entity =>
    {
      entity.HasKey(e => e.DivisionId);

      entity.ToTable("VH_Division");

      entity.Property(e => e.DivisionCode).HasMaxLength(500);
      entity.Property(e => e.DivisionName).HasMaxLength(500);
    });

    modelBuilder.Entity<VhDriver>(entity =>
    {
      entity.HasKey(e => e.DriverId);

      entity.ToTable("VH_Driver");

      entity.Property(e => e.AddedDate)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.ApprovedDate)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.BloodGroup)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.ContactNumber)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.DateofBirth)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.DriverName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.DriverPhoto)
              .HasMaxLength(350)
              .IsUnicode(false);
      entity.Property(e => e.EmergencyContactNumber)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.FatherName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.LicenseExpireDate)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.LicenseNo)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.MotherName)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.NationalId)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.PassportNo)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.PermanentAddress)
              .HasMaxLength(450)
              .IsUnicode(false);
      entity.Property(e => e.PresentAddress)
              .HasMaxLength(450)
              .IsUnicode(false);
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.UpdatedDate)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhEmpPickUpSpot>(entity =>
    {
      entity.HasKey(e => e.EmpPickupSpotId);

      entity.ToTable("VH_EmpPickUpSpot");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.PickUpSpotName).HasMaxLength(150);
      entity.Property(e => e.PickUpTime).HasPrecision(0);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhEmployeePickUpSpot>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Vh_EmployeePickUpSpot");

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.PickUpSpotName).HasMaxLength(250);
    });

    modelBuilder.Entity<VhEngineCapacity>(entity =>
    {
      entity.HasKey(e => e.EngineCapacityId);

      entity.ToTable("VH_EngineCapacity");

      entity.Property(e => e.Capacity)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhExpenseAndReimbursement>(entity =>
    {
      entity.HasKey(e => e.ExpnsAndReimbrsmntId).HasName("PK_ExpenseAndReimbursement");

      entity.ToTable("VH_ExpenseAndReimbursement");

      entity.Property(e => e.FilePath)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.ServicingDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhExpenseDetails>(entity =>
    {
      entity.HasKey(e => e.ExpenseDetailId);

      entity.ToTable("VH_ExpenseDetails");

      entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<VhExpenseMaster>(entity =>
    {
      entity.HasKey(e => e.ExpenseMasterId);

      entity.ToTable("VH_ExpenseMaster");

      entity.Property(e => e.ExpenseMasterId).ValueGeneratedNever();
      entity.Property(e => e.FilePath)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.ServicingDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhExpenseType>(entity =>
    {
      entity.HasKey(e => e.ExpenseTypeId);

      entity.ToTable("VH_ExpenseType");

      entity.Property(e => e.ExpenseTypeName).HasMaxLength(150);
      entity.Property(e => e.TypeCode)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhFuelType>(entity =>
    {
      entity.HasKey(e => e.FuelTypeId);

      entity.ToTable("VH_FuelType");

      entity.Property(e => e.FuelTypeName)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhLocationMaster>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("VH_LocationMaster");

      entity.Property(e => e.LocationName).HasMaxLength(150);
    });

    modelBuilder.Entity<VhManufacture>(entity =>
    {
      entity.HasKey(e => e.ManufactureId);

      entity.ToTable("VH_Manufacture");

      entity.Property(e => e.ManufactureName)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhMasterLocation>(entity =>
    {
      entity.HasKey(e => e.LocationId);

      entity.ToTable("VH_MasterLocation");

      entity.Property(e => e.LocationName).HasMaxLength(150);
    });

    modelBuilder.Entity<VhMilesLogBook>(entity =>
    {
      entity.HasKey(e => e.MilesLogBookId);

      entity.ToTable("VH_MilesLogBook");

      entity.Property(e => e.AddedDate).HasMaxLength(150);
      entity.Property(e => e.ApprovedDate).HasMaxLength(150);
      entity.Property(e => e.EndDate).HasColumnType("datetime");
      entity.Property(e => e.EndMiles).HasMaxLength(150);
      entity.Property(e => e.StartMiles).HasMaxLength(150);
      entity.Property(e => e.StartingDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasMaxLength(150);
    });

    modelBuilder.Entity<VhModel>(entity =>
    {
      entity.HasKey(e => e.ModelId);

      entity.ToTable("VH_Model");

      entity.Property(e => e.ModelNo)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.ModelYear)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhPassengerRoute>(entity =>
    {
      entity.HasKey(e => e.PassengerRouteId);

      entity.ToTable("VH_PassengerRoute");

      entity.Property(e => e.PassengerRouteName).HasMaxLength(250);
    });

    modelBuilder.Entity<VhProvider>(entity =>
    {
      entity.HasKey(e => e.ProviderId);

      entity.ToTable("VH_Provider");

      entity.Property(e => e.ProviderName)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhRegNoFirst>(entity =>
    {
      entity.HasKey(e => e.FirstRegNoId);

      entity.ToTable("VH_RegNoFirst");

      entity.Property(e => e.FirstRegNoName)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhRegNoMiddle>(entity =>
    {
      entity.HasKey(e => e.MiddleRegNoId);

      entity.ToTable("VH_RegNoMiddle");

      entity.Property(e => e.MiddleRegNoName)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhRegion>(entity =>
    {
      entity.HasKey(e => e.RegionId);

      entity.ToTable("VH_Region");

      entity.Property(e => e.RegionCode).HasMaxLength(50);
      entity.Property(e => e.RegionName).HasMaxLength(500);
    });

    modelBuilder.Entity<VhRenewal>(entity =>
    {
      entity.HasKey(e => e.RenewalId);

      entity.ToTable("VH_Renewal");

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.FitnessRenewalDate).HasColumnType("datetime");
      entity.Property(e => e.InsuranceNo).HasMaxLength(150);
      entity.Property(e => e.InsuranceRenewalDate).HasColumnType("datetime");
      entity.Property(e => e.RenewalDate).HasColumnType("datetime");
      entity.Property(e => e.RenewalType).HasMaxLength(150);
      entity.Property(e => e.ServiceMiles).HasMaxLength(150);
      entity.Property(e => e.TaxRenewalDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhRenewalTemp>(entity =>
    {
      entity.HasKey(e => e.RenewalId);

      entity.ToTable("VH_Renewal_Temp");

      entity.Property(e => e.FitnessRenewalDate).HasColumnType("datetime");
      entity.Property(e => e.InsuranceNo).HasMaxLength(150);
      entity.Property(e => e.InsuranceRenewalDate).HasColumnType("datetime");
      entity.Property(e => e.ServiceMiles).HasMaxLength(150);
      entity.Property(e => e.TaxRenewalDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhRenewalType>(entity =>
    {
      entity.HasKey(e => e.RenewalTypeId);

      entity.ToTable("Vh_RenewalType");

      entity.Property(e => e.RenewalType)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhRequisitEmployee>(entity =>
    {
      entity.HasKey(e => e.RequisitEmployeeId);

      entity.ToTable("VH_RequisitEmployee");
    });

    modelBuilder.Entity<VhRequisitionAllocation>(entity =>
    {
      entity.HasKey(e => e.AllocationId);

      entity.ToTable("VH_RequisitionAllocation");

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhRequisitionType>(entity =>
    {
      entity.HasKey(e => e.RequisitionTypeId);

      entity.ToTable("VH_RequisitionType");

      entity.Property(e => e.TypeName).HasMaxLength(100);
    });

    modelBuilder.Entity<VhRoute>(entity =>
    {
      entity.HasKey(e => e.RouteId);

      entity.ToTable("VH_Route");

      entity.Property(e => e.BackTime).HasColumnType("datetime");
      entity.Property(e => e.RouteName).HasMaxLength(150);
      entity.Property(e => e.StartTime).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhRouteAssignPassengerVehicleMap>(entity =>
    {
      entity.HasKey(e => e.RouteAssignPassengerId);

      entity.ToTable("VH_RouteAssignPassengerVehicleMap");
    });

    modelBuilder.Entity<VhRouteAssignPassengerVehicleMapDetails>(entity =>
    {
      entity.HasKey(e => e.RouteEmployeeId);

      entity.ToTable("VH_RouteAssignPassengerVehicleMapDetails");

      entity.HasIndex(e => new { e.HrRecordId, e.IsActive }, "IX_VH_RouteAssignPassengerVehicleMapDetails");
    });

    modelBuilder.Entity<VhRouteVehicleMap>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("VH_RouteVehicleMap");

      entity.Property(e => e.RouteAssignPassengerId).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<VhServiceCenter>(entity =>
    {
      entity.HasKey(e => e.ServiceCenterId);

      entity.ToTable("VH_ServiceCenter");

      entity.Property(e => e.ServiceCenterName).HasMaxLength(150);
    });

    modelBuilder.Entity<VhUsageType>(entity =>
    {
      entity.HasKey(e => e.UsageTypeId);

      entity.ToTable("VH_UsageType");

      entity.Property(e => e.UsageTypeName)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhVehicleOwnership>(entity =>
    {
      entity.HasKey(e => e.OwnershipId);

      entity.ToTable("VH_VehicleOwnership");

      entity.Property(e => e.OwnershipName)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VhVehicleRegistration>(entity =>
    {
      entity.HasKey(e => e.VehicleId);

      entity.ToTable("VH_VehicleRegistration");

      entity.HasIndex(e => e.RegistrationNumber, "IX_VH_VehicleRegistration").IsUnique();

      entity.Property(e => e.AddedDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.ChassisNo).HasMaxLength(250);
      entity.Property(e => e.Currency).HasMaxLength(150);
      entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
      entity.Property(e => e.EngineNo).HasMaxLength(250);
      entity.Property(e => e.NxtFitnessExpDate).HasColumnType("datetime");
      entity.Property(e => e.NxtInsuranceExpDate).HasColumnType("datetime");
      entity.Property(e => e.NxtTaxExpDate).HasColumnType("datetime");
      entity.Property(e => e.Price).HasMaxLength(150);
      entity.Property(e => e.RegistrationNumber).HasMaxLength(150);
      entity.Property(e => e.Registrationdate).HasColumnType("datetime");
      entity.Property(e => e.Remarks).HasMaxLength(150);
      entity.Property(e => e.SeatCapacity).HasMaxLength(150);
      entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
      entity.Property(e => e.WarrantyDate).HasColumnType("datetime");
      entity.Property(e => e.Weight).HasMaxLength(150);
      entity.Property(e => e.WeightUnit).HasMaxLength(150);
    });

    modelBuilder.Entity<VhVehicleRequisition>(entity =>
    {
      entity.HasKey(e => e.VehicleRequisitionId);

      entity.ToTable("VH_VehicleRequisition");

      entity.Property(e => e.ApplyDate).HasColumnType("datetime");
      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.CancelledDate).HasColumnType("datetime");
      entity.Property(e => e.Destination).HasMaxLength(150);
      entity.Property(e => e.Miles).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.MobileNo).HasMaxLength(50);
      entity.Property(e => e.PabxExt).HasMaxLength(5);
      entity.Property(e => e.PickupSpot).HasMaxLength(150);
      entity.Property(e => e.RejectedDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VhVehicleType>(entity =>
    {
      entity.HasKey(e => e.VehicleTypeId);

      entity.ToTable("VH_VehicleType");

      entity.Property(e => e.VehicleTypeName).HasMaxLength(150);
    });

    modelBuilder.Entity<VhVehileRouteAssignToPassengerMapDetails>(entity =>
    {
      entity.HasKey(e => e.RouteEmployeeId);

      entity.ToTable("VH_VehileRouteAssignToPassengerMapDetails");
    });

    modelBuilder.Entity<VigilanceDutyRoster>(entity =>
    {
      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.BreakupHour).HasPrecision(0);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.FromTime).HasPrecision(0);
      entity.Property(e => e.GraceTime).HasColumnType("datetime");
      entity.Property(e => e.Remarks)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.RewardDate).HasColumnType("datetime");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.ToDate).HasColumnType("datetime");
      entity.Property(e => e.ToTime).HasPrecision(0);
      entity.Property(e => e.Wfstate).HasColumnName("WFstate");
    });

    modelBuilder.Entity<VigilanceDutyTime>(entity =>
    {
      entity.Property(e => e.ActualLoginTime).HasColumnType("datetime");
      entity.Property(e => e.ActualLogoutTime).HasColumnType("datetime");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.GraceTime).HasPrecision(0);
      entity.Property(e => e.IsNightShift).HasDefaultValue(false);
      entity.Property(e => e.Remarks).HasColumnType("text");
      entity.Property(e => e.RewardType)
              .HasMaxLength(1)
              .IsUnicode(false);
      entity.Property(e => e.StartTime).HasPrecision(0);
      entity.Property(e => e.ToDate).HasColumnType("datetime");
      entity.Property(e => e.ToTime).HasPrecision(0);
      entity.Property(e => e.VigilanceDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<VigilanceRewardType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Id).ValueGeneratedOnAdd();
      entity.Property(e => e.Type)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<VigilanceRosterDetails>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Vigilanc__3214EC070F44B8A9");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.Wfstate).HasColumnName("WFState");
    });

    modelBuilder.Entity<VigilanceRosterDetailsTemp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("VigilanceRosterDetails_temp");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
      entity.Property(e => e.Wfstate).HasColumnName("WFState");
    });

    modelBuilder.Entity<VigilanceRosterPolicy>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<VoucharDetails>(entity =>
    {
      entity.Property(e => e.AccountHeadCode).HasMaxLength(50);
      entity.Property(e => e.CrAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.DrAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.LedgerReference).HasMaxLength(50);
      entity.Property(e => e.SubLedgerAccountCode).HasMaxLength(50);
      entity.Property(e => e.SubLedgerCode).HasMaxLength(50);
      entity.Property(e => e.SubLedgerName).HasMaxLength(250);
    });

    modelBuilder.Entity<VoucharMaster>(entity =>
    {
      entity.HasKey(e => e.VoucharId);

      entity.HasIndex(e => e.VoucharNo, "IX_VoucharMaster").IsUnique();

      entity.Property(e => e.ChequeNo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Detaills).HasColumnType("text");
      entity.Property(e => e.MakeDate).HasColumnType("datetime");
      entity.Property(e => e.Naration).HasColumnType("text");
      entity.Property(e => e.PostedDate).HasColumnType("datetime");
      entity.Property(e => e.Source)
              .HasMaxLength(100)
              .IsUnicode(false);
      entity.Property(e => e.TotalCreditAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.TotalDebitAmount).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.VoucharDate).HasColumnType("datetime");
      entity.Property(e => e.VoucharNo).HasMaxLength(50);
      entity.Property(e => e.VoucherSource).HasDefaultValue(0);
    });

    modelBuilder.Entity<VoucharType>(entity =>
    {
      entity.Property(e => e.VoucharTypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<WelfareFund>(entity =>
    {
      entity.Property(e => e.AmountCr).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.AmountDr).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<WelfareFundApplication>(entity =>
    {
      entity.HasKey(e => e.WelfareApplicationId);

      entity.Property(e => e.AppliedAmount).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<Wfaction>(entity =>
    {
      entity.ToTable("WFAction");

      entity.Property(e => e.WfactionId).HasColumnName("WFActionId");
      entity.Property(e => e.ActionName).HasMaxLength(50);
      entity.Property(e => e.EmailAlert).HasColumnName("EMAIL_ALERT");
      entity.Property(e => e.SmsAlert).HasColumnName("SMS_ALERT");
      entity.Property(e => e.WfstateId).HasColumnName("WFStateId");
    });

    modelBuilder.Entity<Wfstate>(entity =>
    {
      entity.ToTable("WFState");

      entity.Property(e => e.WfstateId).HasColumnName("WFStateId");
      entity.Property(e => e.Sequence).HasColumnName("sequence");
      entity.Property(e => e.StateName).HasMaxLength(50);
    });

    modelBuilder.Entity<WorkLog>(entity =>
    {
      entity.HasKey(e => e.LogId);

      entity.Property(e => e.ApproveDate).HasColumnType("datetime");
      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.LogDate).HasColumnType("datetime");
      entity.Property(e => e.MatriceId)
              .HasMaxLength(2000)
              .IsUnicode(false);
      entity.Property(e => e.RecomanderDate).HasColumnType("datetime");
      entity.Property(e => e.RevisionId)
              .HasMaxLength(2000)
              .IsUnicode(false);
      entity.Property(e => e.WorkHour).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<WrittenMarksTemp>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EducationMarks).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.JobVacancyId).HasColumnName("jobVacancyId");
      entity.Property(e => e.RollNumber).HasMaxLength(50);
      entity.Property(e => e.WrittenMarks).HasColumnType("decimal(18, 2)");
    });

    modelBuilder.Entity<XP>(entity =>
    {
      entity.HasKey(e => e.XPdId);

      entity.ToTable("xP");

      entity.Property(e => e.XPdId).HasColumnName("xPdId");
      entity.Property(e => e.XpId).HasColumnName("xpId");
      entity.Property(e => e.Xpg)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xpg");
      entity.Property(e => e.Xpp)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xpp");
      entity.Property(e => e.XppAmt)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xppAmt");
      entity.Property(e => e.Xtr)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xtr");
      entity.Property(e => e.Xtx)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xtx");
    });

    modelBuilder.Entity<Xbi>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("XBI");

      entity.Property(e => e.Ac)
              .HasMaxLength(250)
              .IsUnicode(false)
              .HasColumnName("AC");
      entity.Property(e => e.Bank)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.Branch)
              .HasMaxLength(250)
              .IsUnicode(false);
      entity.Property(e => e.EmpId)
              .HasMaxLength(250)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Xdelete>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.EmployeeDelId)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Xena>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("XEna");

      entity.Property(e => e.Email).HasMaxLength(250);
      entity.Property(e => e.Emp).HasMaxLength(50);
    });

    modelBuilder.Entity<Xpd>(entity =>
    {
      entity.HasKey(e => e.XpddId);

      entity.ToTable("xpd");

      entity.Property(e => e.XpddId).HasColumnName("xpddId");
      entity.Property(e => e.XcId).HasColumnName("xcId");
      entity.Property(e => e.Xcv)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xcv");
      entity.Property(e => e.XpId).HasColumnName("xpId");
    });

    modelBuilder.Entity<Xpdh>(entity =>
    {
      entity.HasKey(e => e.XpddhId);

      entity.ToTable("xpdh");

      entity.Property(e => e.XpddhId).HasColumnName("xpddhId");
      entity.Property(e => e.XcId)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xcId");
      entity.Property(e => e.Xcv)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xcv");
      entity.Property(e => e.XpId).HasColumnName("xpId");
    });

    modelBuilder.Entity<Xph>(entity =>
    {
      entity.HasKey(e => e.XpdhId);

      entity.ToTable("xph");

      entity.Property(e => e.XpdhId).HasColumnName("xpdhId");
      entity.Property(e => e.XpId).HasColumnName("xpId");
      entity.Property(e => e.Xpg)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xpg");
      entity.Property(e => e.Xpp)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xpp");
      entity.Property(e => e.XppAmt)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xppAmt");
      entity.Property(e => e.Xtr)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xtr");
      entity.Property(e => e.Xtx)
              .HasColumnType("decimal(18, 2)")
              .HasColumnName("xtx");
    });

    modelBuilder.Entity<XxtoDeleteEmp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("XXToDeleteEmp");

      entity.Property(e => e.ToDeleteEmpId)
              .HasMaxLength(10)
              .IsFixedLength();
    });

    modelBuilder.Entity<ZBcdlpf>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("zBCDLPF");

      entity.Property(e => e.Name)
              .HasMaxLength(500)
              .IsUnicode(false);
      entity.Property(e => e.Pf)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("PF");
      entity.Property(e => e.Sl)
              .HasMaxLength(500)
              .IsUnicode(false)
              .HasColumnName("sl");
      entity.Property(e => e.TheirCard)
              .HasMaxLength(500)
              .IsUnicode(false);
    });

    modelBuilder.Entity<ZEmployeeLeaveTemp>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("zEmployeeLeaveTemp");

      entity.Property(e => e.ClosingLeaveBalance)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.EmployeeId)
              .HasMaxLength(150)
              .IsUnicode(false)
              .HasColumnName("EmployeeID");
      entity.Property(e => e.ForceLeaveDeducted)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.LeaveEnjoyed)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.LeaveForward)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.LeaveType)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.OpeningLeaveBalance)
              .HasMaxLength(150)
              .IsUnicode(false);
    });

    modelBuilder.Entity<ZfieldForce>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ZFieldForce");

      entity.Property(e => e.Designation)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.Name)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.Pf)
              .HasMaxLength(150)
              .IsUnicode(false)
              .HasColumnName("PF");
    });

    modelBuilder.Entity<ZrosterDetailsBackup>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("ZRosterDetailsBackup");

      entity.Property(e => e.DateValue).HasColumnType("datetime");
    });

    // Configure the TokenBlacklist entity
    modelBuilder.Entity<TokenBlacklist>(entity =>
    {
      // Set the table name
      entity.ToTable("TokenBlacklist");

      // Define the primary key
      entity.HasKey(e => e.TokenId); // TokenId is the primary key

      // Configure the Token column
      entity.Property(e => e.Token)
            .IsRequired() // Ensure the token is not null
            .HasMaxLength(int.MaxValue); // NVARCHAR(MAX) maps to MaxLength(int.MaxValue)

      // Configure the ExpiryDate column
      entity.Property(e => e.ExpiryDate)
            .HasColumnType("datetime") // Use the appropriate database type
            .IsRequired(); // Ensure the expiry date is not null
    });

    modelBuilder.Entity<CrmapplicantCourseDetials>(entity =>
    {
      entity.HasNoKey().ToTable("CRMApplicantCourseDetials");

      entity.Property(e => e.ApplicantCourseDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.ApplicationFee).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CourseRemarks)
          .HasMaxLength(500)
          .IsUnicode(false);
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentReferenceNumber)
          .HasMaxLength(50)
          .IsUnicode(false);
    });

    modelBuilder.Entity<CRMInstituteType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.InstituteTypeId).ValueGeneratedOnAdd();
      entity.Property(e => e.InstituteTypeName)
          .HasMaxLength(50)
          .IsUnicode(false);
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
