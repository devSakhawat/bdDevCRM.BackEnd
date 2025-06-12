using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AcrDetails
{
    public int Acrid { get; set; }

    public int Senderid { get; set; }

    public int Hrrecordid { get; set; }

    public int Typeofassessment { get; set; }

    public DateTime Assessmentperiodform { get; set; }

    public DateTime Assessmentperiodto { get; set; }

    public string? Presentmajorworkres { get; set; }

    public string? Knowledgeachived { get; set; }

    public string? Valueaddedbytheexecutive { get; set; }

    public string? Shortcomming { get; set; }

    public int? Targetmetting { get; set; }

    public int? Projectdealing { get; set; }

    public int? Supportresponse { get; set; }

    public int? Attendance { get; set; }

    public int? Newknowledgegained { get; set; }

    public int? Properofficediscipline { get; set; }

    public int? Qualityofwork { get; set; }

    public int? Cooparationtoothers { get; set; }

    public string? Nextyearvalueaddedforcompany { get; set; }

    public int? Physicalfitness { get; set; }

    public int? Statusid { get; set; }

    public DateTime? Actiondate { get; set; }

    public int? ApproverOrSupervisorid { get; set; }

    public int? IsApprove { get; set; }

    public int? ApproverId { get; set; }

    public int? IsSupervise { get; set; }

    public int? SuperviserId { get; set; }

    public int? IsAdminRecommended { get; set; }

    public int? AdminRecommenderId { get; set; }

    public string? ReasonOfSendBack { get; set; }

    public string? ApproverComments { get; set; }

    public string? SupervisorComments { get; set; }

    public string? AdminComments { get; set; }
}
