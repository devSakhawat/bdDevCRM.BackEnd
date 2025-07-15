using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class CrmApplication
{
    public int ApplicationId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public string ApplicationStatus { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<AdditionalInfo> AdditionalInfo { get; set; } = new List<AdditionalInfo>();

    public virtual ICollection<ApplicantCourse> ApplicantCourse { get; set; } = new List<ApplicantCourse>();

    public virtual ICollection<ApplicantInfo> ApplicantInfo { get; set; } = new List<ApplicantInfo>();

    public virtual ICollection<ApplicantReference> ApplicantReference { get; set; } = new List<ApplicantReference>();

    public virtual ICollection<EducationHistory> EducationHistory { get; set; } = new List<EducationHistory>();

    public virtual ICollection<Gmatinformation> Gmatinformation { get; set; } = new List<Gmatinformation>();

    public virtual ICollection<Ieltsinformation> Ieltsinformation { get; set; } = new List<Ieltsinformation>();

    public virtual ICollection<Othersinformation> Othersinformation { get; set; } = new List<Othersinformation>();

    public virtual ICollection<PermanentAddress> PermanentAddress { get; set; } = new List<PermanentAddress>();

    public virtual ICollection<PresentAddress> PresentAddress { get; set; } = new List<PresentAddress>();

    public virtual ICollection<Pteinformation> Pteinformation { get; set; } = new List<Pteinformation>();

    public virtual ICollection<StatementOfPurpose> StatementOfPurpose { get; set; } = new List<StatementOfPurpose>();

    public virtual ICollection<Toeflinformation> Toeflinformation { get; set; } = new List<Toeflinformation>();

    public virtual ICollection<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
}
