using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Hris
{
    public int HrisSettingsId { get; set; }

    public int? IsFieldForceSalaryEnable { get; set; }

    public int? MultipleSalaryAccountApplicable { get; set; }

    public int? IsMobileBillCalculationTotal { get; set; }

    public int? IsWfbalanceCheckingApplicable { get; set; }

    public int? IsMultipleLoanApplicable { get; set; }

    public int? ManualEmployeeOrder { get; set; }

    public string? EmployeeOrderSql { get; set; }

    public int? IsOnlyActiveEmployeeLoad { get; set; }

    public int? FieldForceShowByDefault { get; set; }

    public int? IsEmployeeOrderOff { get; set; }

    public int? IsLwfShowSalarySheet { get; set; }

    public int? IsClientSeparationForMovement { get; set; }

    public int? ExtraMonthlyAttendanceStatus { get; set; }

    public int? IsDynamicEducationActive { get; set; }

    public int? MinmumOtMin { get; set; }

    public int? IsAttendanceMonthCheck { get; set; }

    public int? IsStampEnableForOtherPayment { get; set; }

    public int? IsIndividualLeaveApplicationForAdmin { get; set; }

    public int? IsAccessRestrictionActiveForAppraisal { get; set; }

    public int? IsDuplicateEmployeeUploadCheking { get; set; }

    public int? IsIndividualBulkOutstationDeploymentForAdmin { get; set; }

    public int? IsManulSalaryOrder { get; set; }

    public string? SalaryOrderSql { get; set; }

    public int? IsAdditionalRsmManagerExist { get; set; }

    public int? IsOpeningCplEnable { get; set; }

    public int? IsPersonalManagementModified { get; set; }

    public int? IsFieldForceAutoMapToCostCenter { get; set; }

    public int? IsDecimalCalculateOnOt { get; set; }

    public int? OmitMovmentBrakupCalOfficeTime { get; set; }

    public int? IsRosterLockEnableAfterSalary { get; set; }

    public int? IsHolidayPunchOn { get; set; }

    public int? IsCompanyMandetoryForSalary { get; set; }

    public int? IsIndividualLeavePolicyMapping { get; set; }

    public int? IsArearSeparately { get; set; }

    public int? ArearCtcId { get; set; }

    public int? UnpaidSalaryDisable { get; set; }

    public int? IsReportViewerToPdf { get; set; }

    public int? IsCostCenterShowInSalarySheet { get; set; }

    public int? IsOnlyBankEnable { get; set; }

    public int? IsOnlyCashEnable { get; set; }

    public int? IsPfCompanyEnable { get; set; }

    public int? IsBasicPayEnable { get; set; }

    public int? IsCashEmployeeEnableOnBank { get; set; }

    public int? IsNetTaxEnableInCash { get; set; }

    public int? IsArearFestibleBonus { get; set; }

    public int? PunchTimeAdjustment { get; set; }

    public int? IsAdjustmentClearanceMandatoryForSalaryProcess { get; set; }

    public int? IsShowManualTaxAmount { get; set; }

    public int? IsSentSalaryMakePaymentEmail { get; set; }

    public int? ArearDeductionCtcId { get; set; }

    public int? IsClosedEmployeeEntry { get; set; }

    public int? IsAttendanceMonthEndMandatoryForSalary { get; set; }

    public int? IsDeleteDuplicateWhileSalaryProcess { get; set; }

    public int? IsMultipleApprovalForPendingPayroll { get; set; }

    public string? EmployeeTypeInTargetState { get; set; }

    public string? EmployeeTypeInAchievementState { get; set; }

    public int? AllowedForApplicationDaysAfter { get; set; }

    public int? LeaveApplicationAutoprocessDaysAfter { get; set; }

    public int? IsMultipleApprovalForAttendanceRequest { get; set; }

    public int? IsMultipleApprovalForOutstation { get; set; }

    public int? IsMultipleApprovalForLeave { get; set; }

    public int? IsMultipleApprovalForCpl { get; set; }

    public int? IsMultipleApprovalForOtallocation { get; set; }
}
