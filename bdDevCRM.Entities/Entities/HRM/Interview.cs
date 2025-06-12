using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Interview
{
    public int InterviewId { get; set; }

    public int JobVacancyId { get; set; }

    public int? ApplicantId { get; set; }

    public DateTime? InterviewDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsSelected { get; set; }

    public DateTime? SelectedDate { get; set; }

    public int? SelectedBy { get; set; }

    public int? CoordinatorId { get; set; }

    public string? Vanue { get; set; }

    public DateTime? NoteIssueDate { get; set; }

    public DateTime? NoteApproveDate { get; set; }

    public int? Status { get; set; }

    public int? IsAttendFirstInterview { get; set; }

    public DateTime? FirstInterviewDate { get; set; }

    public int? IsAttendSecondInterview { get; set; }

    public DateTime? SecondInterviewDate { get; set; }

    public int? IsAttendThirdInterview { get; set; }

    public DateTime? ThirdInterviewDate { get; set; }

    public int? InterviewType { get; set; }

    public int? RecuitmentSalaryNote { get; set; }

    public DateTime? RecuitmentJoiningDateNote { get; set; }

    public string? RecuitmentRemarksNote { get; set; }

    public int? RecuitmentNoteMadeBy { get; set; }
}
