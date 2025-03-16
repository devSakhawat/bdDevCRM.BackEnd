using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ExResignationApplication
{
    public int ResignationApplicationId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? ResignationDate { get; set; }

    public string? Reason { get; set; }

    public DateTime? AppliedDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateUser { get; set; }

    public int? StateId { get; set; }

    public bool? IsAcepted { get; set; }

    public bool? IntentToRetain { get; set; }

    public string? FinalStatus { get; set; }

    public string? ResonForSeparation { get; set; }

    public DateOnly? LastDayOfWork { get; set; }

    public bool? IsRetained { get; set; }

    public bool? IsExit { get; set; }

    public bool? IsFinalAcepted { get; set; }

    public bool? IsFinalRetained { get; set; }

    public bool? IsDeclearForClear { get; set; }

    public int? IsExitInterviewStarted { get; set; }

    public int? ExitInterviewStartedBy { get; set; }

    public DateTime? ExitInterviewStartedDate { get; set; }

    public int? IsClearenceVerified { get; set; }

    public int? IsSendToApproverOrRecommender { get; set; }

    public DateTime? AskingSeparationDate { get; set; }

    public DateTime? DateOfSeparation { get; set; }

    public int? AcceptanceLetterType { get; set; }

    public string? Remarks { get; set; }
}
