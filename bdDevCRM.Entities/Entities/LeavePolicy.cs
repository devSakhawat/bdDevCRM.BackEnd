using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeavePolicy
{
    public int LeavePolicyId { get; set; }

    /// <summary>
    /// Casual Leave, Medical Leave, Annual Leave
    /// </summary>
    public int? LeaveTypeId { get; set; }

    /// <summary>
    /// 0=none;1=Full;2=Half
    /// </summary>
    public int? EncashmentType { get; set; }

    public int? IsCertificateRequired { get; set; }

    public int? NoOfDays { get; set; }

    public int? IsLeavePlanApplicable { get; set; }

    public int? MaxAvilTime { get; set; }

    public int? EligiblityTime { get; set; }

    public int? IsActive { get; set; }

    public int? IsHolidayReplacement { get; set; }

    public int? IsShortLeave { get; set; }

    public decimal? ShortLeaveDuration { get; set; }

    public int? MaxChange { get; set; }

    public int? IsLeaveMarge { get; set; }

    public int? IsHolidayMarge { get; set; }

    public int? IsLfaapplicable { get; set; }

    public int? LfaminimumDays { get; set; }

    public int? IsDeligationApplicable { get; set; }

    public int? IsEncashmentAppForFs { get; set; }

    public int? IsProdataCalculate { get; set; }

    public int? IsNomineeValidation { get; set; }

    public int? IsGovHoliday { get; set; }

    public string? LeavePolicyName { get; set; }

    public int? EndUserVisibilityDisbale { get; set; }

    public int? ProRataCalculateOn { get; set; }

    public int? DaysToEarnOneLeave { get; set; }

    public int? IsNextHolidayLeave { get; set; }

    public int? IsPreviousHolidayLeave { get; set; }

    public int? IsBalanceNotRemoveable { get; set; }

    public int? IsPreviousYearNotApplicableEmployee { get; set; }

    public int? LeaveDaysToMandatoryCertificate { get; set; }

    public int? IsHalfDayLeaveApplicable { get; set; }

    public int? IsEarnApplicable { get; set; }

    public int? IsBasicForFsencashment { get; set; }

    public int? LeavecanapplyDaysLimit { get; set; }

    public int? LeaveApproavlType { get; set; }

    public int? EarnLeaveForwardDays { get; set; }

    public int? IsVigilanceLeave { get; set; }
}
