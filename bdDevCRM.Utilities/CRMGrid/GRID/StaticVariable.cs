namespace bdDevCRM.Entities.CRMGrid.GRID
{
  public static class StaticVariable
  {
    public static class InvestmentStatus
    {
      public static int SaveAsDraft = 1;
      public static int Live = 2;
      public static int Close = 3;
    }

    public static class EmployeeStatus
    {
      public static int Active = 7;
      public static int Confirmation = 4;

    }

    public static class PfLoanNotification
    {
      public static int Apply = 501;
      public static int Recommand = 502;
      public static int Approved = 503;
      public static int Disbursed = 504;
      public static int Settlement = 505;
      public static int PaidLastEMI = 506;
      public static int SendForApproval = 507;
    }

    public static class Fund
    {
      public static int PFBalanceConfirmation = 601;
    }

    public enum LeaveNotificationEnum
    {
      Apply = 701,
      Recommand = 702,
      Approved = 703,
      Cancel = 704,
      Reject = 705,
      Confirmation = 706,
      AprovedByAdmin = 707,
      AutoApproveBySystem = 708,
      Delete = 709
    }

    public enum CoffNotificationEnum
    {
      Apply = 70111,
      Recommand = 70222,
      Approved = 70333,
      Cancel = 70444,
      Reject = 70555,
      Confirmation = 70666,
      AprovedByAdmin = 70777,
      AutoApproveBySystem = 70888,
      Delete = 70999
    }

    public enum AnnualIncrementNotificationEnum
    {

      Recommand = 901

    }

    public enum ConfirmationReviewState
    {

      Recommand = 2101,
      Approved = 2102,


    }

    public enum PrmotedEmployeeState
    {

      Recommand = 2001,
      Approved = 2002,


    }

    public static class Vehicle
    {
      public static int AssignToManager = 610;
      public static int AssignToPole = 611;
      public static int AssignRequisition = 612;
      public static int Others = 613;
      public static int Free = 614;

    }

    public static class VehicleRequisition
    {
      public static int Apply = 601;
      public static int Recommand = 602;
      public static int Approved = 603;
      public static int Allocated = 604;
      public static int Rejected = 605;
      public static int Canceled = 606;

    }

    public enum OSApplication
    {
      Apply = 401,
      Recommand = 402,
      Approved = 403,
      Canceled = 404,
      Rejected = 405
    }

    public enum AttendanceRequestStatus
    {
      Apply = 301,
      Recommand = 302,
      Approved = 303,
      Canceled = 304,
      Rejected = 305
    }

    public enum MovementLogStatus
    {
      Apply = 406,
      Recommand = 407,
      Approved = 408,
      Canceled = 409,
      Rejected = 410
    }

    public enum AttendanceMonthEndNotification
    {
      Approval = 420
    }

    public static class TrainingRequisition
    {
      public static int Apply = 1401;
      public static int Recommend = 1402;
      public static int Approved = 1403;
      public static int Cancelled = 1404;
      public static int Rejected = 1405;

    }

    public static class TrainingParticipant
    {
      public static int Requested = 160;
      public static int Recommended = 161;
      public static int Cancelled = 162;
      public static int Approved = 163;
      public static int Rejected = 164;
      public static int Deleted = 165;
      public static int Selected = 166;
      public static int Removed = 167;

    }

    public static class TrainingManagement
    {
      public static int SaveAsDraft = 140;
      public static int Publish = 141;
      public static int Cancel = 142;
      public static int Start = 143;
      public static int Complete = 144;

    }

    public static class TrainingBooking
    {
      public static int Invite = 150;
      public static int RejectInvitation = 151;
      public static int Apply = 152;
      public static int Recommend = 153;
      public static int Cancel = 154;
      public static int Approve = 155;
      public static int Reject = 156;
      public static int Nominated = 157;
      public static int Booked = 158;
      public static int Enrolled = 159;
      public static int AcceptInvitation = 160;
      public static int SelectedFromRequisition = 161;
      public static int Completed = 162;
      public static int Removed = 163;

    }
    public static class Resignation
    {
      public static int Apply = 1001;
      public static int Retained = 1002;
      public static int Accept = 1003;
      public static int Withdraw = 1004;
      public static int Exit = 1005;
      public static int Clearence = 1006;
      public static int Accepted = 1007;
      public static int NotAccepted = 1008;
      public static int ForwardToAudit = 1009;
      public static int ForwardToPayroll = 1010;
      public static int AcceptanceLetter = 1011;
    }
    public static class Grievance
    {
      public static int GrievanceSubmit = 2601;
      public static int GrievanceEnquiry = 2602;
      public static int GrievanceRelieve = 2603;
      public static int GrievanceTakeAction = 2604;
      public static int GrievanceRollback = 2605;
      public static int GrievanceHRSubmit = 2606;
      public static int GrievanceSendHeadDept = 2607;
      public static int GrievanceReview = 2608;
      public static int GrievanceConductEnquiry = 2609;
    }
    public static class InterviewStatus
    {
      public static int FirstInterviewStart = 1;
      public static int FirstInterviewEnd = 2;
      public static int SecondInterviewStart = 3;
      public static int SecondInterviewEnd = 4;
      public static int ThirdInterviewStart = 5;
      public static int ThirdInterviewEnd = 6;

    }
  }

  public enum VoucherTypeEnum
  {
    PaymentVoucher = 1,
    ReceivedVoucher = 2,
    JournalVoucher = 2,
  }

  public enum TransectionTypeEnum
  {
    BankPayment = 1,
    CashPayment = 2,
    BankReceive = 3,
    CashReceive = 4,
    Journal = 5,
  }

  public enum CustomAction
  {
    DeleteAll = -1,
    None = 0
  }

  public enum NotificationType
  {
    LeaveNotification = 1,
    VehicleNotification = 2,
    OSNotification = 3,
    AttendanceAdjustmentNotification = 4,
    MovementLogNotification = 5,
    ConfirmationNotification = 6,
    RecruitmentNotification = 7,
    TrainingNotification = 8,
    SurveyNotification = 9,
    BirthDayNotification = 10,
    MarriageDayNotification = 11,
    EidNotification = 12,
    DurgaPujaNotification = 13,
    BuddhaPurnimaNotification = 14,
    BanglaNoboBorshoNotification = 15,
    NewYearNotification = 16,
    ConfirmationReminderNotification = 17,
    RetirementNotification = 18,
    PendingLeaveApprovalNotification = 19,
    PlannedLeaveNotification = 20,
    LateLeaveApplication = 21,
    AbsentNotification = 22,
    LatePresentNotification = 23,
    ContactEndNotification = 24,
    IntrimReviewNotification = 25,
    InvestmentMaturityNotification = 26,
    AttendanceSummaryNotification = 27,
    EarlyExitNotification = 28,
    OutPunchMissingNotification = 29,
    DrivingLicenseExpiryNotification = 30,
    VehicleFitnessExpiryNotification = 31,
    VehicleInsuranceExpiryNotification = 32,
    VehicleTaxExpiryNotification = 33,
    PayrollNotification = 34,

    SalaryMakePaymentNotification = 79



  }

  public enum NotificationEmailContent
  {
    //BirthDayEmailContent = 1,
    //ConfirmationReminder1st = 27001,
    //ConfirmationReminder2nd = 27002
    //Email Title Format for Notification( Module+NotificationTypeId+Sequence)
    BirthDayWish = 271001,
    MarriageDayWish = 271101,
    EidFestivalWish = 271201,
    DurgaPujaWish = 271301,
    BuddhaPurnimaWish = 271401,
    BanglaNoboBorshoWish = 271501,
    NewYearWish = 271601,
    ConfirmationReminder1st = 271701,
    ConfirmationReminder2nd = 271702,
    RetirementReminder = 271801,
    PendingLeaveApprovalReminder = 271901,
    PlannedLeaveReminder = 272001,
    LateLeaveApplicationReminder = 272101,
    AbsentReminder = 272201,
    AbsentSummaryReminder = 272202,
    LatePresentReminder = 272301,
    LateSummaryReminder = 272302,
    ContactEndReminder = 272401,
    IntrimReviewReminder = 272501,
    InvestmentMaturityReminder = 272601,
    AttendanceSummaryReminder = 272701,
    EarlyExitReminder = 272801,
    EarlyExitSummaryReminder = 272802,
    OutPunchMissingReminder = 272901,
    DrivingLicenseExpiryReminder = 273001,
    VehicleFitnessExpiryReminder = 273101,
    VehicleInsuranceExpiryReminder = 273201,
    VehicleTaxExpiryReminder = 273301,
    PayrollApproveEmail = 273401,
    JobConfirmationEmailForRecommender = 213201,
    JobConfirmationEmailForApprover = 213202,
    JobConfirmationEmailForFirstHr = 213203,
    JobConfirmationEmailForCHRO = 213204,
    JobConfirmationEmailForEmployee = 213205,
    JobConfirmationRejection = 213206,
    JobConfirmationAfterFinalApprove = 213207,

    PMSYearConfigEmailNotification = 243401,
    PMSEmailNotificationForRecommender = 243501,
    PMSEmailNotificationForApprover = 243502,
    PMSEmailNotificationForHR = 243503,
    PMSEmailNotificationForApplicationRejection = 243504,
    PMSEmailNotificationForApplicationRollBack = 243505,
    PMSEmailNotificationForRecommenderToReview = 243506,
    PMSEmailNotificationForApproverToReview = 243507,
    PMSEmailNotificationForHRToReview = 243508,
    PMSEmailNotificationForCHROToReview = 243509,
    PMSEmailNotificationForVCToReview = 243510,
    PMSEmailNotificationForApplicationRollBackToReview = 243511,
    PMSEmailNotificationForApplicationRejectionToReview = 243512,
    PMSEmailNotificationForPMSLetter = 243513,

    PMSEmailNotificationForCHROToEvaluation = 243524,
    PMSEmailNotificationForVCToEvaluation = 243525,
    PMSEmailNotificationForRollbackEvaluation = 243526,
    PMSEmailNotificationForRejectEvaluation = 243527,

    PMSEvaluationOpenNotificationForDeptHead = 243528,


    // Recruitment
    RecruitmentManpowerPlanningApply = 153401,
    RecruitmentManpowerPlanningApprovalToApplicant = 153402,
    RecruitmentManpowerPlanningApprovalToChrm = 153403,
    RecruitmentManpowerPlanningReview = 153404,
    RecruitmentManpowerPlanningReject = 153405,
    RecruitmentManpowerPlanningForward = 153406,

    RecruitmentManpowerRequisitionApply = 153501,
    RecruitmentManpowerRequisitionApprove = 153502,
    RecruitmentManpowerRequisitionReview = 153503,
    RecruitmentManpowerRequisitionReject = 153504,


    JobVacancyApprovalNotificationToApprover = 153510,
    JobVacancyApprovalNotificationToInitiator = 153511,
    JobVacancyRejectedNotification = 1541055,
    JobVacancyApprovalNotificationToInitiatorForReview = 153512,
    JobVacancyPublishNotificationToLine = 153513,


    ApplicantEntryFormPrimaryshortlist = 154101,
    ApplicantEntryFormshortlist = 154102,
    ApplicantEntryFormHrApproval = 154103,
    ApplicantEntryFormChrmApproval = 154104,
    ApplicantEntryFormRecruiterMail = 154105,
    ApplicantEntryFormRejectMail = 154106,
    ApplicantEntryFormJobOfferRejectMail = 1541020,

    ApplicantOfferLetterMail = 154107,
    ApplicantAppointmentLetter = 154108,
    ApplicantJoiningLetter = 154131,

    ApplicantInvitationForAssesment = 154109,
    ApplicantInvitationForWrittenExam = 154110,


    SecondInterviewInvited = 1541056,

    ApplicantReInvitationForAssesment = 154111,
    ApplicantReInvitationForWrittenExam = 154112,

    ApplicantInvitationForInitialAssesment = 1541031,
    ApplicantInvitationForAcademicAssesment = 1541032,
    ApplicantRejectedFromAssesment = 1541010,
    ApplicantPipelined = 1541011,
    ApplicantInvitationForSecondInterview = 1541012,
    ApplicantInvitationForThirdInterview = 1541013,
    ReportingLineForDidnotJoin = 1541014,
    ReportingLineForJoined = 1541015,
    ApplicantSelectedFromAssesment = 1541016,
    EmailToItDepartment = 1541017,
    EmailToLineManagerOfferAccoknowledge = 1541018,
    EmailForIdCardRequisition = 1541019,
    ApplicantWrittenExamAdmitCard = 1541025,

    ApplicantInitialSelectedFromAssesment = 1541050,
    ApplicantInitialRejectedFromAssesment = 1541051,
    ApplicantInitialPipelinedFromAssesment = 1541052,


    EmailForClearenceReminder = 1051013,

    JobOfferApprovalFromCHRO = 1541053,
    JobOfferApprovalFromVC = 1541054,

    SmsForJoiningReminderToApplicant = 1541023,
    EmailToLineManagerAsStakeHolder = 1541024,

    SmsForSelectedInWrittenExam = 1541021,
    SmsForRejectedInWrittenExam = 1541022,
    CircularEmialForAll = 1541030,
    CvSortingNotificationEmail = 805,
    CvSortingEmailForHR = 806,
    InterviewReminderNotification = 1541057,


    FsCheckNotification = 105105,
    FsReviewNotification = 105106,
    FsApproverNotification = 105107,
    FsPaidNotification = 105108,
    FsRollBackNotification = 105109,
    FsFirstVerifierMail = 1051010,
    FsSecondVerifierMail = 1051011,
    FsFinanceNotificationMail = 1051012,
    //EmailForClearenceReminder = 1051013,
    FsPendingPaymentNotification = 105114,
    FsApprovedNotification = 105115,

    SalaryMakePaymentNotification = 105116,
  }

  public enum NotificationSourceEnum
  {
    Messaging = 1,
    LeaveApplication = 2,
    DayOff = 3,
    VehicleRequisition = 4,
    AttendanceAdjustment = 5,
    OutStation = 6,
    Movement = 7,
    BirthDayWish = 8,
    EidFestivalWish = 9,
    DurgaPujaWish = 10,
    BuddhaPurnimaWish = 11,
    NewYearWish = 12,
    BanglaNoboBorshoWish = 13,
    ConfirmationReminder = 14,
    HRReminder = 15,
    MarriageDayWish = 16,
    AbsentReminder = 17,
    LatePresentReminder = 18,
    AttendanceSummaryReminder = 19,
    EarlyExitReminder = 20,
    OutPuntchMissingReminder = 21,
    DrivingLicenseExpiryReminder = 22,
    VehicleFitnessExpiryReminder = 23,
    VehicleInsuranceExpiryReminder = 24,
    VehicleTaxExpiryReminder = 25,
    RetirementReminder = 26,
    ContactEndReminder = 27,
    PendingLeaveApprovalReminder = 28,
    Budgeting = 30,
    AbsentSummaryReminder = 31,
    LateSummaryReminder = 32,
    EarlyExitSummaryReminder = 33,
    PayrollNotification = 34,
    ConfirmationLetterReminder = 35,
    //Recruitment
    RecruitmentManpowerPlanning = 40,
    ApplicantEntryForm = 41,
    RecruitmentManpowerRequisitionNotification = 42,
    RecruitmentManpowerRequisition = 43,
    JobVacancyNotification = 49,
    CvSortingReminder = 50,
    InterviewerReminder = 51,
    CoffCertificate = 52,
    PerformanceNotification = 55
  }

  public enum NotificationSourceEnumForPMS
  {
    PMSYearConfigSetUp = 22,
    EvaluationOpenNotification = 23
  }


  public enum NotificationSourceForRecruitment
  {
    JobVacancyNotification = 24,
    RecruitmentJobOfferApproval = 25,

  }




  public enum EmpressClientIdentity
  {
    Azolution = 1,
    Rahimafroze = 2,
    MeghnaGroup = 3,
    BusinessAutomation = 4,
    PartexStartGroup = 5,
    ParadiseGroup = 6,
    HeidelbergCement = 7,
    HabibBank = 8,
    Renata = 9,
    ServicEngineBPO = 10,
    OrionGroup = 11,
    BasshundharaGroup = 12,
    ComillaDCOffice = 13,
    SajidaFoundation = 14,
    UniqueGroup = 15,
    Intertek = 16,
    TrustBank = 17,
    Agora = 18,
    ModhumotiBank = 19,
    ExpoGroup = 20
  }



  public enum ModuleEnum
  {
    EmpressCore = 1,
    HumanResource = 2,
    AttendanceManagement = 4,
    LeaveManagement = 7,
    PayrollManagement = 10,
    SurveyManagement = 11,
    NewsNoticeManagement = 12,
    TrainingManagement = 14,
    RecruitmentManagement = 15,
    ControlPanel = 16,
    VehicleManagement = 19,
    AnnualIncrement = 20,
    JobConfirmation = 21,
    Notifications = 27,
    PerformanceManagment = 24,
    DivisionalBudget = 25,
    DivisionalPerformanceManagment = 26


  }

  public enum AuditType
  {
    Browse = 1,
    Login = 2,
    Insert = 3,
    Update = 4,
    Delete = 5,
    Error = 6

  }

  public enum AuditStatus
  {
    Success,
    Error,


  }

  public enum AttendanceFlags
  {
    Present,
    Absent,
    Late,
    Weekend,
    Holiday,
    EarlyExit,
    Delay,
    OutStation,
    ShortLeave,
    OutPunchMissing
  }

  public static class NotificationEnum
  {
    public const string BirthDayNotification = "IsBirthDayNotification",
        MarriageDayNotification = "IsMarriageDayNotification",
        EidNotification = "IsEidNotification",
        DurgaPujaNotification = "IsDurgaPujaNotification",
        BuddhaPurnimaNotification = "IsBuddhaPurnimaNotification",
        BanglaNoboBorshoNotification = "IsBanglaNoboBorshoNotification",
        NewYearNotification = "IsNewYearNotification",
        ConfirmationReminderNotification = "IsConfirmationReminderNotification",
        RetirementNotification = "IsRetirementNotification",
        PendingLeaveApprovalNotification = "IsPendingLeaveApprovalNotification",
        PlannedEarnedLeave = "IsPlannedEarnedLeave",
        LateLeaveApplication = "IsLateLeaveApplication",
        ContractEndDayNotification = "IsContractEndDayNotification",
        IntrimReviewNotification = "IsIntrimReviewNotification",
        InvestmentMaturityNotification = "IsInvestmentMaturityNotification",
        AbsentNotification = "IsAbsentNotification",
        AbsentSummaryNotification = "IsAbsentSummaryNotification",
        LatePresentNotification = "IsLatePresentNotification",
        LateSummaryNotification = "IsLateSummaryNotification",
        EarlyExitNotifiaction = "IsEarlyExitNotifiaction",
        EarlyExitSummaryNotifiaction = "IsEarlyExitSummaryNotifiaction",
        OutPunchMissingNotification = "IsOutPunchMissingNotification",
        AttendanceSummaryNotification = "IsAttendanceSummaryNotification",
        DrivingLicenseExpiredNotification = "IsDrivingLicenseExpiredNotification",
        VehicleFitnessExpiryNotification = "IsVehicleFitnessExpiryNotification",
        VehicleInsuranceExpiryNotification = "IsVehicleInsuranceExpiryNotification",
        VehicleTaxExpiryNotification = "IsVehicleTaxExpiryNotification",
        ConfirmationLetterNotification = "IsConfirmationLetterNotification",
        CVSortingNotification = "IsCVSortingReminderNotification",
        InterviewerNotification = "IsInterviewerNotification",
        InterviewerNotificationRemainder = "IsInterviewerNotificationRemainder";

  }

  public enum KRANotificationEnum
  {
    PreperationSubmit = 1601,
    PreperationFreez = 1602,
    PreperationRedo = 1603,
    MidyearOpen = 1604,
    MidyearSubmit = 1605,
    MidyearFreez = 1606,
    MidyearRedo = 1607,
    YearEndOpen = 1608,
    YearEndSubmit = 1609,
    YearEndRedo = 1610,
    YearEndApprove = 1611,
    PIPNotification = 1612,
    GoalSubmit = 1613,
    GoalFreez = 1614,
    GoalRedo = 1615,
    VisionSubmit = 1616,
    BudgetSubmit = 1617,
    BudgetFrezz = 1618,
    BudgetRedo = 1619,
    DepartmentKPISubmit = 1620,
    DepartmentKPIFrezz = 1621,
    DepartmentKPIRedo = 1622,
    DepartmentKPIRecommend = 1623,
    MonthKPIAssessmentOpen = 1624,
    MonthKPIAssessmentSubmit = 1625,
    MonthKPIAssessmenFrezz = 1626,
    MonthKPIAssessmentReview = 1627
  }

}

