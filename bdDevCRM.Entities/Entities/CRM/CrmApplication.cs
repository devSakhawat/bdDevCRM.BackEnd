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

    public virtual ICollection<CrmAdditionalInfo> CrmAdditionalInfo { get; set; } = new List<CrmAdditionalInfo>();

    public virtual ICollection<CrmApplicantCourse> CrmApplicantCourse { get; set; } = new List<CrmApplicantCourse>();

    public virtual ICollection<CrmApplicantInfo> CrmApplicantInfo { get; set; } = new List<CrmApplicantInfo>();

    public virtual ICollection<CrmApplicantReference> CrmApplicantReference { get; set; } = new List<CrmApplicantReference>();

    public virtual ICollection<CrmEducationHistory> CrmEducationHistory { get; set; } = new List<CrmEducationHistory>();

    public virtual ICollection<CrmGMATInformation> CrmGmatinformation { get; set; } = new List<CrmGMATInformation>();

    public virtual ICollection<CrmIELTSInformation> CrmIELTSInformation { get; set; } = new List<CrmIELTSInformation>();

    public virtual ICollection<CrmOthersInformation> CrmOthersInformation { get; set; } = new List<CrmOthersInformation>();

    public virtual ICollection<CrmPermanentAddress> CrmPermanentAddress { get; set; } = new List<CrmPermanentAddress>();

    public virtual ICollection<CrmPresentAddress> CrmPresentAddress { get; set; } = new List<CrmPresentAddress>();

    public virtual ICollection<CrmPTEInformation> CrmPteinformation { get; set; } = new List<CrmPTEInformation>();

    public virtual ICollection<CrmTOEFLInformation> CrmToeflinformation { get; set; } = new List<CrmTOEFLInformation>();

    public virtual ICollection<CrmWorkExperience> CrmWorkExperience { get; set; } = new List<CrmWorkExperience>();
}
