using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecCandidateJoining
{
    public int CandidateJoiningId { get; set; }

    public int ApplicantId { get; set; }

    public int JoiningStateId { get; set; }

    public DateTime? NotifieDate { get; set; }

    public int? NotifieBy { get; set; }

    public DateTime? JoinedDate { get; set; }

    public int? JoinedBy { get; set; }

    public int? JoiningCanceBy { get; set; }

    public DateTime? JoiningCancelDate { get; set; }

    public int? AppointedBy { get; set; }

    public DateTime? AppointedDate { get; set; }

    public DateTime? TemporaryFromDate { get; set; }

    public DateTime? TemporaryToDate { get; set; }

    public int? AppointmentLetterSentBy { get; set; }

    public DateTime? AppointmentLetterSentDate { get; set; }

    public DateTime? IdGenerateDate { get; set; }

    public int? IdGenerateBy { get; set; }

    public DateTime? CircularMailSentDate { get; set; }

    public int? CircularMailSentby { get; set; }

    public DateTime? CircularGeneratedDate { get; set; }

    public int? CircularMailGeneratedby { get; set; }

    public decimal? TraineeReumenarationAmount { get; set; }

    public DateTime? JoiningLetterSentDate { get; set; }

    public int? JoiningLetterSentBy { get; set; }

    public string? JoiningLetterRefNo { get; set; }

    public string? AppointmentSignatoryEmpName { get; set; }

    public string? AppointmentSignatoryEmpId { get; set; }

    public int? CompanyNoticePay { get; set; }

    public int? EmployeeNoticePay { get; set; }

    public string? AppointmentCopyTo { get; set; }

    public int? AppointPaygradeId { get; set; }
}
