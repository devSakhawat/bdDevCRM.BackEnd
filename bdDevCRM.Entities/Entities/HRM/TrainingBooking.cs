using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingBooking
{
    public int TrainingBookingId { get; set; }

    public int TrainingScheduleId { get; set; }

    public int? TrainingPlanningId { get; set; }

    public int TrainingInfoId { get; set; }

    public int HrRecordId { get; set; }

    public DateTime? AppliedDate { get; set; }

    public int Status { get; set; }

    public string? StatusName { get; set; }

    public int? InvitedBy { get; set; }

    public DateTime? InvitedDate { get; set; }

    public int? RecomendedBy { get; set; }

    public DateTime? RecomendedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? RejectedBy { get; set; }

    public DateTime? RejectedDate { get; set; }

    public int? FeedbackSubmitted { get; set; }

    public int? CertificateUploadStatus { get; set; }

    public string? CertificateUploadPath { get; set; }

    public string? Rational { get; set; }

    public int? TrainingCompanyId { get; set; }

    public int? TrainingBranchId { get; set; }

    public int? TrainingDivisionId { get; set; }

    public int? TrainingDepartmentId { get; set; }

    public int? TrainingFacilityId { get; set; }

    public int? TrainingSectionId { get; set; }
}
